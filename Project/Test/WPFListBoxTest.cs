using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Dynamic;
using System.Diagnostics;
using System.Windows;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;
using System.Linq;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace Test
{
    [TestClass]
    public class WPFListBoxTest
    {
        WindowsAppFriend app;

        WPFListBox listBox;

        [TestInitialize]
        public void TestInitialize() {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<ListBox>()();
            grid.Children.Add(target);
            target.ItemsSource = Enumerable.Range(0, 100).Select(i => "value " + i).ToArray();
            listBox = new WPFListBox(target);
        }

        [TestCleanup]
        public void TestCleanup() {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void SelectedIndexの値を取得して設定できる()
        {
            var index = listBox.SelectedIndex;
            Assert.AreEqual(-1, (int)index);

            //selector.SelectedIndex = 3;
            listBox.EmulateChangeSelectedIndex(3);

            index = listBox.SelectedIndex;
            Assert.AreEqual(3, (int)index);
        }

        static void AddSelectionEvent(ListBox list)
        {
            list.SelectionChanged += delegate
            {
                MessageBox.Show("");
            };
        }

        [TestMethod]
        public void SelectedIndexAsync()
        {
            app.Type<WPFListBoxTest>().AddSelectionEvent(listBox.AppVar);
            Async async = new Async();
            WindowControl main = WindowControl.FromZTop(app);
            listBox.EmulateChangeSelectedIndex(3, async);
            new NativeMessageBox(main.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
            int index = listBox.SelectedIndex;
            Assert.AreEqual(3, index);
        }
    }
}
