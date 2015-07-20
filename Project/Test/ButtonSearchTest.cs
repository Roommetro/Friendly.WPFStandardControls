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
using System.Windows.Input;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class ButtonSearchTest
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
            _ctrl = _app.Type(GetType()).Init(main._grid);
        }

        static ButtonSearchTestControl Init(Grid grid)
        {
            ButtonSearchTestControl ctrl = new ButtonSearchTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void TestByCommand()
        {
            var buttons = TreeUtility.LogicalTree((AppVar)_ctrl).ByType<Button>();
            AppVar button2 = ButtonSearcher.ByCommand(buttons, ApplicationCommands.Close).Single();
            Assert.AreEqual(button2, _ctrl._button2);
            button2 = ButtonSearcher.ByCommand(buttons, typeof(ApplicationCommands).FullName, "Close").Single();
            Assert.AreEqual(button2, _ctrl._button2);
        }

        [TestMethod]
        public void TestByCommandExtensions()
        {
            var buttons = ((AppVar)_ctrl).LogicalTree().ByType<Button>();
            AppVar button2 = buttons.ByCommand(ApplicationCommands.Close).Single();
            Assert.AreEqual(button2, _ctrl._button2);
            button2 = buttons.ByCommand(typeof(ApplicationCommands).FullName, "Close").Single();
            Assert.AreEqual(button2, _ctrl._button2);
        }

        [TestMethod]
        public void TestByCommandInTarget()
        {
            _app.Type(GetType()).TestByCommandInTarget(_ctrl, _ctrl._button2);
        }

        static void TestByCommandInTarget(ButtonSearchTestControl ctrl, Button button2Expected)
        {
            var buttons = TreeUtilityInTarget.LogicalTree(ctrl).ByType<Button>();
            var button2 = ButtonSearcherInTarget.ByCommand(buttons, ApplicationCommands.Close).Single();
            Assert.AreEqual(button2, button2Expected);
        }

        [TestMethod]
        public void TestByCommandInTargetExtensions()
        {
            _app.Type(GetType()).TestByCommandInTargetExtensions(_ctrl, _ctrl._button2);
        }

        static void TestByCommandInTargetExtensions(ButtonSearchTestControl ctrl, Button button2Expected)
        {
            var buttons = ctrl.LogicalTree().ByType<Button>();
            var button2 = buttons.ByCommand(ApplicationCommands.Close).Single();
            Assert.AreEqual(button2, button2Expected);
        }

        [TestMethod]
        public void TestByCommandParameter()
        {
            var buttons = TreeUtility.LogicalTree((AppVar)_ctrl).ByType<Button>();
            AppVar button1 = ButtonSearcher.ByCommandParameter(buttons, "button1").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            button1 = ButtonSearcher.ByCommandParameter(buttons, new ExplicitAppVar(_app.Copy("button1"))).Single();
            Assert.AreEqual(button1, _ctrl._button1);
        }

        [TestMethod]
        public void TestByCommandParameterExtensions()
        {
            var buttons = ((AppVar)_ctrl).LogicalTree().ByType<Button>();
            AppVar button1 = buttons.ByCommandParameter("button1").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            button1 = buttons.ByCommandParameter(new ExplicitAppVar(_app.Copy("button1"))).Single();
            Assert.AreEqual(button1, _ctrl._button1);
        }

        [TestMethod]
        public void TestByCommandParameterInTarget()
        {
            _app.Type(GetType()).TestByCommandParameterInTarget(_ctrl, _ctrl._button3);
        }

        static void TestByCommandParameterInTarget(ButtonSearchTestControl ctrl, Button button3Expected)
        {
            var buttons = TreeUtilityInTarget.LogicalTree(ctrl).ByType<Button>();
            var button3 = ButtonSearcherInTarget.ByCommandParameter(buttons, "button3").Single();
            Assert.AreEqual(button3, button3Expected);
        }

        [TestMethod]
        public void TestByCommandParameterInTargetExtensions()
        {
            _app.Type(GetType()).TestByCommandParameterInTargetExtensions(_ctrl, _ctrl._button3);
        }

        static void TestByCommandParameterInTargetExtensions(ButtonSearchTestControl ctrl, Button button3Expected)
        {
            var buttons = ctrl.LogicalTree().ByType<Button>();
            var button3 = buttons.ByCommandParameter("button3").Single();
            Assert.AreEqual(button3, button3Expected);
        }
    }
}
