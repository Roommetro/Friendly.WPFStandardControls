using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// This is the base class for classes that operate on basic controls in System.Windows.Controls.
    /// </summary>
#else
    /// <summary>
    /// WPFのコントロールを操作するためのクラスの基本クラスです。
    /// </summary>
#endif
    public class WPFControlBase : AppVarWrapper
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
        protected WPFControlBase(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Returns true if the item is set to visible.
        /// </summary>
#else
        /// <summary>
        /// 表示/非表示を取得します。
        /// </summary>
#endif
        public Visibility Visibility { get { return Getter<Visibility>("Visibility"); } }

#if ENG
        /// <summary>
        /// Returns true if the control is enabled.
        /// </summary>
#else
        /// <summary>
        /// 活性/非活性を取得します。
        /// </summary>
#endif
        public bool IsEnabled { get { return Getter<bool>("IsEnabled"); } }
    }
}
