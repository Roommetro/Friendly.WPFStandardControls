using System.Collections.Generic;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFPasswordBox")]
    public class WPFPasswordBoxGenerator : CaptureCodeGeneratorBase
    {
        PasswordBox _control;

        protected override void Attach()
        {
            _control = (PasswordBox)ControlObject;
            _control.PasswordChanged += PasswordChanged;
        }

        protected override void Detach()
        {
            _control.PasswordChanged -= PasswordChanged;
        }

        void PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_control.IsFocused)
            {
                var literal = GenerateUtility.ToLiteral(_control.Password);
                AddSentence(new TokenName(),
                            ".EmulateChangePassword(",
                            literal,
                            new TokenAsync(CommaType.Before),
                            ");");
            }
        }

        public override void Optimize(List<Sentence> code)
        {
            GenerateUtility.RemoveDuplicationFunction(this, code, "EmulateChangePassword");
        }
    }
}
