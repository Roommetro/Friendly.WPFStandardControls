using System;
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
using System.Linq;
using System.Windows.Controls.Primitives;

namespace Test
{
    [TestClass]
    public class SearchTest
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
        public void TestLogicalTree()
        {
            AppVar button1 = Searcher.ByBinding(TreeUtility.LogicalTree(_ctrl), "Button1Command").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            Assert.AreEqual(0, TreeUtility.LogicalTree((AppVar)_ctrl._listView).ByType<ListViewItem>().Count);
        }

        [TestMethod]
        public void TestLogicalTreeInTarget()
        {
            _app.Type(GetType()).TestLogicalTreeInTarget(_ctrl, _ctrl._button1, _ctrl._listView);
        }

        static void TestLogicalTreeInTarget(SearchTestControl ctrl, Button button1, ListView listView)
        {
            var b = SearcherInTarget.ByBinding(TreeUtilityInTarget.LogicalTree(ctrl), "Button1Command").Single();
            Assert.AreEqual(button1, b);
            Assert.AreEqual(0, TreeUtilityInTarget.LogicalTree(listView).ByType<ListViewItem>().Count());
        }

        [TestMethod]
        public void TestLogicalTreeExtensions()
        {
            AppVar button1 = ((AppVar)_ctrl).LogicalTree().ByBinding("Button1Command").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            Assert.AreEqual(0, ((AppVar)_ctrl._listView).LogicalTree().ByType<ListViewItem>().Count);
        }
        class MyAppVar : IAppVarOwner
        {
            public AppVar AppVar { get; set; }
        }

        [TestMethod]
        public void TestLogicalTreeExtensionsForIAppVarOwner()
        {
            AppVar button1 = new MyAppVar() { AppVar = _ctrl }.LogicalTree().ByBinding("Button1Command").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            Assert.AreEqual(0, new WPFListView(_ctrl._listView).LogicalTree().ByType<ListViewItem>().Count);
        }

        [TestMethod]
        public void TestLogicalTreeInTargetExtensions()
        {
            _app.Type(GetType()).TestLogicalTreeInTargetExtensions(_ctrl, _ctrl._button1, _ctrl._listView);
        }

        static void TestLogicalTreeInTargetExtensions(SearchTestControl ctrl, Button button1, ListView listView)
        {
            var b = ctrl.LogicalTree().ByBinding("Button1Command").Single();
            Assert.AreEqual(button1, b);
            Assert.AreEqual(0, listView.LogicalTree().ByType<ListViewItem>().Count());
        }

