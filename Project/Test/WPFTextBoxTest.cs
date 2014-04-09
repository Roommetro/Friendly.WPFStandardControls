using System;
using System.Diagnostics;
using System.Windows;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;

namespace Test
{
    [TestClass]
    public class WPFTextBoxTest : WPFTestBase<TextBox>
    {
        const string TestValue = "It worked!";

        [TestMethod]
        public void TestEmulateChangeText()
        {
            WPFTextBox textBox = new WPFTextBox(Target);
            textBox.EmulateChangeText(TestValue);
            string textBoxText = textBox.Text;
            Assert.AreEqual(TestValue, textBoxText);
        }

        [TestMethod]
        public void TestEmulateChangeTextAsync()
        {
            WPFTextBox textBox = new WPFTextBox(Target);
            CallRemoteMethod("AttachChangeTextEvent", textBox);
            textBox.EmulateChangeText(TestValue, new Async());

            ClickNextMessageBox();

            string textBoxText = textBox.Text;
            Assert.AreEqual(TestValue, textBoxText);
        }

        static void AttachChangeTextEvent(TextBox textbox)
        {
            TextChangedEventHandler handler = null;
            handler = (s, e) =>
            {
                MessageBox.Show("");
                textbox.TextChanged -= handler;
            };

            textbox.TextChanged += handler;
        }

    }
}
