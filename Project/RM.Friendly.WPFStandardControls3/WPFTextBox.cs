using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Windows;

namespace RM.Friendly.WPFStandardControls
{
    public class WPFTextBox : WPFControlsBase
    {
        public WPFTextBox(WindowsAppFriend app, AppVar appVar)
            : base(app, appVar) { }

        public void EmulateChangeText(string text)
        {
            App[GetType(), "EmulateChangeTextInTarget"](AppVar, text);
        }

        public void EmulateChangeText(string text, Async async)
        {
            App[GetType(), "EmulateChangeTextInTarget", async](AppVar, text);
        }

        public string Text
        {
            get
            {
                return (string)AppVar["Text"]().Core;
            }
        }

        private static void EmulateChangeTextInTarget(TextBox textBox, string value)
        {
            textBox.Text = value;
        }
    }
}
