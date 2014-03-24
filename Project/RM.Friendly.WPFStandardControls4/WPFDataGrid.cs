using System;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.Generic;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.DataGrid.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.DataGridのウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFDataGrid : WPFControlBase4
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        public WPFDataGrid(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Get Item.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>Item.</returns>
#else
        /// <summary>
        /// アイテムの取得。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>アイテム。</returns>
#endif
        public dynamic this[int index] { get { return this.Dynamic().Items[index]; } }

#if ENG
        /// <summary>
        /// Item index of current selected cell.
        /// </summary>
#else
        /// <summary>
        /// 現在の選択セルのアイテムインデックス。
        /// </summary>
#endif
        public int CurrentItemIndex { get { return (int)InvokeStatic("GetCurrentItemIndex").Core; } }

#if ENG
        /// <summary>
        /// Column index of current selected cell.
        /// </summary>
#else
        /// <summary>
        /// 現在の選択セルの列インデックス。
        /// </summary>
#endif
        public int CurrentColIndex { get { return (int)InvokeStatic("GetCurrentColIndex").Core; } }

#if ENG
        /// <summary>
        /// Changes the cell selected state of cells.
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
#endif
        public void EmulateChangeCurrentCell(int itemIndex, int col)
        {
            InvokeStatic("EmulateChangeCurrentCell", itemIndex, col);
        }

#if ENG
        /// <summary>
        /// Changes the cell selected state of cells.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeCurrentCell(int itemIndex, int col, Async async)
        {
            InvokeStatic("EmulateChangeCurrentCell", async, itemIndex, col);
        }

#if ENG
        /// <summary>
        /// Sets the checked state state of a cell.
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="isChecked">Checked state to use.</param>
#else
        /// <summary>
        /// セルのチェック状態を変更します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <param name="isChecked">チェック状態。</param>
#endif
        public void EmulateCellCheck(int itemIndex, int col, bool? isChecked)
        {
            InvokeStatic("EmulateCellCheck", itemIndex, col, isChecked);
        }

#if ENG
        /// <summary>
        /// Sets the checked state state of a cell.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="isChecked">Checked state to use.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// セルのチェック状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <param name="isChecked">チェック状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateCellCheck(int itemIndex, int col, bool isChecked, Async async)
        {
            InvokeStatic("EmulateCellCheck", async, itemIndex, col, isChecked);
        }

#if ENG
        /// <summary>
        /// Modifies the text of a cell.
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="text">The text to use.</param>
#else
        /// <summary>
        /// セルのテキストを変更します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <param name="text">テキスト。</param>
#endif
        public void EmulateChangeCellText(int itemIndex, int col, string text)
        {
            InvokeStatic("EmulateChangeCellText", itemIndex, col, text);
        }

#if ENG
        /// <summary>
        /// Modifies the text of a cell.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="text">The text to use.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// セルのテキストを変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <param name="text">テキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeCellText(int itemIndex, int col, string text, Async async)
        {
            InvokeStatic("EmulateChangeCellText", async, itemIndex, col, text);
        }

#if ENG
        /// <summary>
        /// Sets the selected value of a combo box cell.
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="index">The index to select.</param>
#else
        /// <summary>
        /// セルコンボの選択を変更します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <param name="index">インデックス。</param>
#endif
        public void EmulateChangeCellComboSelect(int itemIndex, int col, int index)
        {
            InvokeStatic("EmulateChangeCellComboSelect", itemIndex, col, index);
        }

#if ENG
        /// <summary>
        /// Sets the selected value of a combo box cell.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="index">The index to select.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// セルコンボの選択を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <param name="index">インデックス。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeCellComboSelect(int itemIndex, int col, int index, Async async)
        {
            InvokeStatic("EmulateChangeCellComboSelect", async, itemIndex, col, index);
        }

#if ENG
        /// <summary>
        /// Get cell text.
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
#else
        /// <summary>
        /// セルの文字列を取得します
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
#endif
        public string GetCellText(int itemIndex, int col)
        {
            return (string)InvokeStatic("GetCellText", itemIndex, col).Core;
        }

        static void EmulateChangeCurrentCell(DataGrid grid, int itemIndex, int col)
        {
            if (0 < grid.Items.Count && grid.Items[0].GetType().IsValueType)
            {
                throw new NotSupportedException(ResourcesLocal.Instance.DataGridErrorNotSupportedStruct);
            }
            grid.Focus();
            grid.CurrentCell = new DataGridCellInfo(grid.Items[itemIndex], grid.Columns[col]);
        }

        static string GetCellText(DataGrid grid, int itemIndex, int col)
        {
            EmulateChangeCurrentCell(grid, itemIndex, col);
            DataGridRow temp = grid.ItemContainerGenerator.ContainerFromIndex(itemIndex) as DataGridRow;
            dynamic text = grid.Columns[col].GetCellContent(temp);
            try
            {
                return text.Text;
            }
            catch
            {
                throw new NotSupportedException(ResourcesLocal.Instance.DataGridErrorHasNotTextProperty);
            }
        }

        static void EmulateChangeCellText(DataGrid grid, int itemIndex, int col, string text)
        {
            bool success = false;
            EventHandler<DataGridCellEditEndingEventArgs> hanlder = (s, e) =>
            {
                TextBox textBox = e.EditingElement as TextBox;
                if (textBox != null)
                {
                    textBox.Text = text;
                    success = true;
                }
            };
            grid.CellEditEnding += hanlder;
            EmulateChangeCurrentCell(grid, itemIndex, col);
            grid.BeginEdit();
            grid.CommitEdit(DataGridEditingUnit.Row, true);
            grid.CellEditEnding -= hanlder;
            if (!success)
            {
                throw new NotSupportedException(ResourcesLocal.Instance.DataGridErrorNotTextBoxCell);
            }
        }

        static void EmulateCellCheck(DataGrid grid, int itemIndex, int col, bool? isChecked)
        {
            bool success = false;
            EventHandler<DataGridCellEditEndingEventArgs> hanlder = (s, e) =>
            {
                CheckBox checkBox = e.EditingElement as CheckBox;
                if (checkBox != null)
                {
                    checkBox.IsChecked = isChecked;
                    success = true;
                }
            };
            grid.CellEditEnding += hanlder;
            EmulateChangeCurrentCell(grid, itemIndex, col);
            grid.BeginEdit();
            grid.CommitEdit(DataGridEditingUnit.Row, true);
            grid.CellEditEnding -= hanlder;
            if (!success)
            {
                throw new NotSupportedException(ResourcesLocal.Instance.DataGridErrorNotCheckBoxCell);
            }
        }

        static void EmulateChangeCellComboSelect(DataGrid grid, int itemIndex, int col, int index)
        {
            bool success = false;
            EventHandler<DataGridCellEditEndingEventArgs> hanlder = (s, e) =>
            {
                ComboBox comboBox = e.EditingElement as ComboBox;
                if (comboBox != null)
                {
                    comboBox.SelectedIndex = index;
                    success = true;
                }
            };
            grid.CellEditEnding += hanlder;
            EmulateChangeCurrentCell(grid, itemIndex, col);
            grid.BeginEdit();
            grid.CommitEdit(DataGridEditingUnit.Row, true);
            grid.CellEditEnding -= hanlder;
            if (!success)
            {
                throw new NotSupportedException(ResourcesLocal.Instance.DataGridErrorNotComboBoxCell);
            }
        }

        static int GetCurrentItemIndex(DataGrid grid)
        {
            if (0 < grid.Items.Count && grid.Items[0].GetType().IsValueType)
            {
                throw new NotSupportedException(ResourcesLocal.Instance.DataGridErrorNotSupportedStruct);
            }
            grid.Focus();
            var current = grid.CurrentCell;
            if (current == null || current.Item == null)
            {
                return -1;
            }
            for (int i = 0; i < grid.Items.Count; i++)
            {
                if (ReferenceEquals(current.Item, grid.Items[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        static int GetCurrentColIndex(DataGrid grid)
        {
            grid.Focus();
            var current = grid.CurrentCell;
            if (current == null || current.Item == null)
            {
                return -1;
            }
            return current.Column.DisplayIndex;
        }
    }
}
