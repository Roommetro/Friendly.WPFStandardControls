using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Windows;

namespace RM.Friendly.WPFStandardControls
{
    public class WPFTextBox : WPFControlBase
    {
        public WPFTextBox(WindowsAppFriend app, AppVar appVar)
            : base(app, appVar) { }

        public void EmulateChangeText(string text)
        {
            this.EmulateInTarget(text);
        }

        public void EmulateChangeText(string text, Async async)
        {
            EmulateInTarget(async, text);
        }

        public string Text
        {
            get { return GetPropValue<string>(); }
        }

        private static void EmulateChangeTextInTarget(TextBox textBox, string value)
        {
            textBox.Text = value;
        }
    }
}
