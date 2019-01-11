using System;
using System.Collections.Generic;
using System.Text;

namespace XamFormsTest
{
    public class StageData
    {
        public string stageName;
        public bool isNeutral;
        public bool isCounter;

        public string ImageName
        {
            get
            {
                return stageName.Replace(" ", string.Empty);
            }
        }
    }
}
