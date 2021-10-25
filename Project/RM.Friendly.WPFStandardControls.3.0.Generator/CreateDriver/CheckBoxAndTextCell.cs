using System;
using System.Drawing;
using System.Windows.Forms;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    /// <summary>
    /// チェックボックス付きテキストセルクラス
    /// </summary>
    class CheckBoxAndTextCell : DataGridViewCheckBoxCell
    {
        private const int PadLeft = 2;
        private const int PadRight = 3;
        private const int PadTop = 4;
        private const int PadBottom = 3;

        private readonly TextFormatFlags FormatFlags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;

        private string _text = string.Empty;

        // チェックボックスの横に表示するテキストを取得、設定する。
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public override object DefaultNewRowValue
        {
            get { return false; }
        }

        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds, Rectangle cellBounds,
                                      int rowIndex, DataGridViewElementStates cellState,
                                      object value, object formattedValue, string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            // DataGridViewCheckBoxCell.Paint で、チェックボックスを描画する。
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                       value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // チェックボックスの横のセル内の残りスペースに、テキストを描画する。
            Rectangle checkBoxBounds = base.GetContentBounds(graphics, cellStyle, rowIndex);
            Point textLocation = GetTextLocation(cellBounds, checkBoxBounds);
            var availableTextSize = GetAvailableTextSize(cellBounds, checkBoxBounds);
            var availableTextRect = new Rectangle(textLocation, availableTextSize);
            var foreColor = Selected ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
            TextRenderer.DrawText(graphics, Text,
                                  cellStyle.Font, availableTextRect, foreColor, FormatFlags);
        }

        private Point GetTextLocation(Rectangle cellBounds, Rectangle contentBounds)
        {
            int textX = cellBounds.X + contentBounds.Right + PadLeft;
            int textY = cellBounds.Y + PadTop;
            var textLocation = new Point(textX, textY);
            return textLocation;
        }

        private Size GetAvailableTextSize(Rectangle cellBounds, Rectangle contentBounds)
        {
            int textWidth = Math.Max(0, cellBounds.Width - contentBounds.Width - PadLeft - PadRight);
            int textHeight = Math.Max(0, cellBounds.Height - PadBottom);
            var textSize = new Size(textWidth, textHeight);
            return textSize;
        }

        // カラムの自動サイズ設定に使用する。
        protected override Size GetPreferredSize(Graphics graphics,
                                                 DataGridViewCellStyle cellStyle, int rowIndex,
                                                 Size constraintSize)
        {
            Rectangle checkBoxBounds = base.GetContentBounds(graphics, cellStyle, rowIndex);
            Size preferredTextSize = TextRenderer.MeasureText(graphics, Text, cellStyle.Font);
            int contentWidth = checkBoxBounds.Width + preferredTextSize.Width + PadLeft + PadRight;
            int contentHeight = Math.Max(checkBoxBounds.Height, preferredTextSize.Height + PadTop + PadBottom);
            var contentSize = new Size(contentWidth, contentHeight);
            return contentSize;
        }

        // 追加した Text プロパティもクローンに含める。
        public override object Clone()
        {
            var cloneCell = (CheckBoxAndTextCell)base.Clone();
            cloneCell.Text = this.Text;
            return cloneCell;
        }
    }
}
