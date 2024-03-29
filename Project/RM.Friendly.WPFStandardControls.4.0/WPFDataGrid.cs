﻿using System;
using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Automation;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Input;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.DataGrid.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.DataGridに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.DataGrid")]
    public class WPFDataGrid : WPFControlBase4<DataGrid>
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
        /// current selected cell.
        /// </summary>
#else
        /// <summary>
        /// 現在の選択セル。
        /// </summary>
#endif
        public WPFDataGridCell CurrentCell
        {
            get
            {
                AppVar cell = App.Type<WPFDataGrid>().GetCurrentCell(this);
                return cell.IsNull ? null : cell.Dynamic();
            }
        }

#if ENG
        /// <summary>
        /// Item index of current selected cell.
        /// </summary>
#else
        /// <summary>
        /// 現在の選択セルのアイテムインデックス。
        /// </summary>
#endif
        public int CurrentItemIndex { get { return InvokeStatic(GetCurrentItemIndex, Ret<int>()); } }

#if ENG
        /// <summary>
        /// Column index of current selected cell.
        /// </summary>
#else
        /// <summary>
        /// 現在の選択セルの列インデックス。
        /// </summary>
#endif
        public int CurrentColIndex { get { return InvokeStatic(GetCurrentColIndex, Ret<int>()); } }

#if ENG
        /// <summary>
        /// Item count.
        /// </summary>
#else
        /// <summary>
        /// アイテム数
        /// </summary>
#endif
        public int ItemCount { get { return this.Dynamic().Items.Count; } }

#if ENG
        /// <summary>
        /// Column count.
        /// </summary>
#else
        /// <summary>
        /// カラム数
        /// </summary>
#endif
        public int ColCount { get { return this.Dynamic().Columns.Count; } }

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
            InvokeStatic(EmulateChangeCurrentCell, itemIndex, col);
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
            InvokeStatic(EmulateChangeCurrentCell, async, itemIndex, col);
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
            InvokeStatic(EmulateCellCheck, itemIndex, col, isChecked);
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
        public void EmulateCellCheck(int itemIndex, int col, bool? isChecked, Async async)
        {
            InvokeStatic(EmulateCellCheck, async, itemIndex, col, isChecked);
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
            InvokeStatic(EmulateChangeCellText, itemIndex, col, text);
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
            InvokeStatic(EmulateChangeCellText, async, itemIndex, col, text);
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
            InvokeStatic(EmulateChangeCellComboSelect, itemIndex, col, index);
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
            InvokeStatic(EmulateChangeCellComboSelect, async, itemIndex, col, index);
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
            return InvokeStatic(GetCellText, Ret<string>(), itemIndex, col);
        }

#if ENG
        /// <summary>
        /// Get row.
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <returns>DataGridRow in target app.</returns>
#else
        /// <summary>
        /// 行取得。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <returns>対象プロセス内のDataGridRow。</returns>
#endif
        public WPFDataGridRow GetRow(int itemIndex)
        {
            return new WPFDataGridRow(App.Type(typeof(WPFDataGrid)).GetRow(this, itemIndex));
        }

#if ENG
        /// <summary>
        /// Get cell.
        /// </summary>
        /// <param name="itemIndex">Item index of the cell.</param>
        /// <param name="col">Column number of the cell.</param>
        /// <returns>DataGridCell in target app.</returns>
#else
        /// <summary>
        /// セル取得。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <returns>対象プロセス内のDataGridCel</returns>
