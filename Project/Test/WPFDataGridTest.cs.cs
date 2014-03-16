using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Windows.Controls;
using System.Windows;

namespace Test
{
    [TestClass]
    public class WPFDataGridTest
    {
        WindowsAppFriend app;
        dynamic dataGrid;
        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            dynamic main = app.Type<Application>().Current.MainWindow;
            dataGrid = app.Type<WPFDataGridTest>().InitDataGrid(main._grid);
        }

        static DataGrid InitDataGrid(Grid grid)
        {
            DataGrid dataGrid = new DataGrid();
            grid.Children.Add(dataGrid);
            dataGrid.ItemsSource = new Item[]
            {
                new Item(){ Name = "山田", Age = 30},
                new Item(){ Name = "鈴木", Age = 31},
            };
            return dataGrid;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    class Item
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
