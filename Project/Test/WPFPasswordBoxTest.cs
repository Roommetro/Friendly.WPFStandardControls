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
    public class WPFPasswordBoxTest
    {
        WindowsAppFriend app;
        WindowControl window;
        WPFPasswordBox password;

        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<PasswordBox>()();
            grid.Children.Add(target);
            password = new WPFPasswordBox(target);
            window = new WindowControl(win);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestChangePassword()
        {
            password.EmulateChangePassword("abc");
            Assert.AreEqual("abc", password.Password);
        }

        [TestMethod]
        public void TestChangePasswordAsync()
        {
            app.Type(GetType()).MessageBoxEvent(password);
            password.EmulateChangePassword("abc", new Async());
            new NativeMessageBox(window.WaitForNextModal()).EmulateButtonClick("OK");
            Assert.AreEqual("abc", password.Password);
        }

        static void MessageBoxEvent(PasswordBox password)
        {
            password.PasswordChanged += delegate
            {
                MessageBox.Show("");
            };
        }
    }
}
