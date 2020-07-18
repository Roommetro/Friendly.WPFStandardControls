using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public class DriverDesigner : IDriverDesigner
    {
        const string Indent = "    ";
        const string TodoComment = "// TODO It is not the best way to identify. Please change to a better method.";
        const string WindowsAppFriendTypeFullName = "Codeer.Friendly.Windows.WindowsAppFriend";
        const string AttachByTypeFullName = "Type Full Name";
        const string AttachByWindowText = "Window Text";
        const string AttachVariableWindowText = "VariableWindowText";
        const string AttachCustom = "Custom";

        readonly WPFCustomIdentify _customIdentify = new WPFCustomIdentify();

        public int Priority { get; }

        public bool CanDesign(object obj) => obj is UIElement;

        static bool CanBeParent(object obj) => obj is Window || obj is UserControl || obj is Page;

        public string CreateDriverClassName(object coreObj)
        {
            var driverTypeNameManager = new DriverTypeNameManager(DriverCreatorAdapter.SelectedNamespace, DriverCreatorAdapter.TypeFullNameAndWindowDriver, DriverCreatorAdapter.TypeFullNameAndUserControlDriver);
            return driverTypeNameManager.MakeDriverType(coreObj, out var _);
        }

        public string[] GetAttachExtensionClassCandidates(object obj)
        {
            var candidates = new List<string>();
            var parent = VisualTreeHelper.GetParent((DependencyObject)obj);
            while (parent != null)
            {
                var driver = DriverCreatorUtils.GetDriverTypeFullName(parent, new Dictionary<string, ControlDriverInfo>(),
                                                                    DriverCreatorAdapter.TypeFullNameAndUserControlDriver,
                                                                    DriverCreatorAdapter.TypeFullNameAndWindowDriver, out var _);
                if (!string.IsNullOrEmpty(driver))
                {
                    candidates.Add(driver);
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            candidates.Add(WindowsAppFriendTypeFullName);
            return candidates.ToArray();
        }

        public string[] GetAttachMethodCandidates(object obj)
        {
            var candidates = new List<string>();
            candidates.Add(AttachByTypeFullName);
            if (obj is Window)
            {
                candidates.Add(AttachByWindowText);
                candidates.Add(AttachVariableWindowText);
            }
            candidates.Add(AttachCustom);
            return candidates.ToArray();
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
            var code = GenerateCodeCore((Control)targetControl, info);
            var fileName = $"{info.ClassName}.cs";
            DriverCreatorAdapter.AddCode(fileName, code, targetControl);

            //行選択でのツリーとの連動用
            foreach (var e in info.Properties)
            {
                DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, e.Identify, e.Element);
            }
        }

        internal static void CreateControlDriver(UIElement root)
        {
            var driverName = root.GetType().Name + "Driver";
            var generatorName = driverName + "Generator";

            var driverCode = @"using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace [*namespace]
{
    [ControlDriver(TypeFullName = ""{typefullname}"", Priority = 2)]
    public class {driverName} : WPFUIElement
    {
        public {driverName}(AppVar appVar)
            : base(appVar) { }
    }
}
";
            DriverCreatorAdapter.AddCode($"{driverName}.cs", driverCode.Replace("{typefullname}", root.GetType().FullName).Replace("{driverName}", driverName), root);

            var generatorCode = @"using System;
using System.Windows;
using Codeer.TestAssistant.GeneratorToolKit;

namespace [*namespace]
{
    [CaptureCodeGenerator(""[*namespace.{driverName}]"")]
    public class {generatorName} : CaptureCodeGeneratorBase
    {
        UIElement _element;

        protected override void Attach()
        {
            _element = (UIElement)ControlObject;
        }

        protected override void Detach()
        {
        }
    }
}
";
            DriverCreatorAdapter.AddCode($"{generatorName}.cs", generatorCode.Replace("{generatorName}", generatorName).Replace("{driverName}", driverName), root);
        }

        static void GetMembers(DriverDesignInfo info, out List<string> usings, out List<string> members)
        {
            usings = new List<string>();
            members = new List<string>();
            var fileName = $"{info.ClassName}.cs";
            foreach (var e in info.Properties)
            {
                var typeName = SeparateNameSpaceAndTypeName(e.TypeFullName, usings);
                var todo = (e.IsPerfect.HasValue && !e.IsPerfect.Value) ? TodoComment : string.Empty;
                members.Add($"public {typeName} {e.Name} => {e.Identify}; {todo}");
                foreach (var x in e.ExtensionUsingNamespaces)
                {
                    if (!usings.Contains(x)) usings.Add(x);
                }
            }
        }

        string GenerateCodeCore(Control targetControl, DriverDesignInfo info)
        {
            //クラス定義部分
            var classDefine = GenerateClassDefine(targetControl, info, out var memberUsings);

            //拡張メソッド部分
            var extentionsDefine = GenerateExtensions(targetControl, info, out var extensionUsings);

            //using
            var usings = new List<string>();
            DistinctAddRange(new[]
                    {
                        "Codeer.TestAssistant.GeneratorToolKit",
                        "Codeer.Friendly.Windows.Grasp",
                        "Codeer.Friendly.Windows",
                        "Codeer.Friendly.Dynamic",
                        "Codeer.Friendly",
                        "System.Linq"
                    }, usings);
            DistinctAddRange(memberUsings, usings);
            DistinctAddRange(extensionUsings, usings);
            usings.Sort();

            //コード作成
            var code = new List<string>();
            foreach (var e in usings)
            {
                code.Add($"using {e};");
            }
            code.Add(string.Empty);
            code.Add($"namespace {DriverCreatorAdapter.SelectedNamespace}");
            code.Add("{");
            code.AddRange(classDefine);
            code.AddRange(extentionsDefine);
            code.Add("}");
            return string.Join(Environment.NewLine, code.ToArray());
        }

        static List<string> GenerateClassDefine(object targetControl, DriverDesignInfo info, out List<string> usings)
        {
            GetMembers(info, out usings, out var members);

            var code = new List<string>();

            var attr = (targetControl is Window) ? "WindowDriver" : "UserControlDriver";
            code.Add($"{Indent}[{attr}(TypeFullName = \"{targetControl.GetType().FullName}\")]");
            code.Add($"{Indent}public class {info.ClassName}");
            code.Add($"{Indent}{{");

            if (targetControl is Window)
            {
                code.Add($"{Indent}{Indent}public WindowControl Core {{ get; }}");
                foreach (var e in members)
                {
                    code.Add($"{Indent}{Indent}{e}");
                }
                code.Add(string.Empty);
                code.Add($"{Indent}{Indent}public {info.ClassName}(WindowControl core)");
                code.Add($"{Indent}{Indent}{{");
                code.Add($"{Indent}{Indent}{Indent}Core = core;");
                code.Add($"{Indent}{Indent}}}");
                code.Add(string.Empty);
                code.Add($"{Indent}{Indent}public {info.ClassName}(AppVar core)");
                code.Add($"{Indent}{Indent}{{");
                code.Add($"{Indent}{Indent}{Indent}Core = new WindowControl(core);");
                code.Add($"{Indent}{Indent}}}");
            }
            else
            {
                code.Add($"{Indent}{Indent}public WPFUserControl Core {{ get; }}");
                foreach (var e in members)
                {
                    code.Add($"{Indent}{Indent}{e}");
                }
                code.Add(string.Empty);
                code.Add($"{Indent}{Indent}public {info.ClassName}(AppVar core)");
                code.Add($"{Indent}{Indent}{{");
                code.Add($"{Indent}{Indent}{Indent}Core = new WPFUserControl(core);");
                code.Add($"{Indent}{Indent}}}");
            }
            code.Add($"{Indent}}}");
            return code;
        }

        static List<string> GenerateExtensions(Control targetControl, DriverDesignInfo info, out List<string> usings)
        {
            var code = new List<string>();
            usings = new List<string>();

            if (!info.CreateAttachCode) return code;

            code.Add(string.Empty);
            code.Add($"{Indent}public static class {info.ClassName}Extensions");
            code.Add($"{Indent}{{");

            var funcName = GetFuncName(info.ClassName);

            //WindowsAppFriendにアタッチする場合
            if (info.AttachExtensionClass == WindowsAppFriendTypeFullName)
            {
                if (targetControl is Window window)
                {
                    if (info.AttachMethod == AttachCustom)
                    {
                        code.Add($"{Indent}{Indent}[WindowDriverIdentify(CustomMethod = \"TryGet\")]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, T identifier)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                        code.Add(string.Empty);
                        code.Add($"{Indent}{Indent}public static bool TryGet(WindowControl window, out T identifier)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                    }
                    else if (info.AttachMethod == AttachVariableWindowText)
                    {
                        code.Add($"{Indent}{Indent}[WindowDriverIdentify(CustomMethod = \"TryGet\")]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, string text)");
                        code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromWindowText(\"{window.Title}\").Dynamic();");
                        code.Add(string.Empty);
                        code.Add($"{Indent}{Indent}public static bool TryGet(WindowControl window, out string text)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}text = window.GetWindowText();");
                        code.Add($"{Indent}{Indent}{Indent}return window.TypeFullName == \"{targetControl.GetType().FullName}\";");
                        code.Add($"{Indent}{Indent}}}");
                    }
                    else
                    {
                        if (info.ManyExists)
                        {
                            if (info.AttachMethod == AttachByTypeFullName)
                            {
                                code.Add($"{Indent}{Indent}[WindowDriverIdentify(CustomMethod = \"TryGet\")]");
                                code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, int index)");
                                code.Add($"{Indent}{Indent}{Indent}=> app.GetFromTypeFullName(\"{targetControl.GetType().FullName}\")[index].Dynamic();");
                                code.Add(string.Empty);
                                code.Add($"{Indent}{Indent}public static bool TryGet(WindowControl window, out int index)");
                                code.Add($"{Indent}{Indent}{{");
                                code.Add($"{Indent}{Indent}{Indent}index = window.App.GetFromTypeFullName(\"{targetControl.GetType().FullName}\").Select(e => e.Handle).ToList().IndexOf(window.Handle);");
                                code.Add($"{Indent}{Indent}{Indent}return index != -1;");
                                code.Add($"{Indent}{Indent}}}");
                            }
                            else
                            {
                                code.Add($"{Indent}{Indent}[WindowDriverIdentify(CustomMethod = \"TryGet\")]");
                                code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, int index)");
                                code.Add($"{Indent}{Indent}{Indent}=> app.GetFromWindowText(\"{window.Title}\")[index].Dynamic();");
                                code.Add(string.Empty);
                                code.Add($"{Indent}{Indent}public static bool TryGet(WindowControl window, out int index)");
                                code.Add($"{Indent}{Indent}{{");
                                code.Add($"{Indent}{Indent}{Indent}index = window.App.GetFromWindowText(\"{window.Title}\").Select(e => e.Handle).ToList().IndexOf(window.Handle);");
                                code.Add($"{Indent}{Indent}{Indent}return index != -1;");
                                code.Add($"{Indent}{Indent}}}");
                            }
                        }
                        else
                        {
                            if (info.AttachMethod == AttachByTypeFullName)
                            {
                                code.Add($"{Indent}{Indent}[WindowDriverIdentify(TypeFullName = \"{targetControl.GetType().FullName}\")]");
                                code.Add($"{Indent}{Indent}public static {info.ClassName} {GetFuncName(info.ClassName)}(this WindowsAppFriend app)");
                                code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromTypeFullName(\"{targetControl.GetType().FullName}\").Dynamic();");
                            }
                            else
                            {
                                code.Add($"{Indent}{Indent}[WindowDriverIdentify(WindowText = \"{window.Title}\")]");
                                code.Add($"{Indent}{Indent}public static {info.ClassName} {GetFuncName(info.ClassName)}(this WindowsAppFriend app)");
                                code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromWindowText(\"{window.Title}\").Dynamic();");
                            }
                        }
                    }
                }
                //UserControl
                else
                {
                    if (info.AttachMethod == AttachCustom)
                    {
                        code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, T identifier)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                        code.Add(string.Empty);
                        code.Add($"{Indent}{Indent}public static void TryGet(this WindowsAppFriend app, out T[] identifiers)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                    }
                    else
                    {
                        if (info.ManyExists)
                        {
                            if (info.AttachMethod == AttachByTypeFullName)
                            {
                                code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                                code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, int index)");
                                code.Add($"{Indent}{Indent}{Indent}=> app.GetTopLevelWindows().SelectMany(e => e.GetFromTypeFullName(\"{targetControl.GetType().FullName}\")).ToArray()[index].Dynamic();");
                                code.Add(string.Empty);
                                code.Add($"{Indent}{Indent}public static void TryGet(this WindowsAppFriend app, out int[] indices)");
                                code.Add($"{Indent}{Indent}{Indent}=> indices = Enumerable.Range(0, app.GetTopLevelWindows().Sum(e => e.GetFromTypeFullName(\"{targetControl.GetType().FullName}\").Length)).ToArray();");
                            }
                        }
                        else
                        {
                            if (info.AttachMethod == AttachByTypeFullName)
                            {
                                code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                                code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                                code.Add($"{Indent}{Indent}{Indent}=> app.GetTopLevelWindows().SelectMany(e => e.GetFromTypeFullName(\"{targetControl.GetType().FullName}\")).SingleOrDefault()?.Dynamic();");
                            }
                        }
                    }
                }
            }
            //ドライバへのアタッチ
            else
            {
                SeparateNameSpaceAndTypeName(info.AttachExtensionClass, out var ns, out var parentDriver);
                if (!string.IsNullOrEmpty(ns))
                {
                    usings.Add(ns);
                }

                if (info.AttachMethod == AttachCustom)
                {
                    code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                    code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} parent, T identifier)");
                    code.Add($"{Indent}{Indent}{{");
                    code.Add($"{Indent}{Indent}{Indent}//TODO");
                    code.Add($"{Indent}{Indent}}}");
                    code.Add(string.Empty);
                    code.Add($"{Indent}{Indent}public static void TryGet(this {parentDriver} parent, out T identifier)");
                    code.Add($"{Indent}{Indent}{{");
                    code.Add($"{Indent}{Indent}{Indent}//TODO");
                    code.Add($"{Indent}{Indent}}}");
                }
                else
                {
                    if (info.ManyExists)
                    {
                        if (info.AttachMethod == AttachByTypeFullName)
                        {
                            code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} parent, int index)");
                            code.Add($"{Indent}{Indent}{Indent}=> parent.Core.VisualTree().ByType(\"{targetControl.GetType().FullName}\")[index].Dynamic();");
                            code.Add(string.Empty);
                            code.Add($"{Indent}{Indent}public static void TryGet(this {parentDriver} parent, out int[] indices)");
                            code.Add($"{Indent}{Indent}{Indent}=> indices = Enumerable.Range(0, parent.Core.VisualTree().ByType(\"{targetControl.GetType().FullName}\").Count).ToArray();");
                        }
                    }
                    else
                    {
                        if (info.AttachMethod == AttachByTypeFullName)
                        {
                            code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} parent)");
                            code.Add($"{Indent}{Indent}{Indent}=> parent.Core.VisualTree().ByType(\"{targetControl.GetType().FullName}\").SingleOrDefault()?.Dynamic();");
                        }
                    }
                }
            }
            code.Add($"{Indent}}}");

            return code;
        }

        static string SeparateNameSpaceAndTypeName(string typeFullNamme, List<string> usings)
        {
            var spGeneric = typeFullNamme.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            var typeParts = new List<string>();
            foreach (var y in spGeneric)
            {
                SeparateNameSpaceAndTypeName(y, out var ns, out var t);
                typeParts.Add(t);
                if (!string.IsNullOrEmpty(ns) && !usings.Contains(ns))
                {
                    usings.Add(ns);
                }
            }

            var typeDst = string.Join("<", typeParts.ToArray());

            for (int i = 0; i < typeParts.Count - 1; i++)
            {
                typeDst += ">";
            }
            return typeDst;
        }

        static void SeparateNameSpaceAndTypeName(string typeFullName, out string ns, out string type)
        {
            ns = string.Empty;
            type = typeFullName;

            var sp = typeFullName.Split('.');
            if (sp.Length < 2) return;

            type = sp[sp.Length - 1];
            var nsArray = new string[sp.Length - 1];
            Array.Copy(sp, nsArray, nsArray.Length);
            ns = string.Join(".", nsArray);
        }

        static string GetFuncName(string driverClassName)
        {
            var index = driverClassName.IndexOf(DriverCreatorUtils.Suffix);
            if (0 < index && index == driverClassName.Length - DriverCreatorUtils.Suffix.Length) return "Attach" + driverClassName;

            return $"Attach{driverClassName.Substring(0, driverClassName.Length - DriverCreatorUtils.Suffix.Length)}";
        }

        static void DistinctAddRange(IEnumerable<string> src, List<string> dst)
        {
            foreach (var e in src)
            {
                if (!dst.Contains(e)) dst.Add(e);
            }
        }

        DriverIdentifyInfo[] GetIdentifyingCandidatesCore(CodeDomProvider dom, DependencyObject rootCtrl, DependencyObject elementCtrl)
        {
            if (rootCtrl == elementCtrl) return new DriverIdentifyInfo[0];

            var ancestor = new List<DependencyObject>();
            var current = VisualTreeHelper.GetParent(elementCtrl);
            while (current != null)
            {
                if (CanBeParent(current))
                {
                    ancestor.Add(current);
                }
                if (ReferenceEquals(current, rootCtrl)) break;
                current = VisualTreeHelper.GetParent(current);
            }
            if (ancestor.Count == 0)
            {
                ancestor.Add(rootCtrl);
            }

            //Fieldでたどることができる範囲を取得
            var target = elementCtrl;
            var bindingExpressionCache = new BindingExpressionCache();
            var isPerfect = true;
            string name = string.Empty;
            var usings = new List<string>();
            var accessPaths = new List<string>();
            var isTree = new List<bool>();
            foreach (var e in ancestor)
            {
                //直接のフィールドに持っているか？
                var path = GetAccessPath(e, target);
                if (!string.IsNullOrEmpty(path))
                {
                    //最初がフィールドで特定できた場合はその名前を使う
                    if (target == elementCtrl)
                    {
                        var sp = path.Split('.');
                        name = sp.Length == 0 ? string.Empty : sp[sp.Length - 1];
                    }

                    accessPaths.Insert(0, path);
                    isTree.Insert(0, false);
                    target = e;
                }
                else
                {
                    //Treeから検索
                    var logicalForGetter = WPFUtility.GetLogicalTreeDescendants(e, false, false, 0);
                    var visualForGetter = WPFUtility.GetVisualTreeDescendants(e, false, false, 0);
                    path = MakeCodeGetFromTree(string.Empty, logicalForGetter, visualForGetter, target, bindingExpressionCache, usings, out var nogood);
                    if (string.IsNullOrEmpty(path)) return null;
                    if (nogood) isPerfect = false;
                    accessPaths.Insert(0, path);
                    isTree.Insert(0, true);
                    target = e;
                }
            }

            if (target != rootCtrl) return null;

            if (string.IsNullOrEmpty(name))
            {
                var names = new List<string>();
                var customNameGenerator = new DriverElementNameGeneratorAdaptor(dom);
                name = customNameGenerator.MakeDriverPropName(elementCtrl, string.Empty, names);
            }

            var appVarCast = string.Empty;
            bool modeDynamic = false;
            for (int i = 0; i < isTree.Count; i++)
            {
                if (isTree[i])
                {
                    if (modeDynamic)
                    {
                        if (0 < i)
                        {
                            appVarCast = "((AppVar)" + appVarCast;
                            accessPaths[i - 1] = accessPaths[i - 1] + ")";
                        }
                    }
                    modeDynamic = false;
                }
                else
                {
                    if (!modeDynamic)
                    {
                        accessPaths[i] = "Dynamic()." + accessPaths[i];
                    }
                    modeDynamic = true;
                }
            }
            var accessPath = string.Join(".", accessPaths.ToArray());
            if (!modeDynamic)
            {
                accessPath += ".Dynamic()";
            }

            return new[]
            {
                new DriverIdentifyInfo
                {
                    IsPerfect = isPerfect,
                    Identify = appVarCast + "Core." + accessPath,
                    DefaultName = name,
                    ExtensionUsingNamespaces = usings.ToArray(),
                    DriverTypeCandidates = GetDriverTypeCandidates(elementCtrl)
                }
            };
        }

        static string[] GetDriverTypeCandidates(DependencyObject elementCtrl)
            => DriverCreatorUtils.GetDriverTypeFullNames(elementCtrl, DriverCreatorAdapter.MultiTypeFullNameAndControlDriver, DriverCreatorAdapter.MultiTypeFullNameAndUserControlDriver, DriverCreatorAdapter.MultiTypeFullNameAndWindowDriver);

        static string GetAccessPath(DependencyObject parent, DependencyObject target)
        {
            foreach (var e in parent.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (ReferenceEquals(e.GetValue(parent), target)) return e.Name;
            }
            return string.Empty;
        }

        static bool Exist(List<DependencyObject> tree, DependencyObject obj)
        {
            foreach (var e in tree)
            {
                if (ReferenceEquals(e, obj)) return true;
            }
            return false;
        }

        static string TryIdentifyFromBinding(List<DependencyObject> tree, DependencyObject obj, BindingExpressionCache cache)
        {
            foreach (var e in cache.GetBindingExpression(obj))
            {
                var path = e.ParentBinding.Path.Path;
                if (string.IsNullOrEmpty(path)) continue;

                var matchExps = new List<BindingExpression>();
                bool objFind = false;
                foreach (var t in tree)
                {
                    if (t == obj) objFind = true;
                    var matchExp = WPFUtility.GetMatchBindingExpression(cache.GetBindingExpression(t), path);
                    if (matchExp != null) matchExps.Add(matchExp);
                }
                if (objFind && matchExps.Count == 1) return $".ByBinding(\"{path}\")";
            }
            return string.Empty;
        }

        string MakeCodeGetFromTree(string prefix, List<DependencyObject> logical, List<DependencyObject> visual, DependencyObject obj, BindingExpressionCache cache, List<string> usings, out bool nogood)
        {
            nogood = false;

            var preIdentify = prefix + "LogicalTree()";
            foreach (var tree in new[] { logical, visual })
            {
                if (Exist(tree, obj))
                {
                    //バインディングで特定できた
                    var identifyCode = TryIdentifyFromBinding(tree, obj, cache);
                    if (!string.IsNullOrEmpty(identifyCode))
                    {
                        return $"{preIdentify}{identifyCode}.Single()";
                    }

                    //特殊な手法で特定できた
                    var code = _customIdentify.Generate(obj, tree, usings);
                    if (!string.IsNullOrEmpty(code))
                    {
                        //.Dynamic()が今合ってないので削除しておく
                        var toDynamic = ".Dynamic()";
                        var index = code.LastIndexOf(toDynamic);
                        if (index == code.Length - toDynamic.Length)
                        {
                            code = code.Substring(0, code.Length - toDynamic.Length);
                        }

                        return preIdentify + code;
                    }
                    var sameType = CollectionUtility.OfType(tree, obj.GetType());
                    preIdentify = $"{preIdentify}.ByType(\"{obj.GetType().FullName}\")";

                    if (sameType.Count == 1)
                    {
                        //タイプで特定できた
                        return $"{preIdentify}.Single()";
                    }

                    //タイプとバインディングで特定できた
                    identifyCode = TryIdentifyFromBinding(sameType, obj, cache);
                    if (!string.IsNullOrEmpty(identifyCode))
                    {
                        return $"{preIdentify}{identifyCode}.Single()";
                    }
                }
                preIdentify = prefix + "VisualTree()";
            }

            nogood = true;

            //特定できなかったのでインデックスで行く
            preIdentify = prefix + "LogicalTree()";
            foreach (var tree in new[] { logical, visual })
            {
                if (Exist(tree, obj))
                {
                    var sameType = CollectionUtility.OfType(tree, obj.GetType());
                    preIdentify = $"{preIdentify}.ByType(\"{obj.GetType().FullName}\")";
                    for (int i = 0; i < sameType.Count; i++)
                    {
                        if (ReferenceEquals(sameType[i], obj))
                        {
                            return $"{preIdentify}[{i}]";
                        }
                    }
                }
                preIdentify = prefix + "VisualTree()";
            }

            return string.Empty;
        }
    }
}
