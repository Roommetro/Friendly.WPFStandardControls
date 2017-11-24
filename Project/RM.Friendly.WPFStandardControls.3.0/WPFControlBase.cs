using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

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
    public class WPFControlBase<CoreType> : AppVarWrapper<CoreType>, IUIObject where CoreType : UIElement
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

#if ENG
        /// <summary>
        /// Returns the size of IUIObject.
        /// </summary>
#else
        /// <summary>
        /// IUIObjectのサイズを取得します。
        /// </summary>
#endif
        public System.Drawing.Size Size
        {
            get
            {
                var size = Getter<Size>("RenderSize");
                return new System.Drawing.Size((int)size.Width, (int)size.Height);
            }
        }
        
#if ENG
        /// <summary>
        /// Make it active.
        /// </summary>
#else
        /// <summary>
        /// アクティブな状態にします。
        /// </summary>
#endif
        public void Activate()
        {
            var source = App[typeof(HwndSource), "FromVisual"](this);
            new WindowControl(App, (IntPtr)source["Handle"]().Core).Activate();
            AppVar["Focus"]();
        }

#if ENG
        /// <summary>
        /// Convert IUIObject's client coordinates to screen coordinates.
        /// </summary>
        /// <param name="clientPoint">client coordinates.</param>
        /// <returns>screen coordinates.</returns>
#else
        /// <summary>
        /// IUIObjectのクライアント座標からスクリーン座標に変換します。
        /// </summary>
        /// <param name="clientPoint">クライアント座標</param>
        /// <returns>スクリーン座標</returns>
#endif
        public System.Drawing.Point PointToScreen(System.Drawing.Point clientPoint)
        {
            var pos = (Point)AppVar["PointToScreen"](new Point(clientPoint.X, clientPoint.Y)).Core;
            return new System.Drawing.Point((int)pos.X, (int)pos.Y);
        }
    }
}
