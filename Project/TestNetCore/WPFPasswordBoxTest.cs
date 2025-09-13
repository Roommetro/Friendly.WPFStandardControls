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
    
    public class WPFPasswordBoxTest
    {
        WindowsAppFriend app;
        WindowControl window;
        WPFPasswordBox password;

        [SetUp]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<PasswordBox>()();
            grid.Children.Add(target);
            password = new WPFPasswordBox(target);
            window = new WindowControl(win);
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void TestChangePassword()
        {
            password.EmulateChangePassword("abc");
            Assert.AreEqual("abc", password.Password);
        }

        [Test]
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
