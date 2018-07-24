﻿using System.Collections.Generic;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFTextBox")]
    public class WPFTextBoxGenerator : CaptureCodeGeneratorBase
    {
        TextBox _control;

        protected override void Attach()
        {
            _control = (TextBox)ControlObject;
            _control.TextChanged += TextChanged;
        }

        protected override void Detach()
        {
            _control.TextChanged -= TextChanged;
        }

        void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_control.IsFocused)
            {
                var literal = GenerateUtility.ToLiteral(_control.Text);
                AddSentence(new TokenName(),
                            ".EmulateChangeText(",
                            literal,
                            new TokenAsync(CommaType.Before),
                            ");");
            }
        }

        public override void Optimize(List<Sentence> code)
        {
            GenerateUtility.RemoveDuplicationFunction(this, code, "EmulateChangeText");
        }
    }
}
