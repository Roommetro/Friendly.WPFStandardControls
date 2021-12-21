using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class VisualTreeWithPopupTest
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
            _ctrl = _app.Type<VisualTreeWithPopupTest>().Init(main._grid);
        }

        static VisualTreeWithPopupTestControl Init(Grid grid)
        {
            VisualTreeWithPopupTestControl ctrl = new VisualTreeWithPopupTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestVisualTree()
        {
            AppVar button = null;
            try
            {
                button = Searcher.ByBinding(TreeUtility.VisualTree(_ctrl), "Button2Command");
            }
            catch { }
            Assert.AreEqual(button, null);
            
            button = Searcher.ByBinding(TreeUtility.VisualTreeWithPopup(_ctrl), "Button2Command").Single();
            Assert.AreEqual(button, _ctrl._button2);
        
            Assert.AreEqual(1, TreeUtility.VisualTree((AppVar)_ctrl).ByType<Button>().Count);
            Assert.AreEqual(2, TreeUtility.VisualTreeWithPopup((AppVar)_ctrl).ByType<Button>().Count);
        }

        [TestMethod]
        public void TestVisualTreeInTarget()
        {
            _app.Type(GetType()).TestVisualTreeInTarget(_ctrl, _ctrl._button2);
        }

        static void TestVisualTreeInTarget(VisualTreeWithPopupTestControl ctrl, Button button2)
        {
            var b = SearcherInTarget.ByBinding(TreeUtilityInTarget.VisualTreeWithPopup(ctrl), "Button2Command").Single();
            Assert.AreEqual(button2, b);
            Assert.AreEqual(2, TreeUtilityInTarget.VisualTreeWithPopup(ctrl).ByType<Button>().Count());
        }

        [TestMethod]
        public void TestVisualTreeExtensions()
        {
            AppVar button2 = ((AppVar)_ctrl).VisualTreeWithPopup().ByBinding("Button2Command").Single();
            Assert.AreEqual(button2, _ctrl._button2);
            Assert.AreEqual(2, ((AppVar)_ctrl).VisualTreeWithPopup().ByType<Button>().Count);
        }

        class MyAppVar : IAppVarOwner
        {
            public AppVar AppVar { get; set; }
        }

        [TestMethod]
        public void TestVisualTreeExtensionsForIAppVarOwner()
        {
            AppVar button2 = new MyAppVar() { AppVar = _ctrl }.VisualTreeWithPopup().ByBinding("Button2Command").Single();
            Assert.AreEqual(button2, _ctrl._button2);
            Assert.AreEqual(2, new WPFListView(_ctrl).VisualTreeWithPopup().ByType<Button>().Count);
        }

        [TestMethod]
        public void TestVisualTreeInTargetExtensions()
        {
            _app.Type(GetType()).TestVisualTreeInTargetExtensions(_ctrl, _ctrl._button2);
        }

        static void TestVisualTreeInTargetExtensions(VisualTreeWithPopupTestControl ctrl, Button button2)
        {
            var b = ctrl.VisualTreeWithPopup().ByBinding("Button2Command").Single();
            Assert.AreEqual(button2, b);
            Assert.AreEqual(2, ctrl.VisualTreeWithPopup().ByType<Button>().Count());
        }
    }
}