        [TestMethod]
        public void TestLogicalAncestors()
        {
            var collection = ((AppVar)_ctrl._button1).LogicalTree(TreeRunDirection.Ancestors);
            Assert.AreEqual(collection[0], _ctrl._button1);
            Assert.AreEqual(collection[collection.Count - 1], _app.Type<Application>().Current.MainWindow);

            AppVar item = ((AppVar)_ctrl._listView).VisualTree().ByType<ListViewItem>()[0];
            collection = item.LogicalTree(TreeRunDirection.Ancestors);
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void TestVisualTree()
        {
            AppVar button1 = Searcher.ByBinding(TreeUtility.VisualTree(_ctrl), "Button1Command").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            Assert.AreEqual(3, TreeUtility.VisualTree((AppVar)_ctrl._listView).ByType<ListViewItem>().Count);
        }

        [TestMethod]
        public void TestVisualTreeInTarget()
        {
            _app.Type(GetType()).TestVisualTreeInTarget(_ctrl, _ctrl._button1, _ctrl._listView);
        }

        static void TestVisualTreeInTarget(SearchTestControl ctrl, Button button1, ListView listView)
        {
            var b = SearcherInTarget.ByBinding(TreeUtilityInTarget.VisualTree(ctrl), "Button1Command").Single();
            Assert.AreEqual(button1, b);
            Assert.AreEqual(3, TreeUtilityInTarget.VisualTree(listView).ByType<ListViewItem>().Count());
        }

        [TestMethod]
        public void TestVisualTreeExtensions()
        {
            AppVar button1 = ((AppVar)_ctrl).VisualTree().ByBinding("Button1Command").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            Assert.AreEqual(3, ((AppVar)_ctrl._listView).VisualTree().ByType<ListViewItem>().Count);
        }

        [TestMethod]
        public void TestVisualTreeExtensionsForIAppVarOwner()
        {
            AppVar button1 = new MyAppVar() { AppVar = _ctrl }.VisualTree().ByBinding("Button1Command").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            Assert.AreEqual(3, new WPFListView(_ctrl._listView).VisualTree().ByType<ListViewItem>().Count);
        }

        [TestMethod]
        public void TestVisualAncestors()
        {
            var collection = ((AppVar)_ctrl._button1).VisualTree(TreeRunDirection.Ancestors);
            Assert.AreEqual(collection[0], _ctrl._button1);
            Assert.AreEqual(collection[collection.Count - 1], _app.Type<Application>().Current.MainWindow);

            AppVar item = ((AppVar)_ctrl._listView).VisualTree().ByType<ListViewItem>()[0];
            collection = item.VisualTree(TreeRunDirection.Ancestors);
            Assert.AreEqual(collection[0], item);
            Assert.AreEqual(collection[collection.Count - 1], _app.Type<Application>().Current.MainWindow);
        }

        [TestMethod]
        public void TestVisualTreeInTargetExtensions()
        {
            _app.Type(GetType()).TestVisualTreeInTargetExtensions(_ctrl, _ctrl._button1, _ctrl._listView);
        }

        static void TestVisualTreeInTargetExtensions(SearchTestControl ctrl, Button button1, ListView listView)
        {
            var b = ctrl.VisualTree().ByBinding("Button1Command").Single();
            Assert.AreEqual(button1, b);
            Assert.AreEqual(3, listView.VisualTree().ByType<ListViewItem>().Count());
        }

        [TestMethod]
        public void TestBinding()
        {
            AppVar button1 = Searcher.ByBinding(TreeUtility.LogicalTree(_ctrl), "Button1Command").Single();
            Assert.IsTrue(button1.Equals(_ctrl._button1));

            var collection = Searcher.ByBinding(TreeUtility.LogicalTree(_ctrl), "DataText");
            Assert.AreEqual(2, collection.Count);

            var textBox = Searcher.ByBinding(TreeUtility.LogicalTree(_ctrl), "DataText", new ExplicitAppVar(_ctrl.DataContext)).Single();
            Assert.IsTrue(textBox.Equals(_ctrl._textBox));
        }

        [TestMethod]
        public void TestBindingInTarget()
        {
            _app.Type(GetType()).TestBindingInTarget(_ctrl, _ctrl._button1, _ctrl._textBox);
        }

        static void TestBindingInTarget(SearchTestControl ctrl, Button button1, TextBox textBox)
        {
            DependencyObject b = SearcherInTarget.ByBinding(TreeUtilityInTarget.LogicalTree(ctrl), "Button1Command").Single();
            Assert.AreEqual(button1, b);

            var collection = SearcherInTarget.ByBinding(TreeUtilityInTarget.LogicalTree(ctrl), "DataText");
            Assert.AreEqual(2, collection.Count());

            var t = SearcherInTarget.ByBinding(TreeUtilityInTarget.LogicalTree(ctrl), "DataText", ctrl.DataContext).Single();
            Assert.AreEqual(textBox, t);
        }

        [TestMethod]
        public void TestBindingExtensions()
        {
            AppVar target = _ctrl;
            AppVar button1 = target.LogicalTree().ByBinding("Button1Command").Single();
            Assert.IsTrue(button1.Equals(_ctrl._button1));

            var collection = target.LogicalTree().ByBinding("DataText");
            Assert.AreEqual(2, collection.Count);

            var textBox = target.LogicalTree().ByBinding("DataText", new ExplicitAppVar(_ctrl.DataContext)).Single();
            Assert.IsTrue(textBox.Equals(_ctrl._textBox));
        }

        [TestMethod]
        public void TestBindingExtensionsInTarget()
        {
            _app.Type(GetType()).TestBindingExtensionsInTarget(_ctrl, _ctrl._button1, _ctrl._textBox);
        }

        static void TestBindingExtensionsInTarget(SearchTestControl ctrl, Button button1, TextBox textBox)
        {
            DependencyObject b = ctrl.LogicalTree().ByBinding("Button1Command").Single();
            Assert.AreEqual(button1, b);

            var collection = ctrl.LogicalTree().ByBinding("DataText");
            Assert.AreEqual(2, collection.Count());

            var t = ctrl.LogicalTree().ByBinding("DataText", ctrl.DataContext).Single();
            Assert.AreEqual(textBox, t);
        }

        [TestMethod]
        public void TestType()
        {
            AppVar target = _ctrl;
            var collection = Searcher.ByType(TreeUtility.LogicalTree(target), typeof(Button).FullName);
            Assert.AreEqual(2, collection.Count);
            collection = Searcher.ByType<Button>(TreeUtility.LogicalTree(target));
            Assert.AreEqual(2, collection.Count);
            collection = Searcher.ByType<ButtonBase>(TreeUtility.LogicalTree(target));
            Assert.AreEqual(3, collection.Count);

            var list = Searcher.ByType(TreeUtility.LogicalTree(target), typeof(ListView).FullName).Single();
            Assert.AreEqual(list, _ctrl._listView);
            list = Searcher.ByType<ListView>(TreeUtility.LogicalTree(target)).Single();
            Assert.AreEqual(list, _ctrl._listView);
        }
        
        [TestMethod]
        public void TestTypeInTarget()
        {
            _app.Type(GetType()).TestTypeInTarget(_ctrl, _ctrl._listView);
        }

        static void TestTypeInTarget(SearchTestControl ctrl, ListView listView)
        {
            var collection = SearcherInTarget.ByType(TreeUtilityInTarget.LogicalTree(ctrl), typeof(Button).FullName);
            Assert.AreEqual(2, collection.Count());
            collection = SearcherInTarget.ByType<Button>(TreeUtilityInTarget.LogicalTree(ctrl));
            Assert.AreEqual(2, collection.Count());
            collection = SearcherInTarget.ByType<ButtonBase>(TreeUtilityInTarget.LogicalTree(ctrl));
            Assert.AreEqual(3, collection.Count());

            var list = SearcherInTarget.ByType(TreeUtilityInTarget.LogicalTree(ctrl), typeof(ListView).FullName).Single();
            Assert.AreEqual(list, listView);
            list = SearcherInTarget.ByType<ListView>(TreeUtilityInTarget.LogicalTree(ctrl)).Single();
            Assert.AreEqual(list, listView);
        }

        [TestMethod]
        public void TestTypeExtensions()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType(typeof(Button).FullName);
            Assert.AreEqual(2, collection.Count);
            collection = target.LogicalTree().ByType<Button>();
            Assert.AreEqual(2, collection.Count);
            collection = target.LogicalTree().ByType<ButtonBase>();
            Assert.AreEqual(3, collection.Count);

            var list = target.LogicalTree().ByType(typeof(ListView).FullName).Single();
            Assert.AreEqual(list, _ctrl._listView);
            list = target.LogicalTree().ByType<ListView>().Single();
            Assert.AreEqual(list, _ctrl._listView);
        }

