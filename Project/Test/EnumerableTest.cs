using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test
{
    [TestClass]
    public class EnumerableTest
    {
        WindowsAppFriend app;
        dynamic win;
        dynamic control;
        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
            win = app.Type(typeof(Application)).Current.MainWindow;
            dynamic grid = win._grid;
            control = app.Type<EnumerableTestControl>()();
            grid.Children.Add(control);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(app.ProcessId).Kill();
        }

        [TestMethod]
        public void LinqSingleTest()
        {
            var ctrl = ((AppVar)control).LogicalTree().ByType<TextBlock>().ToEnumerable().Single(el => (string)el.Dynamic().Text == "and");
            ctrl.Dynamic().Text = "or";
            Assert.AreEqual((string)((AppVar)control).LogicalTree().ByType<TextBlock>()[1].Dynamic().Text, "or");
        }

        [TestMethod]
        public void LinqAnyTest()
        {
            var isByeExists = ((AppVar)control).LogicalTree().ByType<TextBlock>().ToEnumerable().Any(el => (bool)el.Dynamic().Text.Contains("bye"));
            var isAppleExists = ((AppVar)control).LogicalTree().ByType<TextBlock>().ToEnumerable().Any(el => (bool)el.Dynamic().Text.Contains("apple"));
            Assert.IsTrue(isByeExists);
            Assert.IsFalse(isAppleExists);
        }
    }
}
