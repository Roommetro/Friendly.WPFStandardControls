
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly;
using System.Linq;

namespace Test
{
    
    public class WPFDependencyObjectCollectionTest
    {
        WindowsAppFriend _app;
        dynamic _ctrl;

        [SetUp]
        public void SetUp()
        {
            _app = new WindowsAppFriend(Process.Start("TargetCore.exe"));
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            WindowsAppExpander.LoadAssembly(_app, typeof(TestAttribute).Assembly);
            dynamic main = _app.Type<Application>().Current.MainWindow;
            _ctrl = _app.Type<SearchTest>().Init(main._grid);
        }

        static SearchTestControl Init(Grid grid)
        {
            SearchTestControl ctrl = new SearchTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void TestSingleOrDefault()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType<FrameworkElement>();
            string name = collection.ByName("_listView").SingleOrDefault().Dynamic().Name;
            Assert.AreEqual("_listView", name);

            var notFound = collection.ByName("xxxxxx").SingleOrDefault();
            Assert.IsTrue(notFound == null);
        }

        [Test]
        public void TestFirstOrDefault()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType<FrameworkElement>();
            string name = collection.ByName("_listView").FirstOrDefault().Dynamic().Name;
            Assert.AreEqual("_listView", name);

            var notFound = collection.ByName("xxxxxx").FirstOrDefault();
            Assert.IsTrue(notFound == null);
        }

        [Test]
        public void TestToArray()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType<FrameworkElement>().ToArray();
            var listView = collection.Where(e => (string)e.Dynamic().GetType().FullName == typeof(ListView).FullName).Single().Dynamic();

            string name = listView.Name;
            Assert.AreEqual("_listView", name);
        }
    }
}
