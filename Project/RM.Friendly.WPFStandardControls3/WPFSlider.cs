using Codeer.Friendly;
using System;
using System.Collections.Generic;
using System.Text;

namespace RM.Friendly.WPFStandardControls
{
    public class WPFSlider : WPFControlBase
    {
        public WPFSlider(AppVar appVar)
            : base(appVar) { }
        
        public double Value
        {
            get { return Getter<double>("Text"); }
        }
        public void EmulateChangeValue(double value)
        {
            InTarget("EmulateChangeValue", value);
        }
        public void EmulateChangeValue(double value, Async async)
        {
            InTarget("EmulateChangeValue", async, value);
        }

    }
}
