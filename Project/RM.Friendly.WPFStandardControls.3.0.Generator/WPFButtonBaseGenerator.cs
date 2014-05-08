using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls.Generator
{
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
