using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        const string AttachVariableWindowText = "Variable Window Text";
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

            //Asでのアタッチ用に足しておく
            {
                candidates.AddRange(GetDriverTypeCandidates((DependencyObject)obj));
            }
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
            var code = GenerateCodeCore((UIElement)targetControl, info);
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
            var list = new List<Type>();
            var type = root.GetType();
            // UIElementまでさかのぼって選択できるようにする
            while (type != null)
            {
                list.Add(type);
                type = type.BaseType;
                if (type == null || type.ToString().IndexOf(typeof(UIElement).ToString()) == 0)
                {
                    break;
                }
            }

            var driverName = string.Empty;
            var targetType = root.GetType();
            if (1 == list.Count)
            {
                driverName = list[0].Name + "Driver";
            }
            else
            {
                // 作成するクラスの選択
                using (var dlg = new TypeSelectForm(list.ToArray()))
                {
                    if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                    driverName = dlg.SelectedType.Name + "Driver";
                    targetType = dlg.SelectedType;
                }
            }

            string[] targetEventName = null;
            var generatorName = driverName + "Generator";
            var propertyCode = string.Empty;
            var methodCode = string.Empty;
            // プロパティ/フィールドとメソッドの選択
            using (var dlg = new DriverCodeSettingForm(targetType, driverName, generatorName))
            {
                dlg.DelegateGetDriverCode = GetDriverCode;
                dlg.DelegateGetGeneratorCode = GetGeneratorCode;

                EventInfo[] eventInfoList = null;
                try
                {
                    eventInfoList = targetType.GetEvents(
                        BindingFlags.FlattenHierarchy |
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.Static);
                }
                catch { }
                foreach (var eventInfo in eventInfoList)
                {
                    dlg.AddEventName(eventInfo.Name);
                }
                if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                targetEventName = dlg.GetSelectedEventName();
                propertyCode = dlg.GetOutputTextProperty();
                methodCode = dlg.GetOutputTextMethod();
            }

            var driverCode = GetDriverCode(targetType, driverName, propertyCode, methodCode);
            DriverCreatorAdapter.AddCode($"{driverName}.cs", driverCode, root);

            var generatorCode = GetGeneratorCode(driverName, generatorName, targetEventName);
            DriverCreatorAdapter.AddCode($"{generatorName}.cs", generatorCode, root);
        }

        /// <summary>
        /// ドライバ用コード作成
        /// </summary>
        /// <param name="targetType">対象オブジェクト</param>
        /// <param name="driverName">ドライバ名</param>
        /// <param name="propertyCode">プロパティ/フィールドのコード</param>
        /// <param name="methodCode">メソッドのコード</param>
        /// <returns>生成したコード</returns>
        static string GetDriverCode(Type targetType, string driverName, string propertyCode, string methodCode)
        {
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
    {";
            driverCode += (0 < propertyCode.Length) ? Environment.NewLine : "";
            driverCode += propertyCode;
            driverCode += @"
        public {driverName}(AppVar appVar)
            : base(appVar) { }";
            driverCode += (0 < methodCode.Length) ? Environment.NewLine : "";
            driverCode += methodCode;
            driverCode += @"
    }
}
";
            return driverCode.Replace("{typefullname}", targetType.FullName).Replace("{driverName}", driverName);
        }

        /// <summary>
        /// ジェネレータ用コード作成
        /// </summary>
        /// <param name="driverName">ドライバ名</param>
        /// <param name="generatorName">ジェネレータ名</param>
        /// <param name="targetEventName">追加するイベント名一覧</param>
        /// <returns>生成したコード</returns>
        static string GetGeneratorCode(string driverName, string generatorName, string[] targetEventName)
        {
            var generatorCode = @"using System;
using System.Windows;";
            if (targetEventName != null)
            {
                generatorCode += @"
using System.Collections.Generic;";
            }
            generatorCode += @"
using Codeer.TestAssistant.GeneratorToolKit;

namespace [*namespace]
{
    [CaptureCodeGenerator(""[*namespace.{driverName}]"")]
    public class {generatorName} : CaptureCodeGeneratorBase
    {";
            if (targetEventName != null)
            {
                generatorCode += @"
        List<Action> _removes = new List<Action>();";
            }
            generatorCode += @"
        UIElement _element;

        protected override void Attach()
        {
            _element = (UIElement)ControlObject;";

            if (targetEventName != null)
            {
                foreach (var name in targetEventName)
                {
                    generatorCode += @"
            _removes.Add(EventAdapter.Add(ControlObject, ""{eventName}"", {eventName}));";
                    generatorCode = generatorCode.Replace("{eventName}", name);
                }
            }
            generatorCode += @"
        }

        protected override void Detach()
        {";
            if (targetEventName != null)
            {
                generatorCode += @"
            _removes.ForEach(e => e());";
            }
            generatorCode += @"
        }";
            if (targetEventName != null)
            {
                foreach (var name in targetEventName)
                {
                    generatorCode += @"

        void {eventName}(object sender, dynamic e)
        {
        }";
                    generatorCode = generatorCode.Replace("{eventName}", name);
                }
            }
            generatorCode += @"
    }
}
";
            return generatorCode.Replace("{generatorName}", generatorName).Replace("{driverName}", driverName);
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

                var frameworkElement = e.Element as FrameworkElement;
                if (frameworkElement != null && frameworkElement.ContextMenu != null)
                {
                    var core = (frameworkElement is Window || frameworkElement is UserControl || frameworkElement is Page) ?
                                ".Core" : string.Empty;
                    var code = $"public WPFContextMenu {e.Name}ContextMenu => new WPFContextMenu{{Target = {e.Name}{core}.AppVar}};";
                    members.Add(code);
                }

                foreach (var x in e.ExtensionUsingNamespaces)
                {
                    if (!usings.Contains(x)) usings.Add(x);
                }
            }
        }

        string GenerateCodeCore(UIElement targetControl, DriverDesignInfo info)
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
                        "RM.Friendly.WPFStandardControls",
                        "System.Linq"
                    }, usings);
            DistinctAddRange(memberUsings, usings);
            DistinctAddRange(extensionUsings, usings);
            usings.Remove(DriverCreatorAdapter.SelectedNamespace);
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
                code.Add($"{Indent}{Indent}public WPFUIElement Core {{ get; }}");
                foreach (var e in members)
                {
                    code.Add($"{Indent}{Indent}{e}");
                }
                code.Add(string.Empty);
                code.Add($"{Indent}{Indent}public {info.ClassName}(AppVar core)");
                code.Add($"{Indent}{Indent}{{");
                code.Add($"{Indent}{Indent}{Indent}Core = new WPFUIElement(core);");
                code.Add($"{Indent}{Indent}}}");
            }
            code.Add($"{Indent}}}");
            return code;
        }

        static List<string> GenerateExtensions(UIElement targetControl, DriverDesignInfo info, out List<string> usings)
        {
            var code = new List<string>();
            usings = new List<string>();

            if (!info.CreateAttachCode) return code;

            code.Add(string.Empty);
            code.Add($"{Indent}public static class {info.ClassName}Extensions");
            code.Add($"{Indent}{{");

            var funcName = GetAttachFuncName(info.ClassName);

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
                        if (info.AttachMethod == AttachByTypeFullName)
                        {
                            code.Add($"{Indent}{Indent}[WindowDriverIdentify(TypeFullName = \"{targetControl.GetType().FullName}\")]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                            code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromTypeFullName(\"{targetControl.GetType().FullName}\").Dynamic();");
                        }
                        else
                        {
                            code.Add($"{Indent}{Indent}[WindowDriverIdentify(WindowText = \"{window.Title}\")]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                            code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromWindowText(\"{window.Title}\").Dynamic();");
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
                        code.Add($"{Indent}{Indent}public static T[] TryGet(this WindowsAppFriend app)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                    }
                    else
                    {
                        if (info.AttachMethod == AttachByTypeFullName)
                        {
                            code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                            code.Add($"{Indent}{Indent}{Indent}=> app.GetTopLevelWindows().SelectMany(e => e.GetFromTypeFullName(\"{targetControl.GetType().FullName}\")).FirstOrDefault()?.Dynamic();");
                        }
                    }
                }
            }
            //ドライバへのアタッチ
            else
            {
                var extensionSrc = "parent";
                if (targetControl.GetType().FullName == GetDriverTargetTypeFullName(info.AttachExtensionClass))
                {
                    funcName = GetAsFuncName(info.ClassName);
                    extensionSrc = "src";
                }

                SeparateNameSpaceAndTypeName(info.AttachExtensionClass, out var ns, out var parentDriver);
                if (!string.IsNullOrEmpty(ns))
                {
                    usings.Add(ns);
                }

                if (info.AttachMethod == AttachCustom)
                {
                    code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                    code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} {extensionSrc}, T identifier)");
                    code.Add($"{Indent}{Indent}{{");
                    code.Add($"{Indent}{Indent}{Indent}//TODO");
                    code.Add($"{Indent}{Indent}}}");
                    code.Add(string.Empty);
                    code.Add($"{Indent}{Indent}public static T[] TryGet(this {parentDriver} {extensionSrc})");
                    code.Add($"{Indent}{Indent}{{");
                    code.Add($"{Indent}{Indent}{Indent}//TODO");
                    code.Add($"{Indent}{Indent}}}");
                }
                else
                {
                    if (info.AttachMethod == AttachByTypeFullName)
                    {
                        code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} {extensionSrc})");

                        //ControlDriverであるか。本来はCoreを持っているかの方がよい
                        if (IsControlDriver(info.AttachExtensionClass))
                        {
                            code.Add($"{Indent}{Indent}{Indent}=> {extensionSrc}.VisualTree().ByType(\"{targetControl.GetType().FullName}\").FirstOrDefault()?.Dynamic();");
                        }
                        else
                        {
                            code.Add($"{Indent}{Indent}{Indent}=> {extensionSrc}.Core.VisualTree().ByType(\"{targetControl.GetType().FullName}\").FirstOrDefault()?.Dynamic();");
                        }
                    }
                }
            }
            code.Add($"{Indent}}}");

            return code;
        }

        static bool IsControlDriver(string attachExtensionClass)
        {
            foreach (var e in DriverCreatorAdapter.MultiTypeFullNameAndControlDriver)
            {
                foreach (var y in e.Value)
                {
                    if (y.ControlDriverTypeFullName == attachExtensionClass) return true;
                }
            }
            return false;
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

        static string GetAttachFuncName(string driverClassName)
            => GetAttachFuncNameCore("Attach", driverClassName);

        static string GetAsFuncName(string driverClassName)
            => GetAttachFuncNameCore("As", driverClassName);

        static string GetAttachFuncNameCore(string prefix, string driverClassName)
        {
            var index = driverClassName.IndexOf(DriverCreatorUtils.Suffix);
            if (0 < index && index == driverClassName.Length - DriverCreatorUtils.Suffix.Length)
            {
                return $"{prefix}{driverClassName.Substring(0, driverClassName.Length - DriverCreatorUtils.Suffix.Length)}";
            }
            return prefix + driverClassName;
        }
        
        static string GetDriverTargetTypeFullName(string driverTypeFullname)
        {
            foreach (var x in DriverCreatorAdapter.MultiTypeFullNameAndControlDriver)
            {
                foreach (var y in x.Value)
                {
                    if (y.ControlDriverTypeFullName == driverTypeFullname) return x.Key;
                }
            }
            foreach (var x in DriverCreatorAdapter.MultiTypeFullNameAndUserControlDriver)
            {
                foreach (var y in x.Value)
                {
                    if (y.DriverTypeFullName == driverTypeFullname) return x.Key;
                }
            }
            foreach (var x in DriverCreatorAdapter.MultiTypeFullNameAndWindowDriver)
            {
                foreach (var y in x.Value)
                {
                    if (y.DriverTypeFullName == driverTypeFullname) return x.Key;
                }
            }
            return string.Empty;
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
            var nameSource = string.Empty;
            var usings = new List<string>();
            var accessPaths = new List<string>();
            var isTree = new List<bool>();
            foreach (var e in ancestor)
            {
                //直接のフィールドに持っているか？
                var path = GetAccessPath(e, target, dom);
                if (!string.IsNullOrEmpty(path))
                {
                    //最初がフィールドで特定できた場合はその名前を使う
                    if (target == elementCtrl)
                    {
                        var sp = path.Split('.');
                        nameSource = sp.Length == 0 ? string.Empty : sp[sp.Length - 1];
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
                    path = MakeCodeGetFromTree(string.Empty, logicalForGetter, visualForGetter, target, bindingExpressionCache, usings, out var nogood, out var nameFromTree);
                    if (string.IsNullOrEmpty(path)) return null;
                    if (nogood) isPerfect = false;
                    accessPaths.Insert(0, path);
                    isTree.Insert(0, true);
                    target = e;
                    if (string.IsNullOrEmpty(nameSource))
                    {
                        nameSource = nameFromTree;
                    }
                }
            }

            if (target != rootCtrl) return null;

            var names = new List<string>();
            var customNameGenerator = new DriverElementNameGeneratorAdaptor(dom);
            var name = customNameGenerator.MakeDriverPropName(elementCtrl, nameSource, names);

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

        static string GetAccessPath(DependencyObject parent, DependencyObject target, CodeDomProvider dom)
        {
            foreach (var e in parent.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (!dom.IsValidIdentifier(e.Name)) continue;
                if (ReferenceEquals(e.GetValue(parent), target)) return e.Name;
            }
            foreach (var e in parent.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                try
                {
                    if (!dom.IsValidIdentifier(e.Name)) continue;
                    if (e.GetGetMethod().GetParameters().Length != 0) continue;
                    if (ReferenceEquals(e.GetValue(parent, new object[0]), target)) return e.Name;
                }
                catch { }
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

        static string TryIdentifyFromBinding(List<DependencyObject> tree, DependencyObject obj, BindingExpressionCache cache, out string name)
        {
            name = string.Empty;
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
                if (objFind && matchExps.Count == 1)
                {
                    name = path;
                    return $".ByBinding(\"{path}\")";
                }
            }
            return string.Empty;
        }

        string MakeCodeGetFromTree(string prefix, List<DependencyObject> logical, List<DependencyObject> visual, DependencyObject obj, BindingExpressionCache cache, List<string> usings, out bool nogood, out string name)
        {
            name = string.Empty;
            nogood = false;
            var visualTreeMethodName = GetVisualTreeMethodName(visual, obj);

            var preIdentify = prefix + "LogicalTree()";
            foreach (var tree in new[] { logical, visual })
            {
                if (Exist(tree, obj))
                {
                    //バインディングで特定できた
                    var identifyCode = TryIdentifyFromBinding(tree, obj, cache, out name);
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
                    identifyCode = TryIdentifyFromBinding(sameType, obj, cache, out name);
                    if (!string.IsNullOrEmpty(identifyCode))
                    {
                        return $"{preIdentify}{identifyCode}.Single()";
                    }
                }
                preIdentify = prefix + visualTreeMethodName;
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
                preIdentify = prefix + visualTreeMethodName;
            }

            return string.Empty;
        }

        string GetVisualTreeMethodName(List<DependencyObject> tree, DependencyObject obj)
        {
            int index = tree.IndexOf(obj);
            var objTmp = obj;
            while (0 < index)
            {
                objTmp = VisualTreeHelper.GetParent(objTmp);
                index = tree.IndexOf(objTmp);
                if (index < 0)
                {
                    return "VisualTreeWithPopup()";
                }
            }

            return "VisualTree()";
        }
    }
}
