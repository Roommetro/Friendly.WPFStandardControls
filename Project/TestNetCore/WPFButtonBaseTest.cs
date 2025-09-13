using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

using NotInstallProject;
using RM.Friendly.WPFStandardControls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Test
{
    
    public class WPFButtonBaseTest
    {
        WindowsAppFriend _app;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
        }

        private class ButtonEventCheck
        {
            public bool ButtonClickCalled { get; set; }
            public ButtonEventCheck(ButtonBase ButtonBase, bool showMessageBoxFlg)
            {
                RoutedEventHandler handler = (s, e) =>
                {
                    ButtonClickCalled = true;
                    if (showMessageBoxFlg)
                    {
                        MessageBox.Show("TestMessageWindow");
                    }
                };

                ButtonBase.Click += handler;
            }
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void ButtonClickTest()
        {
            // Arrenge
            dynamic main = _app.Type<Application>().Current.MainWindow;
            AppVar buttonCore = _app.Type<Button>()();
            main._grid.Children.Add(buttonCore);
            dynamic checker = _app.Type<ButtonEventCheck>()(buttonCore, false);
            WPFButtonBase ButtonBase = new WPFButtonBase(buttonCore);

            // Act
            ButtonBase.EmulateClick();
            Assert.IsTrue((bool)checker.ButtonClickCalled);
        }

        [Test]
        public void ButtonClickAsyncTest()
        {
            // Arrenge
            dynamic main = _app.Type<Application>().Current.MainWindow;
            AppVar buttonCore = _app.Type<Button>()();
            main._grid.Children.Add(buttonCore);
            dynamic checker = _app.Type<ButtonEventCheck>()(buttonCore, true);
            WindowControl windowControl = WindowControl.FromZTop(_app);
            WPFButtonBase ButtonBase = new WPFButtonBase(buttonCore);

            // Act
            Async async = new Async();
            ButtonBase.EmulateClick(async);

            // Assert
            WindowControl messageBoxControl = windowControl.WaitForNextModal();
            NativeMessageBox messageBox = new NativeMessageBox(messageBoxControl);
            Assert.AreEqual("TestMessageWindow", messageBox.Message);
            Assert.IsTrue((bool)checker.ButtonClickCalled);

            // Teardown
            messageBox.EmulateButtonClick("OK");
            messageBoxControl.WaitForDestroy();
            async.WaitForCompletion();
        }

        //AppVarWrapperの内部で対象プロセス内にロードされたアセンブリを検索する機能のテスト
        [Test]
        public void Inheritance()
        {
            // Arrenge
            dynamic main = _app.Type<Application>().Current.MainWindow;
            AppVar buttonCore = _app.Type<Button>()();
            main._grid.Children.Add(buttonCore);
            dynamic checker = _app.Type<ButtonEventCheck>()(buttonCore, false);
            var ButtonBase = new WPFButtonBase2(buttonCore);

            // Act
            ButtonBase.EmulateClick();
            ButtonBase.EmulateClick();
        }

        [Test]
        public void Inheritance2()
        {
            // Arrenge
            dynamic main = _app.Type<Application>().Current.MainWindow;
            AppVar buttonCore = _app.Type<Button>()();
            main._grid.Children.Add(buttonCore);
            dynamic checker = _app.Type<ButtonEventCheck>()(buttonCore, false);
            var ButtonBase = new WPFButtonBase2(buttonCore);

            // Act
            ButtonBase.EmulateClick();
            ButtonBase.EmulateClick();
        }
    }
}
