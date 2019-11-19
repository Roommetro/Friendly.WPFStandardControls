using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly;

namespace Test
{
    [TestClass]
    public class WPFDependencyObjectCollectionTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new WindowsAppFriend(Process.Start("Target.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            WindowsAppExpander.LoadAssembly(_app, typeof(TestClassAttribute).Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<SearchTest>().Init(main._grid);
        }

        static SearchTestControl Init(Grid grid)
        {
            SearchTestControl ctrl = new SearchTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestSingleOrDefault()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType<FrameworkElement>();
            string name = collection.ByName("_listView").SingleOrDefault().Dynamic().Name;
            Assert.AreEqual("_listView", name);

            var notFound = collection.ByName("xxxxxx").SingleOrDefault();
            Assert.IsTrue(notFound.IsNull);
        }
    }
}
