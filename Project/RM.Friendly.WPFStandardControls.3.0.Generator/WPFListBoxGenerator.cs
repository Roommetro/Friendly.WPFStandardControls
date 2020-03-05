using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFListBox")]
    public class WPFListBoxGenerator : CaptureCodeGeneratorBase
    {
        Selector _control;

        protected override void Attach()
        {
            _control = (Selector)ControlObject;
            _control.SelectionChanged += SelectionChanged;
        }

        protected override void Detach()
        {
            _control.SelectionChanged -= SelectionChanged;
        }

        void SelectionChanged(object sender, EventArgs e)
        {
            bool isFocused = (_control.IsMouseCaptured || _control.IsKeyboardFocused || _control.IsFocused);
            foreach (var x in TreeUtilityInTarget.VisualTree(_control))
            {
                var element = x as UIElement;
                if (element != null && (element.IsFocused || element.IsMouseCaptured || element.IsKeyboardFocused))
                {
                    isFocused = true;
                    break;
                }
            }
            if (isFocused && _control.SelectedIndex != -1)
            {
                AddSentence(new TokenName(), ".EmulateChangeSelectedIndex(" + _control.SelectedIndex, new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
