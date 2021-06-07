using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFSlider")]
    public class WPFSliderGenerator : CaptureCodeGeneratorBase
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
                AddSentence(new TokenName(), ".EmulateChangeValue(", _control.Value, new TokenAsync(CommaType.Non), ");");
            }
        }

        public override void Optimize(List<Sentence> code)
        {
            GenerateUtility.RemoveDuplicationFunction(this, code, "EmulateChangeValue");
        }
    }
}
