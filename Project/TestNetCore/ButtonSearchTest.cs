using System;

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
    
    public class ButtonSearchTest
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
            _ctrl = _app.Type(GetType()).Init(main._grid);
        }

        static ButtonSearchTestControl Init(Grid grid)
        {
            ButtonSearchTestControl ctrl = new ButtonSearchTestControl();
            grid.Children.Add(ctrl);
            return ctrl;
        }

        [TearDown]
        public void TearDown()
        {
            Process.GetProcessById(_app.ProcessId).CloseMainWindow();
        }

        [Test]
        public void TestByCommand()
        {
            var buttons = TreeUtility.LogicalTree((AppVar)_ctrl).ByType<Button>();
            AppVar button2 = ButtonSearcher.ByCommand(buttons, ApplicationCommands.Close).Single();
            Assert.AreEqual(button2, _ctrl._button2);
            button2 = ButtonSearcher.ByCommand(buttons, typeof(ApplicationCommands).FullName, "Close").Single();
            Assert.AreEqual(button2, _ctrl._button2);
        }

        [Test]
        public void TestByCommandExtensions()
        {
            var buttons = ((AppVar)_ctrl).LogicalTree().ByType<Button>();
            AppVar button2 = buttons.ByCommand(ApplicationCommands.Close).Single();
            Assert.AreEqual(button2, _ctrl._button2);
            button2 = buttons.ByCommand(typeof(ApplicationCommands).FullName, "Close").Single();
            Assert.AreEqual(button2, _ctrl._button2);
        }

        [Test]
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

        [Test]
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

        [Test]
        public void TestByCommandParameter()
        {
            var buttons = TreeUtility.LogicalTree((AppVar)_ctrl).ByType<Button>();
            AppVar button1 = ButtonSearcher.ByCommandParameter(buttons, "button1").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            button1 = ButtonSearcher.ByCommandParameter(buttons, new ExplicitAppVar(_app.Copy("button1"))).Single();
            Assert.AreEqual(button1, _ctrl._button1);
        }

        [Test]
        public void TestByCommandParameterExtensions()
        {
            var buttons = ((AppVar)_ctrl).LogicalTree().ByType<Button>();
            AppVar button1 = buttons.ByCommandParameter("button1").Single();
            Assert.AreEqual(button1, _ctrl._button1);
            button1 = buttons.ByCommandParameter(new ExplicitAppVar(_app.Copy("button1"))).Single();
            Assert.AreEqual(button1, _ctrl._button1);
        }

        [Test]
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

        [Test]
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

        [Test]
        public void TestByCommandParameterEnum()
        {
            var buttons = TreeUtility.LogicalTree((AppVar)_ctrl).ByType<Button>();
            AppVar button4 = ButtonSearcher.ByCommandParameter(buttons, CommandPrameterType.A).Single();
            Assert.AreEqual(button4, _ctrl._button4);
            button4 = ButtonSearcher.ByCommandParameter(buttons, new ExplicitAppVar(_app.Copy(CommandPrameterType.A))).Single();
            Assert.AreEqual(button4, _ctrl._button4);
        }

        [Test]
        public void TestByCommandParameterEnumExtensions()
        {
            var buttons = ((AppVar)_ctrl).LogicalTree().ByType<Button>();
            AppVar button4 = buttons.ByCommandParameter(CommandPrameterType.A).Single();
            Assert.AreEqual(button4, _ctrl._button4);
            button4 = buttons.ByCommandParameter(new ExplicitAppVar(_app.Copy(CommandPrameterType.A))).Single();
            Assert.AreEqual(button4, _ctrl._button4);
        }


        [Test]
        public void TestByCommandParameterEnumInTarget()
        {
            _app.Type(GetType()).TestByCommandParameterEnumInTarget(_ctrl, _ctrl._button4);
        }

        static void TestByCommandParameterEnumInTarget(ButtonSearchTestControl ctrl, Button button4Expected)
        {
            var buttons = TreeUtilityInTarget.LogicalTree(ctrl).ByType<Button>();
            var button4 = ButtonSearcherInTarget.ByCommandParameter(buttons, CommandPrameterType.A).Single();
            Assert.AreEqual(button4, button4Expected);
        }

        [Test]
        public void TestByCommandParameterEnumInTargetExtensions()
        {
            _app.Type(GetType()).TestByCommandParameterEnumInTargetExtensions(_ctrl, _ctrl._button4);
        }

        static void TestByCommandParameterEnumInTargetExtensions(ButtonSearchTestControl ctrl, Button button4Expected)
        {
            var buttons = ctrl.LogicalTree().ByType<Button>();
            var button4 = buttons.ByCommandParameter(CommandPrameterType.A).Single();
            Assert.AreEqual(button4, button4Expected);
        }

        [Test]
        public void TestByCommandParameterText()
        {
            var buttons = TreeUtility.LogicalTree((AppVar)_ctrl).ByType<Button>();
            AppVar button4 = ButtonSearcher.ByCommandParameterText(buttons, "A").Single();
            Assert.AreEqual(button4, _ctrl._button4);
        }

        [Test]
        public void TestByCommandParameterTextExtensions()
        {
            var buttons = ((AppVar)_ctrl).LogicalTree().ByType<Button>();
            AppVar button4 = buttons.ByCommandParameterText("A").Single();
            Assert.AreEqual(button4, _ctrl._button4);
        }

        [Test]
        public void TestByCommandParameterTextInTarget()
        {
            _app.Type(GetType()).TestByCommandParameterTextInTarget(_ctrl, _ctrl._button4);
        }


        static void TestByCommandParameterTextInTarget(ButtonSearchTestControl ctrl, Button button4Expected)
        {
            var buttons = TreeUtilityInTarget.LogicalTree(ctrl).ByType<Button>();
            var button4 = ButtonSearcherInTarget.ByCommandParameterText(buttons, "A").Single();
            Assert.AreEqual(button4, button4Expected);
        }

        [Test]
        public void TestByCommandParameterTextInTargetExtensions()
        {
            _app.Type(GetType()).TestByCommandParameterTextInTargetExtensions(_ctrl, _ctrl._button4);
        }

        static void TestByCommandParameterTextInTargetExtensions(ButtonSearchTestControl ctrl, Button button4Expected)
        {
            var buttons = ctrl.LogicalTree().ByType<Button>();
            var button4 = buttons.ByCommandParameterText("A").Single();
            Assert.AreEqual(button4, button4Expected);
        }

        [Test]
        public void TestByIsCancel()
        {
            var buttons = TreeUtility.LogicalTree((AppVar)_ctrl).ByType<Button>();
            AppVar button5 = ButtonSearcher.ByIsCancel(buttons).Single();
            Assert.AreEqual(button5, _ctrl._button5);
        }

        [Test]
        public void TestByIsCancelExtensions()
        {
            var buttons = ((AppVar)_ctrl).LogicalTree().ByType<Button>();
            AppVar button5 = buttons.ByIsCancel().Single();
            Assert.AreEqual(button5, _ctrl._button5);
        }

        [Test]
        public void TestByIsCancelInTarget()
        {
            _app.Type(GetType()).TestByIsCancelInTarget(_ctrl, _ctrl._button5);
        }

        static void TestByIsCancelInTarget(ButtonSearchTestControl ctrl, Button button5Expected)
        {
            var buttons = TreeUtilityInTarget.LogicalTree(ctrl).ByType<Button>();
            var button5 = ButtonSearcherInTarget.ByIsCancel(buttons).Single();
            Assert.AreEqual(button5, button5Expected);
        }

        [Test]
        public void TestByIsCancelInTargetExtensions()
        {
            _app.Type(GetType()).TestByIsCancelInTargetExtensions(_ctrl, _ctrl._button5);
        }

        static void TestByIsCancelInTargetExtensions(ButtonSearchTestControl ctrl, Button button5Expected)
        {
            var buttons = ctrl.LogicalTree().ByType<Button>();
            var button5 = buttons.ByIsCancel().Single();
            Assert.AreEqual(button5, button5Expected);
        }
    }
}
