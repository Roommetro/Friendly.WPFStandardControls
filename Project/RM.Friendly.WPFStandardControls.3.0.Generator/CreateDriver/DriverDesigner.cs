using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public class DriverDesigner : IDriverDesigner
    {
        public int Priority { get; }

        public bool CanDesign(object obj) => obj is Window || obj is UserControl || obj is Page;

        public string CreateDriverClassName(object coreObj)
        {
            var driverTypeNameManager = new DriverTypeNameManager(DriverCreatorAdapter.SelectedNamespace, DriverCreatorAdapter.TypeFullNameAndWindowDriver, DriverCreatorAdapter.TypeFullNameAndUserControlDriver);
            return driverTypeNameManager.MakeDriverType(coreObj, out var _);
        }

        public DriverIdentifyInfo[] GetIdentifyingCandidates(object root, object element)
        {
            var rootCtrl = root as DependencyObject;
            var elementCtrl = element as DependencyObject;
            if (rootCtrl == null || elementCtrl == null) return new DriverIdentifyInfo[0];

            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                var infos = GetIdentifyingCandidatesCore(dom, rootCtrl, elementCtrl);
                if (infos != null) return infos;
            }

            return new DriverIdentifyInfo[0];
        }

        public void GenerateCode(object targetControl, DriverDesignInfo info)
        {
            var driverTypeNameManager = new DriverTypeNameManager(DriverCreatorAdapter.SelectedNamespace, DriverCreatorAdapter.TypeFullNameAndWindowDriver, DriverCreatorAdapter.TypeFullNameAndUserControlDriver);
            var root = FindRoot((UIElement)targetControl, driverTypeNameManager);
            GetMembers(info, out var usings, out var members);
            var fileName = $"{info.ClassName}.cs";

            var getFromControlTreeOnly = new List<Type>();
            if (info.CreateAttachCode)
            {
                getFromControlTreeOnly.Add(targetControl.GetType());
            }

            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                var creator = new WPFDriverCreator(dom);
                var code = creator.GenerateCode(root, (Control)targetControl, DriverCreatorAdapter.SelectedNamespace, info.ClassName, usings, members, getFromControlTreeOnly);

                DriverCreatorAdapter.AddCode(fileName, code, targetControl);
            }

            //選択のための情報を設定
            foreach (var e in info.Properties)
            {
                DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, e.Identify, e.Element);
            }
        }

        static void GetMembers(DriverDesignInfo info, out List<string> usings, out List<string> members)
        {
            usings = new List<string>();
            members = new List<string>();
            var fileName = $"{info.ClassName}.cs";
            foreach (var e in info.Properties)
            {
                var typeName = DriverCreatorUtils.GetTypeName(e.TypeFullName);
                var nameSpace = DriverCreatorUtils.GetTypeNamespace(e.TypeFullName);
                var todo = (e.IsPerfect.HasValue && !e.IsPerfect.Value) ? WPFDriverCreator.TodoComment : string.Empty;
                members.Add($"public {typeName} {e.Name} => {e.Identify}; {todo}");
                if (!usings.Contains(nameSpace)) usings.Add(nameSpace);
                foreach (var x in e.ExtensionUsingNamespaces)
                {
                    if (!usings.Contains(x)) usings.Add(x);
                }
            }
        }

        static UIElement FindRoot(UIElement targetControl, DriverTypeNameManager driverTypeNameManager)
        {
            var ctrl = VisualTreeHelper.GetParent(targetControl) as UIElement;
            if (ctrl == null) return targetControl;

            while (true)
            {
                if (DriverCreatorAdapter.TypeFullNameAndUserControlDriver.ContainsKey(ctrl.GetType().FullName)) return ctrl;
                if (DriverCreatorAdapter.TypeFullNameAndWindowDriver.ContainsKey(ctrl.GetType().FullName)) return ctrl;
                var parent = VisualTreeHelper.GetParent(ctrl) as UIElement;
                if (parent == null) return ctrl;
                ctrl = parent;
            }
        }

        DriverIdentifyInfo[] GetIdentifyingCandidatesCore(CodeDomProvider dom, DependencyObject rootCtrl, DependencyObject elementCtrl)
        {
            var creator = new WPFDriverCreator(dom);

            var ancestor = new List<DependencyObject>();
            var current = VisualTreeHelper.GetParent(elementCtrl);
            while (current != null)
            {
                if (CanDesign(current))
                {
                    ancestor.Add(current);
                }
                if (ReferenceEquals(current, rootCtrl)) break;
                current = VisualTreeHelper.GetParent(current);
            }

            //Fieldでたどることができる範囲を取得
            var target = elementCtrl;
            var accessPaths = new List<string>();
            var bindingExpressionCache = new BindingExpressionCache();
            var isPerfect = true;
            string name = string.Empty;
            var usings = new List<string>();
            var needDynamic = true;
            foreach (var e in ancestor)
            {
                //直接のフィールドに持っているか？
                var path = GetAccessPath(e, target);
                if (!string.IsNullOrEmpty(path))
                {
                    if (target == elementCtrl)
                    {
                        var sp = path.Split('.');
                        name = sp.Length == 0 ? string.Empty : sp[sp.Length - 1];
                    }
                    accessPaths.Insert(0, path);
                    target = e;
                    needDynamic = true;
                    continue;
                }

                //Treeから検索
                var logicalForGetter = WPFUtility.GetLogicalTreeDescendants(e, false, false, 0);
                var visualForGetter = WPFUtility.GetVisualTreeDescendants(e, false, false, 0);
                path = creator.MakeCodeGetFromTree(string.Empty, logicalForGetter, visualForGetter, target, bindingExpressionCache, usings, out var nogood);
                if (!needDynamic)
                {
                    var toDynamic = ".Dynamic()";
                    var index = path.LastIndexOf(toDynamic);
                    if (index == path.Length - toDynamic.Length)
                    {
                        path = path.Substring(0, path.Length - toDynamic.Length);
                    }
                }

                if (nogood) isPerfect = false;
                if (!string.IsNullOrEmpty(path))
                {
                    accessPaths.Insert(0, path);
                    target = e;
                    needDynamic = false;
                    continue;
                }

                break;
            }

            if (target != rootCtrl) return null;

            if (string.IsNullOrEmpty(name))
            {
                var names = new List<string>();
                var customNameGenerator = new DriverElementNameGeneratorAdaptor(dom);
                name = customNameGenerator.MakeDriverPropName(elementCtrl, string.Empty, names);
            }

            if (needDynamic)
            {
                accessPaths.Insert(0, "Dynamic()");
            }
            var accessPath = string.Join(".", accessPaths.ToArray());
            return new[]
            {
                new DriverIdentifyInfo
                {
                    IsPerfect = isPerfect,
                    Identify = "Core." + accessPath,
                    DefaultName = name,
                    ExtensionUsingNamespaces = usings.ToArray()
                }
            };
        }

        static string GetAccessPath(DependencyObject parent, DependencyObject target)
        {
            foreach (var e in parent.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (ReferenceEquals(e.GetValue(parent), target)) return e.Name;
            }
            return string.Empty;
        }
    }
}