#endif
        [ItemDriverGetter(ActiveItemKeyProperty = "CurrentCell")]
        public WPFDataGridCell GetCell(int itemIndex, int col)
        {
            return new WPFDataGridCell(App.Type(typeof(WPFDataGrid)).GetCell(this, itemIndex, col));
        }

        static HitTestFilterBehavior OnHitTestFilterCallback(DependencyObject target, List<DependencyObject> list)
        {
            var element = target as FrameworkElement;
            if (element != null)
            {
                if (element.Visibility != Visibility.Visible
                    || element.Opacity <= 0)
                {
                    return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
                }
                list.Add(element);
            }
            else
            {
                return HitTestFilterBehavior.ContinueSkipSelf;
            }
            return HitTestFilterBehavior.Continue;
        }

        static DataGridCell GetFeaturedCell(DataGrid grid)
        {
            if (grid.IsMouseOver)
            {
                var focusedVisual = new List<DependencyObject>();
                VisualTreeHelper.HitTest(grid, x => OnHitTestFilterCallback(x, focusedVisual),
                    resultTmp =>
                    {
                        // HitTest結果にはDataGridやDataGridCellが入らないのでフィルタ内で取得する
                        return HitTestResultBehavior.Stop;
                    },
                    new PointHitTestParameters(Mouse.GetPosition(grid)));
                foreach (var item in focusedVisual)
                {
                    var cell = item as DataGridCell;
                    if (cell == null)
                    {
                        continue;
                    }
                    return cell;
                }
            }

            return null;
        }

#if ENG
        /// <summary>
        /// Featured Cell.
        /// This is used when capturing with TestAssistant Pro.
        /// </summary>
#else
        /// <summary>
        /// 注目されたセル
        /// TestAssistantProでのキャプチャ時に使われます。
        /// </summary>
#endif
        public WPFDataGridCell FeaturedCell
        {
            get
            {
                AppVar cell = App.Type<WPFDataGrid>().GetFeaturedCell(this);
                return cell.IsNull ? null : cell.Dynamic();
            }
        }

#if ENG
        /// <summary>
        /// Get item's UserControlDriver.
        /// </summary>
        /// <param name="itemIndex">Item index.</param>
        /// <param name="col">Item column No.</param>
        /// <returns>UserControlDriver.</returns>
#else
        /// <summary>
        /// 指定の位置のセルに割当たったUserControlDriverを取得
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="col">列。</param>
        /// <returns>UserControlDriver</returns>
#endif
        [ItemDriverGetter(ActiveItemKeyProperty = "FeaturedCell")]
        public WPFDataGridCell GetFeaturedCell(int itemIndex, int col)
        {
            return new WPFDataGridCell(App.Type(typeof(WPFDataGrid)).GetCell(this, itemIndex, col));
        }

#if ENG
        /// <summary>
        /// Begin edit.
        /// </summary>
#else
        /// <summary>
        /// 編集を開始します。
        /// </summary>
#endif
        public void EmulateBeginEdit()
            => this.Dynamic().BeginEdit();

#if ENG
        /// <summary>
        /// Commit edit.
        /// </summary>
#else
        /// <summary>
        /// 編集を終了します。
        /// </summary>
#endif
        public void EmulateCommitEdit()
            => this.Dynamic().CommitEdit(DataGridEditingUnit.Row, true);

