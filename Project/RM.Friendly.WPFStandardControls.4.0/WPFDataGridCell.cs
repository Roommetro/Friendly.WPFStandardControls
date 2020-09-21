using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.DataGridCell.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.DataGridCellに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.DataGridCell")]
    public class WPFDataGridCell : WPFControlBase4<DataGridCell>, IItemKey
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
        public WPFDataGridCell(AppVar appVar)
            : base(appVar) { }
#if ENG
        /// <summary>
        /// Get selection state.
        /// </summary>
#else
        /// <summary>
        /// 選択状態であるかを取得。
        /// </summary>
#endif
        public bool IsSelected { get { return Getter<bool>("IsSelected"); } }

#if ENG
        /// <summary>
        /// Change Selected.
        /// </summary>
        /// <param name="isSelected">Item's selection state.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// </summary>
        /// <param name="isSelected">選択状態。</param>
#endif
        public void EmulateChangeSelected(bool isSelected)
        {
            InvokeStatic(EmulateChangeSelected, isSelected);
        }

#if ENG
        /// <summary>
        /// Change Selected.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="isSelected">Item's selection state.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 選択状態を変更します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="isSelected">選択状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeSelected(bool isSelected, Async async)
        {
            InvokeStatic(EmulateChangeSelected, async, isSelected);
        }

        /// <summary>
        /// Get arguments.
        /// </summary>
        /// <returns>Arguments.</returns>
        public object[] GetArguments()
            => new object[] { (int)App.Type<WPFDataGridCell>().GetItemIndex(this), (int)App.Type<WPFDataGridCell>().GetColumnIndex(this) };

        /// <summary>
        /// Get arguments code.
        /// </summary>
        /// <returns>Arguments code.</returns>
        public string GetArgumentsCode()
            => $"{(int)App.Type<WPFDataGridCell>().GetItemIndex(this)}, {(int)App.Type<WPFDataGridCell>().GetColumnIndex(this)}";

        static void EmulateChangeSelected(DataGridCell cell, bool isSelected)
        {
            cell.Focus();
            cell.IsSelected = isSelected;
        }

        static int GetItemIndex(DataGridCell cell)
        {
            var dataGrid = cell.VisualTree(TreeRunDirection.Ancestors).ByType<DataGrid>().FirstOrDefault();
            return dataGrid.Items.IndexOf(cell.DataContext);
        }

        static int GetColumnIndex(DataGridCell cell)
        {
            var dataGrid = cell.VisualTree(TreeRunDirection.Ancestors).ByType<DataGrid>().FirstOrDefault();
            return dataGrid.Columns.IndexOf(cell.Column);
        }
    }

#if ENG
    /// <summary>
    /// Extension method for WPFDataGridCell.
    /// </summary>
#else
    /// <summary>
    /// WPFDataGridCellに対する拡張メソッドです。
    /// </summary>
#endif
    public static class WPFDataGridCellExtensions
    {
#if ENG
        /// <summary>
        /// Get the TextBox from the cell of the DataGridTextColumn.
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <returns>WPFTextBox</returns>
#else
        /// <summary>
        /// DataGridTextColumnのセルからTextBoxを取得します。
        /// </summary>
        /// <param name="cell">セル</param>
        /// <returnsWPFTextBox></returns>
#endif
        [UserControlDriverIdentify]
        public static WPFTextBox AttachTextBox(this WPFDataGridCell cell)
        {
            if (!(bool)cell.App.Type(typeof(WPFDataGridCellExtensions)).IsText(cell)) return null;
            return cell.VisualTree().ByType<TextBox>().FirstOrDefault()?.Dynamic();
        }

#if ENG
        /// <summary>
        /// Get the ComboBox from the cell of the DataGridComboBoxColumn.
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <returns>WPFComboBox</returns>
#else
        /// <summary>
        /// DataGridComboBoxColumnのセルからComboBoxを取得します。
        /// </summary>
        /// <param name="cell">セル</param>
        /// <returns>WPFComboBox</returns>
#endif
        [UserControlDriverIdentify]
        public static WPFComboBox AttachComboBox(this WPFDataGridCell cell)
        {
            if (!(bool)cell.App.Type(typeof(WPFDataGridCellExtensions)).IsComboBox(cell)) return null;
            return cell.VisualTree().ByType<ComboBox>().FirstOrDefault()?.Dynamic();
        }

#if ENG
        /// <summary>
        /// Get the CheckBox from the cell of the DataGridCheckBoxColumn.
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <returns>WPFToggleButton</returns>
#else
        /// <summary>
        /// DataGridCheckBoxColumnのセルからCheckBoxを取得します。
        /// </summary>
        /// <param name="cell">セル</param>
        /// <returns>WPFToggleButton</returns>
#endif
        [UserControlDriverIdentify]
        public static WPFToggleButton AttachCheckBox(this WPFDataGridCell cell)
        {
            if (!(bool)cell.App.Type(typeof(WPFDataGridCellExtensions)).IsCheckBox(cell)) return null;
            return cell.VisualTree().ByType<CheckBox>().FirstOrDefault()?.Dynamic();
        }

#if ENG
        /// <summary>
        /// Get the Hyperlink from the cell of the  DataGridHyperLinkColumn.
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <returns>WPFHyperlink</returns>
#else
        /// <summary>
        /// DataGridHyperLinkColumnのセルからHyperlinkを取得します。
        /// </summary>
        /// <param name="cell">セル</param>
        /// <returns>WPFHyperlink</returns>
#endif
        [UserControlDriverIdentify]
        public static WPFHyperlink AttachHyperlink(this WPFDataGridCell cell)
        {
            if (!(bool)cell.App.Type(typeof(WPFDataGridCellExtensions)).IsHyperlink(cell)) return null;
            var textBlock = cell.VisualTree().ByType<TextBlock>().FirstOrDefault();
            if (textBlock == null) return null;
            return textBlock.LogicalTree().ByType<Hyperlink>().FirstOrDefault()?.Dynamic();
        }

        static bool IsText(DataGridCell cell)
            => typeof(DataGridTextColumn).IsAssignableFrom(cell.Column.GetType());

        static bool IsComboBox(DataGridCell cell)
            => typeof(DataGridComboBoxColumn).IsAssignableFrom(cell.Column.GetType());

        static bool IsCheckBox(DataGridCell cell)
            => typeof(DataGridCheckBoxColumn).IsAssignableFrom(cell.Column.GetType());

        static bool IsHyperlink(DataGridCell cell)
            => typeof(DataGridHyperlinkColumn).IsAssignableFrom(cell.Column.GetType());
    }
}
