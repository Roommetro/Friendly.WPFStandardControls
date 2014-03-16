using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Windows.Controls;
using System.Windows;
using RM.Friendly.WPFStandardControls;

namespace Test
{
    [TestClass]
    public class WPFDataGridTest
    {
        WindowsAppFriend app;
        dynamic dataGridCore;
        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic main = app.Type<Application>().Current.MainWindow;
            dataGridCore = app.Type<WPFDataGridTest>().InitDataGrid(main._grid);
        }


        static DataGrid InitDataGrid(Grid grid)
        {
            DataGrid dataGrid = new DataGrid();
            grid.Children.Add(dataGrid);
            dataGrid.ItemsSource = new Item[]
            {
                new Item(){ Name = "山田", Language = ProgramingLanguage.C, IsActive = false },
                new Item(){ Name = "鈴木", Language = ProgramingLanguage.CPP, IsActive = true },
            };
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
            WPFDataGrid dataGrid = new WPFDataGrid(app, dataGridCore);
            dataGrid.EmulateChangeCellText(0, 0, "佐藤");
            Assert.AreEqual("佐藤", (string)dataGrid.Dynamic().ItemsSource[0].Name);
        }

        [TestMethod]
        public void TestEmulateChangeCellComboSelect()
        {
            WPFDataGrid dataGrid = new WPFDataGrid(app, dataGridCore);
            dataGrid.EmulateChangeCellComboSelect(0, 1, 2);
            Assert.AreEqual(ProgramingLanguage.CSP, (ProgramingLanguage)dataGrid.Dynamic().ItemsSource[0].Language);
        }

        [TestMethod]
        public void TestEmulateCellCheck()
        {
            WPFDataGrid dataGrid = new WPFDataGrid(app, dataGridCore);
            dataGrid.EmulateCellCheck(0, 2, true);
            Assert.AreEqual(true, (bool)dataGrid.Dynamic().ItemsSource[0].IsActive);
        }
    }

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
}
