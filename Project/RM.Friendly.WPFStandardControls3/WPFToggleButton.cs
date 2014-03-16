using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls.Primitives;
namespace RM.Friendly.WPFStandardControls
{
    public class WPFToggleButton : WPFControlBase
    {
        public WPFToggleButton(WindowsAppFriend app, AppVar appVar)
            : base(app, appVar)
        {
        }
        public bool? IsChecked()
        {
            return GetPropValue<bool?>("IsChecked");
        }
        public bool IsThreeState()
        {
            return GetPropValue<bool>("IsThreeState");
        }
        public void EmulateCheck(bool? value)
        {
            this.App[GetType(), "EmulateCheckIntarget"](AppVar, value);
        }
        public void EmulateCheck(bool? value, Async async)
        {
            this.App[GetType(), "EmulateCheckIntarget", async](AppVar, value);
        }
        private static void EmulateCheckIntarget(ToggleButton toggle, bool? value)
        {
            toggle.Focus();
            toggle.IsChecked = value;
        }
    }
}