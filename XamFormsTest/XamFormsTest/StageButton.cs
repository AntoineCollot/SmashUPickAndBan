using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamFormsTest
{
    class StageButton : Image
    {
        public StageData stageData;
        bool isBanned;

        public StageButton(StageData stageData)
        {
            isBanned = false;
            this.stageData = stageData;
        }

        public void OnTap(object sender, EventArgs args)
        {
            isBanned = !isBanned;
            if (isBanned)
                Source = stageData.ImageName + "_Banned";
            else
                Source = stageData.ImageName;
        }

        public void Unban()
        {
            isBanned = false;
            Source = stageData.ImageName;
        }
    }
}
