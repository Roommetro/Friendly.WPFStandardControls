using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Codeer.TestAssistant.GeneratorToolKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;
using Target.CreateDriverTarget;

namespace Test.GeneratorTest
{
    [TestClass]
    public class CreateDriverTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Assembly.Load(typeof(IWPFControlIdentifyCodeGenerator).Assembly.GetName());

            DriverCreatorAdapter.TypeFullNameAndControlDriver.Clear();
            var targets = new[] { typeof(WPFStandardControls_3), typeof(WPFStandardControls_3_5), typeof(WPFStandardControls_4) };
            foreach (var type in targets.SelectMany(t => t.Assembly.GetTypes()))
            {
                //属性をチェック
                foreach (var attr in type.GetCustomAttributes(false).OfType<ControlDriverAttribute>())
                {
                    //コントロールドライバ
                    DriverCreatorAdapter.TypeFullNameAndControlDriver.Add(
                        attr.TypeFullName,
                        new ControlDriverInfo
                        {
                            SearchDescendantUserControls = attr.SearchDescendantUserControls,
                            ControlDriverTypeFullName = type.FullName,
                            DriverMappingEnabled = attr.DriverMappingEnabled
                        });
                }
            }
            // プロパティに値を設定
            var driverCreatorAdapterType = typeof(DriverCreatorAdapter);
            driverCreatorAdapterType.InvokeMember("SetSelectedNamespace", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[] { "TestCode" });
            driverCreatorAdapterType.InvokeMember("SetClientProjectExtension", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[] { ".csproj" });
        }

        [TestMethod]
        public void SingleWindowTest()
        {
            var expected = new Dictionary<string, string>
            {
                ["SingleWindow_Driver.cs"] = Properties.Resources.SingleWindow_Driver
            };
            TestCore<SingleWindow>(expected);
        }

        [TestMethod]
        public void UserControlWindowTest()
        {
            var expected = new Dictionary<string, string>
            {
                ["UserControlWindow_Driver.cs"] = Properties.Resources.UserControlWindow_Driver,
                ["UserControl1_Driver.cs"] = Properties.Resources.UserControl1_Driver
            };
            TestCore<UserControlWindow>(expected);
        }

        [TestMethod]
        public void TabUserControlWindowTest()
        {
            var expected = new Dictionary<string, string>
            {
                ["TabUserControlWindow_Driver.cs"] = Properties.Resources.TabUserControlWindow_Driver,
                ["UserControl2_Driver.cs"] = Properties.Resources.UserControl2_Driver,
                ["UserControl1_Driver.cs"] = Properties.Resources.NestedUserControl1_Driver
            };
            TestCore<TabUserControlWindow>(expected);
        }

        /// <summary>
        /// テスト実行の本体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        private void TestCore<T>(IDictionary<string, string> expected) where T : Window, new()
        {
            // テスト用のフォームを作成してコード生成
            new T().ShowDialog();
            // 生成したコードを取得して正しいかチェック
            var actual = GetCode();
            AreEqual(expected, actual);
        }

        /// <summary>
        /// 生成したコードを取得する
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> GetCode()
        {
            var codes = new Dictionary<string, string>();
            var result = typeof(DriverCreatorAdapter).InvokeMember("PopFiles", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[0]);
            foreach (var item in (dynamic)result)
            {
                var key = (string)GetProperty(item, "Key").GetValue(item);
                var value = GetProperty(item, "Value").GetValue(item);
                var code = (string)GetProperty(value, "Code").GetValue(value);
                codes.Add(key, code);
            }
            return codes;
        }

        /// <summary>
        /// 指定されたプロパティ情報を取得する
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private PropertyInfo GetProperty(object source, string propertyName)
        {
            var info = source.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (info == null) throw new InvalidOperationException($"Property [{propertyName}] is not found.");
            return info;
        }

        /// <summary>
        /// 指定されたDictionaryが同じか調べる
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        private void AreEqual(IDictionary<string, string> expected, IDictionary<string, string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var expectedItem in expected)
            {
                Assert.IsTrue(actual.TryGetValue(expectedItem.Key, out var code));
                Assert.AreEqual(expectedItem.Value, code);
            }
        }
    }
}
