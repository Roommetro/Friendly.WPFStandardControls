using Codeer.Friendly;
using Codeer.Friendly.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RM.Friendly.WPFStandardControls
{
    public partial class WPFSelector: WPFControlsBase
    {
        public WPFSelector(WindowsAppFriend app, AppVar appVar): base(app, appVar)
        {
        }

        public int SelectedIndex {
            get {
                return this.GetPropValue<int>();
            }
        }

    }
}
