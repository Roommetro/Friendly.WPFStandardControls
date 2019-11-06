using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using System;
using System.Windows.Interop;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// This class generates operation codes for UIElement.
    /// </summary>
#else
    /// <summary>
    /// UIElementの操作コードを生成します。
    /// </summary>
#endif
    public class WPFUIElement : IAppVarOwner, IUIObject
    {
#if ENG
        /// <summary>
        /// Application manipulation object.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション操作クラスです。
        /// </summary>
#endif
        public WindowsAppFriend App => (WindowsAppFriend)AppVar.App;

#if ENG
        /// <summary>
        /// Variable manipulation object within the target application.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション変数操作クラスです。
        /// </summary>
#endif
        public AppVar AppVar { get; }

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
                var size = (System.Windows.Size)AppVar["RenderSize"]().Core;
                return new System.Drawing.Size((int)size.Width, (int)size.Height);
            }
        }

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
        public WPFUIElement(AppVar appVar)
        {
            if (appVar == null)
            {
                throw new ArgumentNullException("appVar");
            }
            AppVar = appVar;
            WPFStandardControls_3.Injection((WindowsAppFriend)AppVar.App);
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
            var pos = (System.Windows.Point)AppVar["PointToScreen"](new System.Windows.Point(clientPoint.X, clientPoint.Y)).Core;
            return new System.Drawing.Point((int)pos.X, (int)pos.Y);
        }
    }
}
