using System;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RM.Friendly.WPFStandardControls.Inside
{
    /// <summary>
    /// RichTextBoxUtility.
    /// </summary>
    public static class RichTextBoxUtility
    {
        /// <summary>
        /// Get text.
        /// </summary>
        /// <param name="rich">RichTextBox.</param>
        /// <returns>Text.</returns>
        public static string GetText(RichTextBox rich)
        {
            var block = rich.Document.Blocks.FirstBlock;
            if (block == null)
            {
                return string.Empty;
            }
            StringBuilder text = new StringBuilder();
            do
            {
                Paragraph paragraph = block as Paragraph;
                if (paragraph != null)
                {
                    var inline = paragraph.Inlines.FirstInline;
                    do
                    {
                        if (0 < text.Length)
                        {
                            text.Append(Environment.NewLine);
                        }
                        TextRange range = new TextRange(inline.ContentStart, inline.ContentEnd);
                        text.Append(range.Text);
                    } while ((inline = inline.NextInline) != null);
                }
            } while ((block = block.NextBlock) != null);
            return text.ToString();
        }
    }
}
