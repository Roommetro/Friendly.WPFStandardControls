using System;
using System.Diagnostics;
using System.Windows;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;

namespace Test
{
    [TestClass]
    public class WPFTextBoxTest
    {
        WindowsAppFriend app;
        dynamic mainWindow;

        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            mainWindow = app.Type<Application>().Current.MainWindow;
            WindowsAppExpander.LoadAssemblyFromFile(app, GetType().Assembly.Location);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (app != null)
            {
                app.Dispose();
                Process process = Process.GetProcessById(app.ProcessId);
                process.CloseMainWindow();
                app = null;
            }
        }

        const string TestValue = "It worked!";

        [TestMethod]
        public void TestEmulateChangeText()
        {
            WPFTextBox textBox = new WPFTextBox(app, mainWindow._textBox);
            textBox.EmulateChangeText(TestValue);
            string textBoxText = textBox.Text;
            Assert.AreEqual(TestValue, textBoxText);
        }
    }
}
