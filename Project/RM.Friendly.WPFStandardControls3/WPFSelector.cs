using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System.Windows.Controls.Primitives;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.Primitives.Selector.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.Primitives.Selectorのウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public partial class WPFSelector : WPFControlBase
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
        public WPFSelector(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Returns the number of items in the list.
        /// </summary>
#else
        /// <summary>
        /// 一覧のアイテム数を取得します。
        /// </summary>
#endif
        public int ItemCount
        {
            get { return (int)(this["Items"]()["Count"]().Core); }
        }

#if ENG
        /// <summary>
        /// Returns the index of the currently selected item.
        /// </summary>
#else
        /// <summary>
        /// 現在選択されているアイテムのインデックスを取得します。
        /// </summary>
#endif
        public int SelectedIndex { get { return Getter<int>("SelectedIndex"); } }

#if ENG
        /// <summary>
        /// Selects the item with the specified index.
        /// </summary>
        /// <param name="index">Item index.</param>
#else
        /// <summary>
        /// 指定のインデックスのアイテムを選択します。
        /// </summary>
        /// <param name="index">インデックス。</param>
#endif
        public void EmulateChangeSelectedIndex(int index)
        {
            InvokeStatic("EmulateChangeSelectedIndex", index);
        }

#if ENG
        /// <summary>
        /// Selects the item with the specified index.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="index">Item index.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 指定のインデックスのアイテムを選択します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeSelectedIndex(int index, Async async)
        {
            InvokeStatic("EmulateChangeSelectedIndex", async, index);
        }

        static void EmulateChangeSelectedIndex(Selector selector, int index)
        {
            selector.Focus();
            selector.SelectedIndex = index;
        }
    }
}
