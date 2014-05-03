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
    public class WPFComboBoxTest
    {
        WindowsAppFriend app;

        WPFComboBox comboBox;

        [TestInitialize]
        public void TestInitialize() {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            dynamic target = app.Type<ComboBox>()();
            grid.Children.Add(target);
            target.ItemsSource = Enumerable.Range(0, 100).Select(i => "value " + i).ToArray();
            target.IsEditable = true;
            target.IsReadOnly = false;
            comboBox = new WPFComboBox(target);
        }

        [TestCleanup]
        public void TestCleanup() {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestTextBox()
        {
            comboBox.TextBox.EmulateChangeText("value 90");
            Assert.AreEqual(90, comboBox.SelectedIndex);
        }
    }
}
