﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly;
using Codeer.Friendly.Windows.NativeStandardControls;
using NotInstallProject;
using Codeer.Friendly.Windows.KeyMouse;
using System.Threading;

namespace Test
{
    [TestClass]
    public class ActiveItemTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        public class ItemDriver : IAppVarOwner
        {
            public AppVar AppVar { get; set; }
            public WPFTextBox Text => AppVar.VisualTree().ByType<TextBox>()[0].Dynamic();
            public WPFButtonBase Button => AppVar.VisualTree().ByType<Button>()[0].Dynamic();
            public ItemDriver(AppVar a)
            {
                AppVar = a;
            }
        }
    
        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<ActiveItemTestControl>()();
            main._grid.Children.Add(_ctrl);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void ListBoxActiveItemIndexTest()
        {
            var listEx = new WPFListBox<ItemDriver>(_ctrl._listBox);
            listEx.GetItem(1).VisualTree().ByType<TextBox>()[0].Dynamic().Focus();

            Assert.AreEqual(-1, listEx.SelectedIndex);
            Assert.AreEqual(1, listEx.ActiveItemIndex);
        }

        [TestMethod]
        public void ListViewActiveItemIndexTest()
        {
            var listEx = new WPFListBox<ItemDriver>(_ctrl._listView);
            listEx.GetItem(1).VisualTree().ByType<TextBox>()[0].Dynamic().Focus();

            Assert.AreEqual(-1, listEx.SelectedIndex);
            Assert.AreEqual(1, listEx.ActiveItemIndex);
        }

        [TestMethod]
        public void TreeViewActiveItemKeyTest()
        {
            var treeEx = new WPFTreeView<ItemDriver>(_ctrl._treeView);
            treeEx.GetItem(0, 2, 1).VisualTree().ByType<TextBox>()[0].Dynamic().Focus();

            WPFTextBox textBox = treeEx.GetItem(0, 2, 1).VisualTree().ByType<TextBox>()[0].Dynamic();
            textBox.Click();

            var keys = treeEx.ActiveItemIndices;

            Assert.AreEqual(3, keys.Length);
            Assert.AreEqual(0, keys[0]);
            Assert.AreEqual(2, keys[1]);
            Assert.AreEqual(1, keys[2]);
        }
    }
}
