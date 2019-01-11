using System;
using System.Collections.Generic;
using System.Text;
using XLabs.Forms.Controls;

namespace XamFormsTest
{
    class StageCheckBox : CheckBox
    {
        public StageData stageData;
        public bool isNeutralStage;

        public StageCheckBox(StageData stageData,bool isNeutralStage)
        {
            this.stageData = stageData;
            this.isNeutralStage = isNeutralStage;
        }

        public void OnCheckedChanged()
        {
            if(isNeutralStage)
                stageData.isNeutral = Checked;
            else
                stageData.isCounter = Checked;
        }
    }
}
