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
    public class WPFTextBoxTest
    {
        WindowsAppFriend app;
        WindowControl mainWindow;

        dynamic target;

        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            mainWindow = WindowControl.FromZTop(app);

            dynamic win = app.Type<Application>().Current.MainWindow;
            dynamic grid = win._grid;
            target = app.Type<TextBox>()();
            grid.Children.Add(target);

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
            WPFTextBox textBox = new WPFTextBox(app, target);
            textBox.EmulateChangeText(TestValue);
            string textBoxText = textBox.Text;
            Assert.AreEqual(TestValue, textBoxText);
        }

        [TestMethod]
        public void TestEmulateChangeTextAsync()
        {
            WPFTextBox textBox = new WPFTextBox(app, target);
            app[GetType(), "ChangeTextEvent"](textBox.AppVar);
            textBox.EmulateChangeText(TestValue, new Async());
            new NativeMessageBox(mainWindow.WaitForNextModal()).EmulateButtonClick("OK");
            string textBoxText = textBox.Text;
            Assert.AreEqual(TestValue, textBoxText);
        }

        static void ChangeTextEvent(TextBox textbox)
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
