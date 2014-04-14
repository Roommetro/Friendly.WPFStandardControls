using System.Windows.Controls;
using Codeer.Friendly;

namespace RM.Friendly.WPFStandardControls
{
    /// <summary>
    /// TypeがSystem.Windows.Controls.ProgressBarのウィンドウに対応した操作を提供します。
    /// </summary>
    public class WPFProgressBar : WPFControlBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="appVar">アプリケーション内変数。</param>
        public WPFProgressBar(AppVar appVar)
            : base(appVar){ }

        /// <summary>
        /// 値を取得します。
        /// </summary>
        public double Value
        {
            get { return Getter<double>("Value"); }
        }

        /// <summary>
        /// 最小値を取得します。
        /// </summary>
        public double Minimum
        {
            get { return Getter<double>("Minimum"); }
        }

        /// <summary>
        /// 最大値を取得します。
        /// </summary>
        public double Maximum
        {
            get { return Getter<double>("Maximum"); }
        }
    }
}
