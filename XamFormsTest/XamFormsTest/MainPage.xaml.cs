using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace XamFormsTest
{
    public partial class MainPage : TabbedPage
    {
        public string[] stageNames = new string[] { "Town And City", "Smashville", "Smashville", "Smashville", "Smashville", "Smashville", "Smashville", "Smashville", "Smashville", "Smashville", "Smashville" };
        public StageData[] stages;
        List<StageButton> displayedStages;

        public MainPage()
        {
            InitializeComponent();

            //Build the stage array
            stages = new StageData[stageNames.Length];
            for (int i = 0; i < stageNames.Length; i++)
            {
                stages[i] = new StageData();
                stages[i].stageName = stageNames[i];
            }

            LoadSavedSettings();
            BuildStageSettingsGrid();
            UpdateStageBanGrid();
            System.Diagnostics.Debug.WriteLine("Start------------------------------------------------------------");
        }

        void LoadSavedSettings()
        {
            foreach (StageData stage in stages)
            {
                if (Application.Current.Properties.ContainsKey(stage.stageName+"_Neutral"))
                {
                    stage.isNeutral= (bool)Application.Current.Properties[stage.stageName + "_Neutral"];
                }
                else
                {
                    Application.Current.Properties.Add(stage.stageName + "_Neutral", false);
                }
                if(Application.Current.Properties.ContainsKey(stage.stageName + "_Counter"))
                {
                    stage.isCounter = (bool)Application.Current.Properties[stage.stageName + "_Counter"];
                }
                else
                {
                    Application.Current.Properties.Add(stage.stageName + "_Counter", false);
                }
            }
        }

        void SaveSettings(string stageName,string type,bool value)
        {
            if (Application.Current.Properties.ContainsKey(stageName + type))
            {
                Application.Current.Properties[stageName + type] = value;
            }
            else
            {
                Application.Current.Properties.Add(stageName + type, value);
            }
        }

        void OnCheckboxChecked(object sender, EventArgs args)
        {
            //Image imageSender = (Image)sender;
            // Do something
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");

            StageCheckBox checkbox = (StageCheckBox)sender;
            checkbox.OnCheckedChanged();

            string type;
            if (checkbox.isNeutralStage)
                type = "_Neutral";
            else
                type = "_Counter";

            SaveSettings(checkbox.stageData.stageName, type, checkbox.Checked);

            UpdateStageBanGrid();
        }

        void OnClearPressed(object sender, EventArgs args)
        {
            if(displayedStages!=null)
            {
                foreach(StageButton sb in displayedStages)
                {
                    sb.Unban();
                }
            }
        }

        void UpdateStageBanGrid()
        {
            displayedStages = new List<StageButton>();

            #region Neutral Stages
            Grid stageNeutralGrid = (Grid)FindByName("StageNeutralsGrid");
            //Clear the grid
            while (stageNeutralGrid.Children.Count > 0)
                stageNeutralGrid.Children.RemoveAt(0);

            int columnCount = 2;
            int id = 0;

            foreach(StageData stage in stages)
            {
                if (!stage.isNeutral)
                    continue;

                StageButton stageButton = new StageButton(stage) {Source = stage.ImageName };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += stageButton.OnTap;
                stageButton.GestureRecognizers.Add(tapGestureRecognizer);

                displayedStages.Add(stageButton);

                stageNeutralGrid.Children.Add(stageButton, id%columnCount,id/columnCount);
                id++;
            }

            int stagesCount = id;
            ((Label)FindByName("NeutralsText")).IsVisible = id != 0;
            stageNeutralGrid.IsVisible = id != 0;
            #endregion

            #region Counter Stages
            Grid stageCounterGrid = (Grid)FindByName("StageCountersGrid");
            //Clear the grid
            while (stageCounterGrid.Children.Count > 0)
                stageCounterGrid.Children.RemoveAt(0);

            columnCount = 2;
            id = 0;

            foreach (StageData stage in stages)
            {
                if (!stage.isCounter)
                    continue;

                StageButton stageButton = new StageButton(stage) { Source = stage.ImageName };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += stageButton.OnTap;
                stageButton.GestureRecognizers.Add(tapGestureRecognizer);

                displayedStages.Add(stageButton);

                stageCounterGrid.Children.Add(stageButton, id % columnCount, id / columnCount);
                id++;
            }

            stagesCount += id;
            //if id is still 0, there are no counter stages, so hide the grid and the text
            ((Label)FindByName("CountersText")).IsVisible = id != 0;
            stageCounterGrid.IsVisible = id != 0;
            #endregion

            //If the stageCount is still 0, there are no stages, show a text to tell the user to select stages in the settings
            ((Label)FindByName("SelectStagesText")).IsVisible = stagesCount == 0;
        }

        void BuildStageSettingsGrid()
        {
            Grid stageGrid = (Grid)FindByName("StageSettingsGrid");
            int gridRow = 1;

            foreach (StageData stage in stages)
            {
                Label stageName = new Label { Text = stage.stageName,
                    FontFamily = "neuton-bold.ttf#Neuton"
                };
                StageCheckBox neutralCheckbox = new StageCheckBox(stage,true) { Checked = stage.isNeutral};
                StageCheckBox counterCheckbox = new StageCheckBox(stage,false) { Checked = stage.isCounter };

                neutralCheckbox.CheckedChanged += OnCheckboxChecked;
                counterCheckbox.CheckedChanged += OnCheckboxChecked;

                stageGrid.Children.Add(stageName, 0, gridRow);
                stageGrid.Children.Add(neutralCheckbox, 1, gridRow);
                stageGrid.Children.Add(counterCheckbox, 2, gridRow);
                gridRow++;
            }
        }
    }
}
