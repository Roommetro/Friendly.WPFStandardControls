using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFComboBox")]
    public class WPFComboBoxGenerator : CaptureCodeGeneratorBase
    {
        ComboBox _control;
        TextBox _textBox;

        protected override void Attach()
        {
            _control = (ComboBox)ControlObject;
            _control.SelectionChanged += SelectionChanged;
            
            foreach (var e in GetVisualTreeDescendants(_control))
            {
                _textBox = e as TextBox;
                if (_textBox != null) break;
            }
            if (_textBox != null) _textBox.TextChanged += TextBoxChanged;
        }

        protected override void Detach()
        {
            if (_control != null) _control.SelectionChanged -= SelectionChanged;
            if (_textBox != null) _textBox.TextChanged -= TextBoxChanged;
        }

        void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (_textBox.IsFocused)
            {
                var literal = GenerateUtility.ToLiteral(_textBox.Text);
                AddSentence(new TokenName(),
                            ".TextBox.EmulateChangeText(",
                            literal,
                            new TokenAsync(CommaType.Before),
                            ");");
            }
        }

        public override void Optimize(List<Sentence> code)
        {
            GenerateUtility.RemoveDuplicationFunction(this, code, "TextBox.EmulateChangeText");
            GenerateUtility.RemoveDuplicationFunction(this, code, "EmulateChangeSelectedIndex");
        }

        void SelectionChanged(object sender, EventArgs e)
        {   
            if ((_control.IsMouseCaptured || _control.IsKeyboardFocused || _control.IsFocused) && _control.SelectedIndex != -1)
            {
                AddSentence(new TokenName(), ".EmulateChangeSelectedIndex(" + _control.SelectedIndex, new TokenAsync(CommaType.Before), ");");
            }
        }

        static IEnumerable<DependencyObject> GetVisualTreeDescendants(DependencyObject obj)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            if (obj == null) return list;
            list.Add(obj);
            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                list.AddRange(GetVisualTreeDescendants(VisualTreeHelper.GetChild(obj, i)));
            }
            return list;
        }
    }
}
