using System;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [Generator("RM.Friendly.WPFStandardControls.WPFButtonBase")]
    public class WPFButtonBaseGenerator : GeneratorBase
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
            if (_control.IsFocused)
            {
                AddSentence(new TokenName(), ".EmulateClick(", new TokenAsync(CommaType.Non), ");");
            }
        }
    }
}
