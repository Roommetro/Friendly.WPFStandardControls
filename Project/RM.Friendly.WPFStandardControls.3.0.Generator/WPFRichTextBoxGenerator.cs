using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Documents;
using RM.Friendly.WPFStandardControls.Inside;

namespace RM.Friendly.WPFStandardControls.Generator
{
    class WPFRichTextBoxGenerator : GeneratorBase
    {
        RichTextBox _control;
        string _lastText = string.Empty;
        protected override void Attach()
        {
            _control = (RichTextBox)ControlObject;
            _control.TextChanged += TextChanged;
            _lastText = RichTextBoxUtility.GetText(_control);
        }

        protected override void Detach()
        {
            _control.TextChanged -= TextChanged;
        }

        void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_control.IsFocused)
            {
                var text = RichTextBoxUtility.GetText(_control);
                if (text.IndexOf(_lastText) != 0)
                {
                    AddSentence(new TokenName(),
                            ".EmulateClearText(",
                            new TokenAsync(CommaType.Non),
                            ");");
                }
                AddSentence(new TokenName(),
                            ".EmulateAppendText(",
                            GenerateUtility.ToLiteral(text),
                            new TokenAsync(CommaType.Before),
                            ");");
            }
            _lastText = RichTextBoxUtility.GetText(_control);
        }

        public override void Optimize(List<Sentence> code)
        {
            GenerateUtility.RemoveDuplicationFunction(this, code, "EmulateAppendText");
        }
    }
}
