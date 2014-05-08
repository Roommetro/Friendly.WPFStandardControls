using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    public class WPFSelectorGenerator : GeneratorBase
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
            AddSentence(new TokenName(), ".EmulateChangeSelectedIndex(" + _control.SelectedIndex, new TokenAsync(CommaType.Before), ");");
        }
    }
}
