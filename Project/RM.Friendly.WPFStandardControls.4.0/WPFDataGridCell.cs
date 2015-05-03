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
    /// Provides operations on controls of type System.Windows.Controls.DataGridCell.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.DataGridCellに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFDataGridCell : WPFControlBase4<DataGridCell>
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

        static void EmulateChangeSelected(DataGridCell item, bool isSelected)
        {
            item.Focus();
            item.IsSelected = isSelected;
        }
    }
}
