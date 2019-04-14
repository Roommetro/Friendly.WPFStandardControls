using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using RM.Friendly.WPFStandardControls;
using System.Collections.Generic;
using Codeer.Friendly;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly.Windows.Grasp;

namespace Test
{
    [TestClass]
    public class WPFContextMenuTest
    {
        WindowsAppFriend app;
        dynamic win;
        dynamic control;
        [TestInitialize]
        public void TestInitialize() {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            control = app.Type<WPFContextMenuTestControl>()();
            grid.Children.Add(control);
        }

        [TestCleanup]
        public void TestCleanup() 
        {
            Process.GetProcessById(app.ProcessId).Kill();
        }

        [TestMethod]
        public void TestEnable()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            Assert.IsTrue(menu.GetItem(0).IsEnabled);
            Assert.IsTrue(menu.GetItem(1).IsEnabled);
            Assert.IsTrue(menu.GetItem(3, 2, 0).IsEnabled);
            Assert.IsTrue(menu.GetItem(3, 2, 1).IsEnabled);

            menu.Target = control._list2;
            Assert.IsTrue(menu.GetItem(0).IsEnabled);
            Assert.IsTrue(menu.GetItem(1).IsEnabled);
            Assert.IsTrue(menu.GetItem(3, 2, 0).IsEnabled);
            Assert.IsTrue(menu.GetItem(3, 2, 1).IsEnabled);

            menu.Target = control._list3;
            Assert.IsTrue(menu.GetItem(0).IsEnabled);
            Assert.IsFalse(menu.GetItem(1).IsEnabled);
            Assert.IsTrue(menu.GetItem(3, 2, 0).IsEnabled);
            Assert.IsFalse(menu.GetItem(3, 2, 1).IsEnabled);
        }

        [TestMethod]
        public void TestTextAndGetItemIndices()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            Assert.AreEqual("a0", menu.GetItem(2).Text);
            Assert.AreEqual("c1", menu.GetItem(3, 2, 1).Text);
        }

        [TestMethod]
        public void TestGetItemTexts()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            Assert.AreEqual("a0", menu.GetItem("a0").Text);
            Assert.AreEqual("c1", menu.GetItem("a1", "b2", "c1").Text);
        }

        [TestMethod]
        public void TestVisible()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            Assert.AreEqual(Visibility.Visible, menu.GetItem(3, 2, 1).Visibility);
        }

        [TestMethod]
        public void TestCheckable()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            Assert.AreEqual(false, menu.GetItem(3, 2, 1).IsCheckable);
        }

        [TestMethod]
        public void TestChecked()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            Assert.AreEqual(false, menu.GetItem(3, 2, 1).IsChecked);
        }

        [TestMethod]
        public void TestClick()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            menu.GetItem("a1", "b2", "c1").EmulateClick();
            List<string> commands = control._commands;
            Assert.AreEqual(1, commands.Count);
            Assert.AreEqual("Delete", commands[0]);

            menu.GetItem("a1", "b2", "c0").EmulateClick();
            commands = control._commands;
            Assert.AreEqual(2, commands.Count);
            Assert.AreEqual("Delete", commands[0]);
            Assert.AreEqual("Open", commands[1]);
        }

        [TestMethod]
        public void TestClickAsync()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            control._isModal = true;
            menu.GetItem("a1", "b2", "c1").EmulateClick(new Async());
            new NativeMessageBox(new WindowControl(win).WaitForNextModal()).EmulateButtonClick("OK");
            List<string> commands = control._commands;
            Assert.AreEqual(1, commands.Count);
            Assert.AreEqual("Delete", commands[0]);
        }

        [TestMethod]
        public void TestGetAllItems()
        {
            var menu = new WPFContextMenu() { Target = control._list1 };
            var rootItems = menu.GetItems();
            Assert.AreEqual(5, rootItems.Length);
            Assert.AreEqual(0, rootItems[0].GetItems().Length);
            Assert.AreEqual(0, rootItems[1].GetItems().Length);
            Assert.AreEqual(0, rootItems[2].GetItems().Length);
            var a1Items = rootItems[3].GetItems();
            Assert.AreEqual(3, a1Items.Length);
            Assert.AreEqual(0, rootItems[4].GetItems().Length);

            Assert.AreEqual(0, a1Items[0].GetItems().Length);
            Assert.AreEqual(0, a1Items[1].GetItems().Length);
            var b2Items = a1Items[2].GetItems();
            Assert.AreEqual(2, b2Items.Length);

            Assert.AreEqual(0, b2Items[0].GetItems().Length);
            Assert.AreEqual(0, b2Items[1].GetItems().Length);

            Assert.AreEqual("c1", b2Items[1].Text);
        }
    }
}
