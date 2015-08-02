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
    public class WPFExpanderTest
    {
        WindowsAppFriend app;
        WPFExpander expander;
        WindowControl window;

        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<Expander>()();
            grid.Children.Add(target);
            expander = new WPFExpander(target);
            window = new WindowControl(win);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestOpenClose()
        {
            Assert.IsFalse(expander.IsExpanded);
            expander.EmulateOpen();
            Assert.IsTrue(expander.IsExpanded);
            expander.EmulateClose();
            Assert.IsFalse(expander.IsExpanded);
        }

        [TestMethod]
        public void TestOpenCloseAsync()
        {
            app.Type(GetType()).MessageBoxEvent(expander);

            Assert.IsFalse(expander.IsExpanded);
            expander.EmulateOpen(new Async());
            new NativeMessageBox(window.WaitForNextModal()).EmulateButtonClick("OK");
            Assert.IsTrue(expander.IsExpanded);
            expander.EmulateClose(new Async());
            new NativeMessageBox(window.WaitForNextModal()).EmulateButtonClick("OK");
            Assert.IsFalse(expander.IsExpanded);
        }

        static void MessageBoxEvent(Expander expander)
        {
            expander.Expanded += delegate
            {
                MessageBox.Show("");
            };
            expander.Collapsed += delegate
            {
                MessageBox.Show("");
            };
        }
    }
}
