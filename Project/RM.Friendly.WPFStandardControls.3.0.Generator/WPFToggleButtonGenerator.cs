using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator
{
    public class WPFToggleButtonGenerator : GeneratorBase
    {
        ToggleButton _control;

        protected override void Attach()
        {
            _control = (ToggleButton)ControlObject;
            _control.Checked += ChangeCheck;
            _control.Unchecked += ChangeCheck;
            _control.Indeterminate += ChangeCheck;
        }

        protected override void Detach()
        {
            _control.Checked -= ChangeCheck;
            _control.Unchecked -= ChangeCheck;
            _control.Indeterminate -= ChangeCheck;
        }

        void ChangeCheck(object sender, RoutedEventArgs e)
        {
            if (_control.IsFocused)
            {
                string isChecked = "null";
                if (_control.IsChecked.HasValue)
                {
                    isChecked = _control.IsChecked.Value ? "true" : "false";
                }
                AddSentence(new TokenName(), ".EmulateCheck(" + isChecked, new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