#if ENG
        /// <summary>
        /// Commit edit.
        /// </summary>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 編集を終了します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateCommitEdit(Async async)
            => this.Dynamic().CommitEdit(DataGridEditingUnit.Row, true, async);

        static DataGridCell GetCurrentCell(DataGrid grid)
        {
            var currentCell = grid.CurrentCell;
            if (currentCell == null) return null;

            if (!TestAssistantMode.IsCreatingMode)
            {
                grid.ScrollIntoView(currentCell.Item);
            }

            var row = grid.ItemContainerGenerator.ContainerFromItem(currentCell.Item) as DataGridRow;
            if (row == null) return null;
            var col = grid.Columns.IndexOf(currentCell.Column);
            if (col == -1) return null;
            
            var presenter = row.VisualTree().OfType<DataGridCellsPresenter>().FirstOrDefault();
            if (presenter == null) return null;
            return (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(col);
        }

        static void EnsureRowVisible(DataGrid grid, int itemIndex)
        {
            grid.ScrollIntoView(grid.Items[itemIndex]);
            grid.UpdateLayout();
            if (grid.ItemContainerGenerator.ContainerFromIndex(itemIndex) == null)
            {
                DataGridAutomationPeer peer = new DataGridAutomationPeer(grid);
                var scroll = peer.GetPattern(PatternInterface.Scroll) as IScrollProvider;
                scroll.SetScrollPercent(scroll.HorizontalScrollPercent, 0);
                grid.UpdateLayout();
                while (grid.ItemContainerGenerator.ContainerFromIndex(itemIndex) == null)
                {
                    scroll.Scroll(ScrollAmount.NoAmount, ScrollAmount.LargeIncrement);
                    grid.UpdateLayout();
                }
            }
        }

        static DataGridRow GetRow(DataGrid grid, int itemIndex)
        {
            EnsureRowVisible(grid, itemIndex);
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(itemIndex);
        }

        static DataGridCell GetCell(DataGrid grid, int itemIndex, int col)
        {
            return GetCell(grid, GetRow(grid, itemIndex), col);
        }

        static DataGridCell GetCell(DataGrid grid, DataGridRow row, int col)
        {
            grid.ScrollIntoView(row, grid.Columns[col]);
            var presenter =
                   (DataGridCellsPresenter)VisualTreeUtility.GetCoreElement(row, typeof(DataGridCellsPresenter));
            return (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(col);
        }

        static void EmulateChangeCurrentCell(DataGrid grid, int itemIndex, int col)
        {
            grid.Focus();
            grid.SelectedIndex = itemIndex;
            grid.CurrentCell = new DataGridCellInfo(GetCell(grid, itemIndex, col));
        }

        static string GetCellText(DataGrid grid, int itemIndex, int col)
        {
            EnsureRowVisible(grid, itemIndex);
            DataGridRow temp = grid.ItemContainerGenerator.ContainerFromIndex(itemIndex) as DataGridRow;
            object obj = grid.Columns[col].GetCellContent(temp);
            if (obj.GetType().GetProperty("Text") != null)
            {
                dynamic text = obj;
                try
                {
                    return text.Text;
                }
                catch
                {
                    throw new NotSupportedException(ResourcesLocal4.Instance.DataGridErrorHasNotTextProperty);
                }
            }

            var framework = obj as FrameworkElement;
            if (framework != null)
            {
                var textBlock = framework.VisualTree().ByType<TextBlock>().FirstOrDefault();
                if (textBlock != null) return textBlock.Text;
                var textBox = framework.VisualTree().ByType<TextBox>().FirstOrDefault();
                if (textBox != null) return textBox.Text;
                var checkBox = framework.VisualTree().ByType<CheckBox>().FirstOrDefault();
                if (checkBox != null)
                {
                    return checkBox.IsChecked == null ? "null" : checkBox.IsChecked.Value.ToString().ToLower();
                }
            }
            throw new NotSupportedException(ResourcesLocal4.Instance.DataGridErrorHasNotTextProperty);
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
                throw new NotSupportedException(ResourcesLocal4.Instance.DataGridErrorNotTextBoxCell);
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
                throw new NotSupportedException(ResourcesLocal4.Instance.DataGridErrorNotCheckBoxCell);
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
                throw new NotSupportedException(ResourcesLocal4.Instance.DataGridErrorNotComboBoxCell);
            }
        }

        static int GetCurrentItemIndex(DataGrid grid)
        {
            //TODO refactoring
            if (grid.Items.Count == 0)
            {
                return -1;
            }
            var current = grid.CurrentCell;

            if (grid.Items[0].GetType().IsValueType)
            {
                for (int itemIndex = 0; itemIndex < grid.Items.Count; itemIndex++)
                {
                    DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(itemIndex);
                    if (row == null)
                    {
                        continue;
                    }
                    for (int col = 0; col < grid.Columns.Count; col++)
                    {
                        DataGridCell cell = GetCell(grid, row, col);
                        if (current == new DataGridCellInfo(cell))
                        {
                            return itemIndex;
                        }
                    }
                }
                return -1;
            }
            else
            {
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
        }

        static int GetCurrentColIndex(DataGrid grid)
        {
            //TODO refactoring
            var current = grid.CurrentCell;
            if (current == null || current.Item == null || current.Column == null)
            {
                return -1;
            }
            return grid.Columns.IndexOf(current.Column);
        }
    }
}
