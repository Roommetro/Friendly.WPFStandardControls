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
    
    public class WPFExpanderTest
    {
        WindowsAppFriend app;
        WPFExpander expander;
        WindowControl window;

        [SetUp]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<Expander>()();
            grid.Children.Add(target);
            expander = new WPFExpander(target);
            window = new WindowControl(win);
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void TestOpenClose()
        {
            Assert.IsFalse(expander.IsExpanded);
            expander.EmulateOpen();
            Assert.IsTrue(expander.IsExpanded);
            expander.EmulateClose();
            Assert.IsFalse(expander.IsExpanded);
        }

        [Test]
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
