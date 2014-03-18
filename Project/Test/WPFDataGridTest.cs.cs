using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Windows.Controls;
using System.Windows;
using RM.Friendly.WPFStandardControls;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Test
{
    [TestClass]
    public class WPFDataGridTest
    {
        public enum ProgramingLanguage
        {
            C,
            CPP,
            CSP
        }

        public class Item
        {
            public string Name { get; set; }
            public ProgramingLanguage Language { get; set; }
            public bool IsActive { get; set; }
        }

        WindowsAppFriend app;
        WPFDataGrid dataGrid;

        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic main = app.Type<Application>().Current.MainWindow;
            dataGrid = new WPFDataGrid(app, app.Type<WPFDataGridTest>().InitDataGrid(main._grid));
        }

        void ResetConnection()
        {
            int id = app.ProcessId;
            app.Dispose();
            app = new WindowsAppFriend(Process.GetProcessById(id));
            dynamic main = app.Type<Application>().Current.MainWindow;
            dataGrid = new WPFDataGrid(app, main._grid.Children[0]);
        }

        static DataGrid InitDataGrid(Grid grid)
        {
            DataGrid dataGrid = new DataGrid();
            grid.Children.Add(dataGrid);
            List<Item> list = new List<Item>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(new Item() { Name = i.ToString(), Language = ProgramingLanguage.C, IsActive = false });
            }
            dataGrid.ItemsSource = list;
            return dataGrid;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestEmulateChangeCellText()
        {
            dataGrid.EmulateChangeCellText(0, 0, "xxx");
            Assert.AreEqual("xxx", (string)dataGrid.Dynamic().ItemsSource[0].Name);
        }

        [TestMethod]
        public void TestEmulateChangeCellTextException()
        {
            TestUtility.TestExceptionMessage(() => { ResetConnection(); dataGrid.EmulateChangeCellText(0, 1, "xxx"); },
                "The cell is not a TextBox.",
                "指定のセルはテキストボックスではありません。");
        }

        [TestMethod]
        public void TestEmulateChangeCellComboSelect()
        {
            dataGrid.EmulateChangeCellComboSelect(0, 1, 2);
            Assert.AreEqual(ProgramingLanguage.CSP, (ProgramingLanguage)dataGrid.Dynamic().ItemsSource[0].Language);
        }

        [TestMethod]
        public void TestEmulateChangeCellComboSelectException()
        {
            TestUtility.TestExceptionMessage(() => { ResetConnection(); dataGrid.EmulateChangeCellComboSelect(0, 0, 2); },
                "The cell is not a ComboBox.",
                "指定のセルはコンボボックスではありません。");
        }

        [TestMethod]
        public void TestEmulateCellCheck()
        {
            dataGrid.EmulateCellCheck(0, 2, true);
            Assert.AreEqual(true, (bool)dataGrid.Dynamic().ItemsSource[0].IsActive);
        }

        [TestMethod]
        public void TestEmulateCellCheckException()
        {
            TestUtility.TestExceptionMessage(() => { ResetConnection(); dataGrid.EmulateCellCheck(0, 0, true); },
                "The cell is not a CheckBox.",
                "指定のセルはチェックボックスではありません。");
        }

        [TestMethod]
        public void TestGetCellText()
        {
            Assert.AreEqual("99", dataGrid.GetCellText(99, 0));
            Assert.AreEqual("C", dataGrid.GetCellText(0, 1));
        }

        [TestMethod]
        public void TestGetCellTextException()
        {
            TestUtility.TestExceptionMessage(() => { ResetConnection(); dataGrid.GetCellText(0, 2); },
                "The cell does not have Text property.",
                "指定のセルはTextプロパティーを持っていません。");
        }

        //@@@非同期テスト
    }
}
