using System;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.Generic;
using Codeer.Friendly;
using Codeer.Friendly.Windows;

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
    public class WPFDataGrid : WPFControlsBase
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        public WPFDataGrid(WindowsAppFriend app, AppVar appVar)
            : base(app, appVar) { }

        /// <summary>
        /// アイテムの取得
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>アイテム</returns>
        public AppVar this[int index] { get { return AppVar["Items"]()["[]"](index); } }

#if ENG
        /// <summary>
        /// Changes the cell selected state of cells.
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
#endif
        public void EmulateChangeCurrentCell(int row, int col)
        {
            App[GetType(), "EmulateChangeCurrentCellInTarget"](AppVar, row, col);
        }

#if ENG
        /// <summary>
        /// Changes the cell selected state of cells.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeCurrentCell(int row, int col, Async async)
        {
            App[GetType(), "EmulateChangeCurrentCellInTarget", async](AppVar, row, col);
        }

#if ENG
        /// <summary>
        /// Sets the checked state state of a cell.
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="isChecked">Checked state to use.</param>
#else
        /// <summary>
        /// セルのチェック状態を変更します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="isChecked">チェック状態。</param>
#endif
        public void EmulateCellCheck(int row, int col, bool isChecked)
        {
            App[GetType(), "EmulateCellCheckInTarget"](AppVar, row, col, isChecked);
        }

#if ENG
        /// <summary>
        /// Sets the checked state state of a cell.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="isChecked">Checked state to use.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// セルのチェック状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="isChecked">チェック状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateCellCheck(int row, int col, bool isChecked, Async async)
        {
            App[GetType(), "EmulateCellCheckInTarget", async](AppVar, row, col, isChecked);
        }

#if ENG
        /// <summary>
        /// Modifies the text of a cell.
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="text">The text to use.</param>
#else
        /// <summary>
        /// セルのテキストを変更します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="text">テキスト。</param>
#endif
        public void EmulateChangeCellText(int row, int col, string text)
        {
            App[GetType(), "EmulateChangeCellTextInTarget"](AppVar, row, col, text);
        }

#if ENG
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="text">The text to use.</param>
#else
        /// <summary>
        /// セルの文字列を取得します
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
#endif
        public string GetCellText(int row, int col)
        {
            return (string)App[GetType(), "GetCellTextInTarget"](AppVar, row, col).Core;
        }

#if ENG
        /// <summary>
        /// Modifies the text of a cell.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="text">The text to use.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// セルのテキストを変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="text">テキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeCellText(int row, int col, string text, Async async)
        {
            App[GetType(), "EmulateChangeCellTextInTarget", async](AppVar, row, col, text);
        }

#if ENG
        /// <summary>
        /// Sets the selected value of a combo box cell.
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="index">The index to select.</param>
#else
        /// <summary>
        /// セルコンボの選択を変更します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="index">インデックス。</param>
#endif
        public void EmulateChangeCellComboSelect(int row, int col, int index)
        {
            App[GetType(), "EmulateChangeCellComboSelectInTarget"](AppVar, row, col, index);
        }

#if ENG
        /// <summary>
        /// Sets the selected value of a combo box cell.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="row">Row number of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <param name="index">The index to select.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// セルコンボの選択を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="index">インデックス。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeCellComboSelect(int row, int col, int index, Async async)
        {
            App[GetType(), "EmulateChangeCellComboSelectInTarget", async](AppVar, row, col, index);
        }


        // ====================================================================================
        // 
        // InTarget
        // 
        // ====================================================================================
        static void EmulateChangeCurrentCellInTarget(DataGrid grid, int row, int col)
        {
            grid.Focus();
            grid.CurrentCell = new DataGridCellInfo(grid.Items[row], grid.Columns[col]);
        }


        /// <summary>
        /// セルのテキストを変更します。
        /// </summary>
        /// <param name="grid">グリッド。</param>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="text">テキスト。</param>
        static void EmulateChangeCellTextInTarget(DataGrid grid, int row, int col, string text)
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
            grid.Focus();
            grid.CurrentCell = new DataGridCellInfo(grid.Items[row], grid.Columns[col]);
            grid.BeginEdit();
            grid.CommitEdit(DataGridEditingUnit.Row, true);
            grid.CellEditEnding -= hanlder;
            if (!success)
            {
                throw new NotSupportedException("テキストボックスのセルではありません。");
            }
        }

        /// <summary>
        /// セルのテキストを取得します。
        /// </summary>
        /// <param name="grid">グリッド。</param>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        static string GetCellTextInTarget(DataGrid grid, int row, int col )
        {
            string text = null;

            // 行のインデックスから行を取得する
            DataGridRow temp = grid.ItemContainerGenerator.ContainerFromIndex(row) as DataGridRow;
            // 行のデータを取得
            text = ((TextBlock)grid.Columns[col].GetCellContent(temp)).Text;

            return text;
        }

        /// <summary>
        /// セルのチェック状態を変更します。
        /// </summary>
        /// <param name="grid">グリッド。</param>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="isChecked">チェック状態。</param>
        static void EmulateCellCheckInTarget(DataGrid grid, int row, int col, bool isChecked)
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
            grid.Focus();
            grid.CurrentCell = new DataGridCellInfo(grid.Items[row], grid.Columns[col]);
            grid.BeginEdit();
            grid.CommitEdit(DataGridEditingUnit.Row, true);
            grid.CellEditEnding -= hanlder;
            if (!success)
            {
                throw new NotSupportedException("チェックボックスのセルではありません。");
            }
        }

        /// <summary>
        /// セルコンボの選択を変更します。
        /// </summary>
        /// <param name="grid">グリッド。</param>
        /// <param name="row">行。</param>
        /// <param name="col">列。</param>
        /// <param name="index">インデックス。</param>
        static void EmulateChangeCellComboSelectInTarget(DataGrid grid, int row, int col, int index)
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
            grid.Focus();
            grid.CurrentCell = new DataGridCellInfo(grid.Items[row], grid.Columns[col]);
            grid.BeginEdit();
            grid.CommitEdit(DataGridEditingUnit.Row, true);
            grid.CellEditEnding -= hanlder;
            if (!success)
            {
                throw new NotSupportedException("コンボボックスのセルではありません。");
            }
        }
    }
}
