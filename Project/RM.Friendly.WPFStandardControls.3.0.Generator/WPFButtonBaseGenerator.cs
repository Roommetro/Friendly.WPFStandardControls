using System;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFButtonBase")]
    public class WPFButtonBaseGenerator : CaptureCodeGeneratorBase
    {
        ButtonBase _control;

        protected override void Attach()
        {
            _control = (ButtonBase)ControlObject;
            _control.Click += ButtonClick;
        }

        protected override void Detach()
        {
            _control.Click -= ButtonClick;
        }

        void ButtonClick(object sender, EventArgs e)
        {
            AddSentence(new TokenName(), ".EmulateClick(", new TokenAsync(CommaType.Non), ");");
        }
    }
}
