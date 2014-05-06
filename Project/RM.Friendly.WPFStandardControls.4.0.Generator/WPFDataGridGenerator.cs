using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.WpfStandardControls.Generator
{
    /// <summary>
    /// コード生成
    /// </summary>
    public class WpfDataGridGenerator : GeneratorBase
    {
        DataGrid _control;

        /// <summary>
        /// アタッチ。
        /// </summary>
        protected override void Attach()
        {
            _control = (DataGrid)ControlObject;
            _control.CellEditEnding += CellEditEnding;
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            _control.CellEditEnding -= CellEditEnding;
        }

        /// <summary>
        /// 選択インデックス変更
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント内容</param>
        void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int col = -1;
            for (int i = 0; i < _control.Columns.Count; i++)
            {
                if (ReferenceEquals(_control.Columns[i], e.Column))
                {
                    col = i;
                    break;
                }
            }

            if (e.EditingElement is TextBox)
            {
                AddSentence(new TokenName(),
                    ".EmulateChangeCellText(@\"" + _control.SelectedIndex + "\", " + col + ", " +
                    ((TextBox)e.EditingElement).Text,
                    new TokenAsync(CommaType.Before), ");");
            }
            else if (e.EditingElement is ComboBox)
            {
                AddSentence(new TokenName(),
                    ".EmulateChangeCellComboSelect(" + _control.SelectedIndex + ", " + col + ", " +
                    ((ComboBox)e.EditingElement).SelectedIndex,
                    new TokenAsync(CommaType.Before), ");");
            }
            else if (e.EditingElement is CheckBox)
            {
                bool? val = ((CheckBox)e.EditingElement).IsChecked;
                string valStr = val.HasValue && val.Value ? "true" : "false";
                AddSentence(new TokenName(),
                    ".EmulateCellCheck(" + _control.SelectedIndex + ", " + col + ", " +
                    valStr,
                    new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
