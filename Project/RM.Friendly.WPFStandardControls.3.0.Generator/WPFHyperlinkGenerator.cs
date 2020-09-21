using System;
using System.Windows.Documents;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFHyperlink")]
    public class WPFHyperlinkGenerator : CaptureCodeGeneratorBase
    {
        Hyperlink _control;

        protected override void Attach()
        {
            _control = (Hyperlink)ControlObject;
            _control.Click += Click;
        }

        protected override void Detach()
        {
            _control.Click -= Click;
        }

        void Click(object sender, EventArgs e)
        {
            AddSentence(new TokenName(), ".EmulateClick(", new TokenAsync(CommaType.Non), ");");
        }
    }
}