        [TestMethod]
        public void TestTypeExtensionsInTarget()
        {
            _app.Type(GetType()).TestTypeExtensionsInTarget(_ctrl, _ctrl._listView);
        }

        static void TestTypeExtensionsInTarget(SearchTestControl ctrl, ListView listView)
        {
            var collection = ctrl.LogicalTree().ByType(typeof(Button).FullName);
            Assert.AreEqual(2, collection.Count());
            collection = ctrl.LogicalTree().ByType<Button>();
            Assert.AreEqual(2, collection.Count());
            collection = ctrl.LogicalTree().ByType<ButtonBase>();
            Assert.AreEqual(3, collection.Count());
            var list = ctrl.LogicalTree().ByType(typeof(ListView).FullName).Single();
            Assert.AreEqual(list, listView);
            list = ctrl.LogicalTree().ByType <ListView>().Single();
            Assert.AreEqual(list, listView);
        }

        [TestMethod]
        public void TestContentTextExtensions()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType<ContentControl>();
            string name = collection.ByContentText("abc").Single().Dynamic().GetType().Name;
            Assert.AreEqual("CheckBox", name);
        }

        [TestMethod]
        public void TestContentTextExtensionsInTarget()
        {
            string name = _app.Type(GetType()).TestContentTextExtensionsInTarget(_ctrl);
            Assert.AreEqual("CheckBox", name);
        }

        static string TestContentTextExtensionsInTarget(SearchTestControl ctrl)
            => ctrl.LogicalTree().ByType<ContentControl>().ByContentText("abc").Single().GetType().Name;

        [TestMethod]
        public void TestTextExtensions()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType<TextBlock>();
            string name = collection.ByText("AAA").Single().Dynamic().Name;
            Assert.AreEqual("_textBlockA", name);
        }

        [TestMethod]
        public void TestTextExtensionsInTarget()
        {
            string name = _app.Type(GetType()).TestTextExtensionsInTarget(_ctrl);
            Assert.AreEqual("_textBlockA", name);
        }

        static string TestTextExtensionsInTarget(SearchTestControl ctrl)
            => ctrl.LogicalTree().ByType<TextBlock>().ByText("AAA").Single().Name;


        [TestMethod]
        public void TestNameTextExtensions()
        {
            AppVar target = _ctrl;
            var collection = target.LogicalTree().ByType<FrameworkElement>();
            string name = collection.ByName("_listView").Single().Dynamic().Name;
            Assert.AreEqual("_listView", name);
        }

        [TestMethod]
        public void TestNameTextExtensionsInTarget()
        {
            string name = _app.Type(GetType()).TestNameTextExtensionsInTarget(_ctrl);
            Assert.AreEqual("_listView", name);
        }

        static string TestNameTextExtensionsInTarget(SearchTestControl ctrl)
            => ctrl.LogicalTree().ByType<FrameworkElement>().ByName("_listView").Single().Name;
    }
}
