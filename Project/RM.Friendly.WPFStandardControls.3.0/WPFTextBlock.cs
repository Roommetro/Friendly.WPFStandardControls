using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.TextBlock.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.TextBlockに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.TextBlock")]
    public class WPFTextBlock : WPFControlBase<TextBlock>
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
        public WPFTextBlock(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Returns the control's text.
        /// </summary>
#else
        /// <summary>
        /// テキストを取得します。
        /// </summary>
#endif
        public string Text
        {
            get { return Getter<string>("Text"); }
        }
    }
}
