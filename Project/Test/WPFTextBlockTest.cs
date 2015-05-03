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
    public class WPFTextBlockTest : WPFTestBase<TextBox>
    {
        const string TestValue = "It worked!";

        [TestMethod]
        public void TestText()
        {
            WPFTextBlock textBox = new WPFTextBlock(Target);
            textBox.Dynamic().Text = TestValue;
            string textBoxText = textBox.Text;
            Assert.AreEqual(TestValue, textBoxText);
        }
    }
}
