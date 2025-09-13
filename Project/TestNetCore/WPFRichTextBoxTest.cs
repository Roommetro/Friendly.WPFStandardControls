using System;
using System.Diagnostics;
using System.Windows;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;

using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;

namespace Test
{
    
    public class WPFRichTextBoxTest : WPFTestBase<RichTextBox>
    {
        [Test]
        public void TestEmulateAppendText()
        {
            WPFRichTextBox textBox = new WPFRichTextBox(Target);
            textBox.EmulateAppendText("A");
            textBox.EmulateAppendText("B");
            Assert.AreEqual("AB", textBox.Text);
        }

        [Test]
        public void TestEmulateAppendTextAsync()
        {
            WPFRichTextBox textBox = new WPFRichTextBox(Target);
            CallRemoteMethod("AttachChangeTextEvent", textBox);
            Async async = new Async();
            textBox.EmulateAppendText("A", async);
            ClickNextMessageBox();
            Assert.AreEqual("A", textBox.Text);
        }

        [Test]
        public void TestEmulateClearText()
        {
            WPFRichTextBox textBox = new WPFRichTextBox(Target);
            textBox.EmulateAppendText("A");
            textBox.EmulateClearText();
            Assert.AreEqual(string.Empty, textBox.Text);
        }

        [Test]
        public void TestEmulateClearTextAsync()
        {
            WPFRichTextBox textBox = new WPFRichTextBox(Target);
            textBox.EmulateAppendText("A");
            CallRemoteMethod("AttachChangeTextEvent", textBox);
            Async async = new Async();
            textBox.EmulateClearText(async);
            ClickNextMessageBox();
            Assert.AreEqual(string.Empty, textBox.Text);
        }

        static void AttachChangeTextEvent(RichTextBox textbox)
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
