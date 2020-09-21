using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Windows.Controls;
using System.Windows;
using RM.Friendly.WPFStandardControls;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using System.Linq;

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

        [Serializable]
        public class Item
        {
            public string Name { get; set; }
            public ProgramingLanguage Language { get; set; }
            public bool IsActive { get; set; }

            public override bool Equals(object obj)
            {
                Item item = obj as Item;
                if (item == null)
                {
                    return false;
                }
                return Name == item.Name && Language == item.Language && IsActive == item.IsActive;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

        [Serializable]
        public struct ItemStruct
        {
            public string Name { get; set; }
            public ProgramingLanguage Language { get; set; }
            public bool IsActive { get; set; }


            public override bool Equals(object obj)
            {
                Item item = obj as Item;
                if (item == null)
                {
                    return false;
                }
                return Name == item.Name && Language == item.Language && IsActive == item.IsActive;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

        WindowsAppFriend app;
        WPFDataGrid dataGrid;

        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic main = app.Type<Application>().Current.MainWindow;
            dataGrid = new WPFDataGrid(app.Type<WPFDataGridTest>().InitDataGrid(main._grid));
        }

        static void AddEditEvent(DataGrid grid)
        {
            grid.BeginningEdit += delegate
            {
                MessageBox.Show("");
            };
        }

        static void AddCurrentCellEvent(DataGrid grid)
        {
            grid.CurrentCellChanged += delegate
            {
                MessageBox.Show("");
            };
        }

        void ResetConnection()
        {
            int id = app.ProcessId;
            app.Dispose();
            app = new WindowsAppFriend(Process.GetProcessById(id));
            dynamic main = app.Type<Application>().Current.MainWindow;
            dataGrid = new WPFDataGrid(main._grid.Children[0]);
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
        public void TestGetItem()
        {
            Assert.AreEqual("0", (string)dataGrid[0].Name);
        }

        [TestMethod]
        public void TestEmulateChangeCurrentCell()
        {
            dataGrid.EmulateChangeCurrentCell(2, 1);
            Assert.AreEqual(2, dataGrid.CurrentItemIndex);
            Assert.AreEqual(1, dataGrid.CurrentColIndex);
        }

        [TestMethod]
        public void TestEmulateChangeCurrentCellAsync()
        {
            app.Type<WPFDataGridTest>().AddCurrentCellEvent(dataGrid.AppVar);
            Async async = new Async();
            WindowControl main = WindowControl.FromZTop(app);
            dataGrid.EmulateChangeCurrentCell(2, 1, async);
            new NativeMessageBox(main.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
            Assert.AreEqual(2, dataGrid.CurrentItemIndex);
            Assert.AreEqual(1, dataGrid.CurrentColIndex);
        }

        [TestMethod]
        public void TestEmulateChangeCellText()
        {
            dataGrid.EmulateChangeCellText(0, 0, "xxx");
            Assert.AreEqual("xxx", (string)dataGrid.Dynamic().ItemsSource[0].Name);
        }

        [TestMethod]
        public void TestEmulateChangeCellTextAsync()
        {
            app.Type<WPFDataGridTest>().AddEditEvent(dataGrid.AppVar);
            Async async = new Async();
            WindowControl main = WindowControl.FromZTop(app);
            dataGrid.EmulateChangeCellText(0, 0, "xxx", async);
            new NativeMessageBox(main.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
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
        public void TestEmulateChangeCellComboSelectAsync()
        {
            app.Type<WPFDataGridTest>().AddEditEvent(dataGrid.AppVar);
            Async async = new Async();
            WindowControl main = WindowControl.FromZTop(app);
            dataGrid.EmulateChangeCellComboSelect(0, 1, 2, async);
            new NativeMessageBox(main.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
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
        public void TestEmulateCellCheckAsync()
        {
            app.Type<WPFDataGridTest>().AddEditEvent(dataGrid.AppVar);
            Async async = new Async();
            WindowControl main = WindowControl.FromZTop(app);
            dataGrid.EmulateCellCheck(0, 2, true, async);
            new NativeMessageBox(main.WaitForNextModal()).EmulateButtonClick("OK");
            async.WaitForCompletion();
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
            Assert.AreEqual("false", dataGrid.GetCellText(0, 2));

            var cell = dataGrid.GetCell(0, 2);
            var checkBox = cell.VisualTree().ByType<CheckBox>().Single().Dynamic();

            checkBox.IsChecked = null;
            Assert.AreEqual("null", dataGrid.GetCellText(0, 2));
            checkBox.IsChecked = true;
            Assert.AreEqual("true", dataGrid.GetCellText(0, 2));
        }

        [TestMethod]
        public void TestRowColCount()
        {
            Assert.AreEqual(101, dataGrid.ItemCount);
            Assert.AreEqual(3, dataGrid.ColCount);
        }
        
        [TestMethod]
        public void TestSameDataClass()
        {
            dataGrid.Dynamic().ItemsSource =
                Enumerable.Range(0, 100).Select(i => new Item() { Name = "A" }).ToArray();
            ResetConnection();
            dataGrid.EmulateChangeCurrentCell(99, 1);
            Assert.AreEqual(99, dataGrid.CurrentItemIndex);
            Assert.AreEqual(1, dataGrid.CurrentColIndex);
        }

        [TestMethod]
        public void TestSameDataStruct()
        {
            dataGrid.Dynamic().ItemsSource =
                Enumerable.Range(0, 100).Select(i => new ItemStruct() { Name = "A" }).ToArray();
            ResetConnection();
            dataGrid.EmulateChangeCurrentCell(99, 1);
            Assert.AreEqual(99, dataGrid.CurrentItemIndex);
            Assert.AreEqual(1, dataGrid.CurrentColIndex);
        }

        [TestMethod]
        public void GetRowEmulateChangeSelected()
        {
            var item = dataGrid.GetRow(99);
            item.EmulateChangeSelected(false);
            Assert.IsFalse(item.IsSelected);
            item.EmulateChangeSelected(true);
            Assert.IsTrue(item.IsSelected);
        }

        [TestMethod]
        public void GetRowEmulateChangeSelectedAsync()
        {
            WindowControl windowControl = WindowControl.FromZTop(app);
            var item = dataGrid.GetRow(99);
            app.Type(GetType()).MessageBoxEvent(item);
            var a = new Async();
            item.EmulateChangeSelected(true, a);
            Assert.IsTrue(item.IsSelected);
            new NativeMessageBox(windowControl.WaitForNextModal()).EmulateButtonClick("OK");
            a.WaitForCompletion();
        }

        static void MessageBoxEvent(DataGridRow item)
        {
            item.Selected += delegate
            {
                MessageBox.Show("");
            };
        }

        [TestMethod]
        public void GetCellEmulateChangeSelected()
        {
            dataGrid.Dynamic().SelectionUnit = DataGridSelectionUnit.Cell;
            var item = dataGrid.GetCell(99, 1);
            item.EmulateChangeSelected(false);
            Assert.IsFalse(item.IsSelected);
            item.EmulateChangeSelected(true);
            Assert.IsTrue(item.IsSelected);
        }

        [TestMethod]
        public void GetCellEmulateChangeSelectedAsync()
        {
            dataGrid.Dynamic().SelectionUnit = DataGridSelectionUnit.Cell;
            WindowControl windowControl = WindowControl.FromZTop(app);
            var item = dataGrid.GetCell(99, 1);
            app.Type(GetType()).MessageBoxEvent(item);
            var a = new Async();
            item.EmulateChangeSelected(true, a);
            Assert.IsTrue(item.IsSelected);
            new NativeMessageBox(windowControl.WaitForNextModal()).EmulateButtonClick("OK");
            a.WaitForCompletion();
        }

        static void MessageBoxEvent(DataGridCell item)
        {
            item.Selected += delegate
            {
                MessageBox.Show("");
            };
        }

        [TestMethod]
        public void TestAttachtext()
        {
            dataGrid.EmulateChangeCurrentCell(0, 0);
            dataGrid.EmulateBeginEdit();
            dataGrid.GetCell(0, 0).AttachTextBox().EmulateChangeText("xxx");
            dataGrid.EmulateCommitEdit();
            Assert.AreEqual("xxx", (string)dataGrid.Dynamic().ItemsSource[0].Name);
        }

        [TestMethod]
        public void TestAttachCombo()
        {
            dataGrid.EmulateChangeCurrentCell(0, 1);
            dataGrid.EmulateBeginEdit();
            dataGrid.GetCell(0, 1).AttachComboBox().EmulateChangeSelectedIndex(2);
            dataGrid.EmulateCommitEdit();
            Assert.AreEqual(ProgramingLanguage.CSP, (ProgramingLanguage)dataGrid.Dynamic().ItemsSource[0].Language);
        }

        [TestMethod]
        public void TestAttachCheck()
        {
            dataGrid.EmulateChangeCurrentCell(0, 2);
            dataGrid.EmulateBeginEdit();
            dataGrid.GetCell(0, 2).AttachCheckBox().EmulateCheck(true);
            dataGrid.EmulateCommitEdit();
            Assert.AreEqual(true, (bool)dataGrid.Dynamic().ItemsSource[0].IsActive);
        }

    }
}
