using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ProgressBar.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ProgressBarに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.ProgressBar")]
    public class WPFProgressBar : WPFControlBase<ProgressBar>
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        public WPFProgressBar(AppVar appVar)
            : base(appVar){ }

#if ENG
        /// <summary>
        /// Returns the control's value.
        /// </summary>
#else
        /// <summary>
        /// 値を取得します。
        /// </summary>
#endif
        public double Value
        {
            get { return Getter<double>("Value"); }
        }

#if ENG
        /// <summary>
        /// Returns the control's minimum value.
        /// </summary>
#else
        /// <summary>
        /// 最小値を取得します。
        /// </summary>
#endif
        public double Minimum
        {
            get { return Getter<double>("Minimum"); }
        }

#if ENG
        /// <summary>
        /// Returns the control's maximum value.
        /// </summary>
#else
        /// <summary>
        /// 最大値を取得します。
        /// </summary>
#endif
        public double Maximum
        {
            get { return Getter<double>("Maximum"); }
        }
    }
}
