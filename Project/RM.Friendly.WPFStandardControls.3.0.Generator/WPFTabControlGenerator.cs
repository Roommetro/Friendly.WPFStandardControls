using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFTabControl")]
    public class WPFTabControlGenerator : CaptureCodeGeneratorBase
    {
        Selector _control;
        int _selectedIndex = -1;

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
            if (_control.SelectedIndex == -1) return;
            if (_selectedIndex == _control.SelectedIndex) return;
            _selectedIndex = _control.SelectedIndex;
            AddSentence(new TokenName(), ".EmulateChangeSelectedIndex(" + _control.SelectedIndex, new TokenAsync(CommaType.Before), ");");
        }

        public override void Optimize(List<Sentence> code)
        {
            GenerateUtility.RemoveDuplicationFunction(this, code, "EmulateChangeSelectedIndex");
        }
    }
}
