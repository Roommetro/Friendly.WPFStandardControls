using System;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFExpander")]
    public class WPFExpanderGenerator : CaptureCodeGeneratorBase
    {
        Expander _control;

        protected override void Attach()
        {
            _control = (Expander)ControlObject;
            _control.Expanded += Expanded;
            _control.Collapsed += Collapsed;
        }

        protected override void Detach()
        {
            _control.Expanded -= Expanded;
            _control.Collapsed -= Collapsed;
        }

        void Expanded(object sender, EventArgs e)
        {
            if (!GenerateUtility.HasFocus(_control)) return;
            AddSentence(new TokenName(), ".EmulateOpen(", new TokenAsync(CommaType.Non), ");");
        }

        void Collapsed(object sender, EventArgs e)
        {
            if (!GenerateUtility.HasFocus(_control)) return;
            AddSentence(new TokenName(), ".EmulateClose(", new TokenAsync(CommaType.Non), ");");
        }
    }
}
