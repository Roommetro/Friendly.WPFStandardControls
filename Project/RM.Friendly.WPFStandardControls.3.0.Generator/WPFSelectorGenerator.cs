using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFSelector")]
    public class WPFSelectorGenerator : CaptureCodeGeneratorBase
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
            if (GenerateUtility.HasFocus(_control) && _control.SelectedIndex != -1)
            {
                AddSentence(new TokenName(), ".EmulateChangeSelectedIndex(" + _control.SelectedIndex, new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
