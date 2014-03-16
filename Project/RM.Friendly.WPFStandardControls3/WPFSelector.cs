using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RM.Friendly.WPFStandardControls
{
    public partial class WPFSelector : WPFControlsBase{
 
        public WPFSelector(WindowsAppFriend app, AppVar appVar): base(app, appVar)
        {
        }

        public int SelectedIndex {
            get {
                return this.GetPropValue<int>();
            }
            //set{
            //    this.SetPropValue<int>(value);
            //}
        }

        public void EmulateChangeSelectedIndex(int index) {
            this.EmulateInTarget(index);
        }

        static void EmulateChangeSelectedIndexInTarget(ListBox selector, int index) {
            selector.Focus();
            selector.SelectedIndex = index;
        }
    }
}
