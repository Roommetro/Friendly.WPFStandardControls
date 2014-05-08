using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    class WPFSliderGenerator : GeneratorBase
    {
        Slider _control;

        protected override void Attach()
        {
            _control = ControlObject as Slider;
            _control.ValueChanged += SliderValueChanged;
        }

        protected override void Detach()
        {
            _control.ValueChanged -= SliderValueChanged;
        }

        void SliderValueChanged(object sender, EventArgs e)
        {
            if (_control.IsFocused)
            {
                AddSentence(new TokenName(), ".EmulateValueChanged(", _control.Value, new TokenAsync(CommaType.Non), ");");
            }
        }
    }
}
