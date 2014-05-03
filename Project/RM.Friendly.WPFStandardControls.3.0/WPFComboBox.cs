using Codeer.Friendly;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{

#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.ComboBox.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.ComboBoxに対応した操作を提供します。
    /// </summary>
#endif
    public partial class WPFComboBox : WPFSelector<ComboBox>
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
        public WPFComboBox(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// If ComboBox is Editable, You get TextBox.
        /// </summary>
#else
        /// <summary>
        /// 編集可能なコンボボックスの場合、テキストボックスを取得できます。
        /// </summary>
#endif
        public WPFTextBox TextBox
        {
            get
            {
                AppVar textBox = AppVar.App[typeof(VisualTreeUtility), "GetCoreElement"](AppVar, typeof(TextBlock).FullName);
                if ((bool)AppVar.App[typeof(object), "ReferenceEquals"](textBox, null).Core)
                {
                    return null;
                }
                return new WPFTextBox(textBox);
            }
        }
    }
}
