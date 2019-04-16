using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public class WPFDriverCreator
    {
        private const string TodoComment = "// TODO It is not the best way to identify. Please change to a better method.";
        private const string Indent = "    ";

        private readonly CodeDomProvider _dom;
        private readonly DriverElementNameGeneratorAdaptor _customNameGenerator;
        private readonly WPFCustomIdentify _customIdentify = new WPFCustomIdentify();
        private readonly DriverTypeNameManager _driverTypeNameManager;

        public WPFDriverCreator(CodeDomProvider dom)
        {
            _dom = dom;
            _customNameGenerator = new DriverElementNameGeneratorAdaptor(dom);
            _driverTypeNameManager = new DriverTypeNameManager(DriverCreatorAdapter.SelectedNamespace, DriverCreatorAdapter.TypeFullNameAndWindowDriver, DriverCreatorAdapter.TypeFullNameAndUserControlDriver);
        }

        public void CreateItemsControlDriver(string driverName, ItemsControl ctrl)
        {
            if (ctrl.Items.Count == 0) return;
            var item = ctrl.ItemContainerGenerator.ContainerFromIndex(0);
            var info = CreateDriverInfo(item, driverName + ".cs");
            DriverCreatorAdapter.AddCode(driverName + ".cs", GenerateCode(null, item, DriverCreatorAdapter.SelectedNamespace, driverName, info.Usings, info.Members, new List<Type>()), info.Target);
        }

        public void CreateDriver(DependencyObject root)
        {
            //FormとUserControlを全取得
            var recursiveCheck = new List<DependencyObject>();
            var targets = new Dictionary<Type, DependencyObject>();

            //ルートはUserControlでなくても指定されたらUserControlDriverを作れるようにする
            if (root is Control)
            {
                targets[root.GetType()] = root;
            }

            var getFromControlTreeOnly = new List<Type>();
            GetAllWindowAndUserControl(false, root, targets, getFromControlTreeOnly, recursiveCheck);

            //ドライバ情報を取得
            var driverInfos = new Dictionary<Type, DriverInfo<DependencyObject>>();
            foreach (var e in targets)
            {
                var fileName = $"{_driverTypeNameManager.MakeDriverType(e.Value)}.cs";
                var info = CreateDriverInfo(e.Value, fileName);
                driverInfos[e.Key] = info;
            }

            //変換
            foreach (var e in driverInfos)
            {
                var driverName = _driverTypeNameManager.MakeDriverType(e.Value.Target, out var nameSpace);
                if (string.IsNullOrEmpty(nameSpace)) nameSpace = DriverCreatorAdapter.SelectedNamespace;

                //コード生成
                DriverCreatorAdapter.AddCode($"{driverName}.cs", GenerateCode(root, e.Value.Target, nameSpace, driverName, e.Value.Usings, e.Value.Members, getFromControlTreeOnly), e.Value.Target);
            }
        }

        private void GetAllWindowAndUserControl(bool isControlTreeOnly, DependencyObject control, Dictionary<Type, DependencyObject> targets, List<Type> getFromControlTreeOnly, List<DependencyObject> recursiveCheck)
        {
            if (control == null) return;

            //再帰チェック
            if (CollectionUtility.HasReference(recursiveCheck, control)) return;
            recursiveCheck.Add(control);

            if ((control is Window) ||
                ((control is UserControl) && (control.GetType() != typeof(UserControl))) ||
                ((control is Page) && (control.GetType() != typeof(Page))))
            {
                targets[control.GetType()] = control;
                if (isControlTreeOnly)
                {
                    getFromControlTreeOnly.Add(control.GetType());
                }

                //Form, UserControlの時はメンバも見る
                foreach (var e in GetFields(control))
                {
                    //まれにGridなどををメンバに持っている場合がある。
                    if (e.Control.GetType().Assembly == typeof(Grid).Assembly) continue;

                    GetAllWindowAndUserControl(false, e.Control, targets, getFromControlTreeOnly, recursiveCheck);
                }
            }
            if (!(control is FrameworkElement)) return;

            foreach (var e in WPFUtility.GetLogicalTreeDescendants(control, true, true, 0))
            {
                GetAllWindowAndUserControl(true, e, targets, getFromControlTreeOnly, recursiveCheck);
            }
            foreach (var e in WPFUtility.GetVisualTreeDescendants(control, true, true, 0))
            {
                GetAllWindowAndUserControl(true, e, targets, getFromControlTreeOnly, recursiveCheck);
            }
        }

        private class ControlAndDefine
        {
            public DependencyObject Control { get; }
            public string Name { get; }
            public string Define { get; }

            public ControlAndDefine(DependencyObject control, string name, string define)
            {
                Control = control;
                Name = name;
                Define = define;
            }
        }

        private DriverInfo<DependencyObject> CreateDriverInfo(DependencyObject targetControl, string fileName)
        {
            var driverInfo = new DriverInfo<DependencyObject>(targetControl);

            var mappedControls = new List<DependencyObject>();
            var names = new List<string>();
            var ancesters = WPFUtility.GetVisualTreeAncestor(targetControl);

            var controlAndDefines = new List<ControlAndDefine>();

            //フィールドから検索
            foreach (var e in GetFields(targetControl))
            {
                //たまに親を持っているのがいるのではじく
                if (CollectionUtility.HasReference(ancesters, e.Control)) continue;

                //不正なフィールド名のものは取得できない
                if (!_dom.IsValidIdentifier(e.Name)) continue;

                //すでにマップされているかチェック
                if (CollectionUtility.HasReference(mappedControls, e.Control)) continue;

                //コントロールドライバ
                var driver = DriverCreatorUtils.GetDriverTypeFullName(e.Control, DriverCreatorAdapter.TypeFullNameAndControlDriver, DriverCreatorAdapter.TypeFullNameAndUserControlDriver);
                if (!string.IsNullOrEmpty(driver))
                {
                    mappedControls.Add(e.Control);
                    var name = _customNameGenerator.MakeDriverPropName(e.Control, e.Name, names);
                    var typeName = DriverCreatorUtils.GetTypeName(driver);
                    var nameSpace = DriverCreatorUtils.GetTypeNamespace(driver);
                    var key = $"Core.Dynamic().{e.Name}";
                    controlAndDefines.Add(new ControlAndDefine(e.Control, name, $"public {typeName} {name} => new {typeName}({key});"));
                    DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, key, e.Control);
                    if (!driverInfo.Usings.Contains(nameSpace)) driverInfo.Usings.Add(nameSpace);
                }
                //ユーザーコントロールドライバ
                else if (((e.Control is UserControl) && (e.Control.GetType() != typeof(UserControl))) ||
                         ((e.Control is Page) && (e.Control.GetType() != typeof(Page))))
                {
                    mappedControls.Add(e.Control);
                    var name = _customNameGenerator.MakeDriverPropName(e.Control, e.Name, names);
                    var typeName = _driverTypeNameManager.MakeDriverType(e.Control, out var nameSpace);
                    var key = $"Core.Dynamic().{e.Name}";
                    controlAndDefines.Add(new ControlAndDefine(e.Control, name, $"public {typeName} {name} => new {typeName}({key});"));
                    if (!string.IsNullOrEmpty(nameSpace) && (nameSpace != DriverCreatorAdapter.SelectedNamespace) && !driverInfo.Usings.Contains(nameSpace)) driverInfo.Usings.Add(nameSpace);
                    DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, key, e.Control);
                }
            }

            //フィールド上に現れないオブジェクトを検索
            CreateDriverInfoFindFromControlTree(targetControl, driverInfo, controlAndDefines, mappedControls, names, fileName);

            //Sortのロジックがイマイチわかっていない。念のため
            try
            {
                // LogicalTree順のコントロールリスト取得
                var controlList = WPFUtility.GetLogicalTreeDescendants(targetControl, true, true, 0);

                // フィールドをタブオーダーでソート
                controlAndDefines.Sort((l, r) => controlList.IndexOf(l.Control) - controlList.IndexOf(r.Control));
            }
            catch { }

            //コンテキストメニュー特別処理
            foreach (var e in controlAndDefines)
            {
                driverInfo.Members.Add(e.Define);
                var frameworkElement = e.Control as FrameworkElement;
                if (frameworkElement != null && frameworkElement.ContextMenu != null)
                {
                    var core = (frameworkElement is Window || frameworkElement is UserControl || frameworkElement is Page) ?
                                ".Core" : string.Empty;
                    var code = $"public WPFContextMenu {e.Name}ContextMenu => new WPFContextMenu{{Target = {e.Name}{core}.AppVar}};";
                    driverInfo.Members.Add(code);
                }
            }

            return driverInfo;
        }

        private void CreateDriverInfoFindFromControlTree(DependencyObject target, DriverInfo<DependencyObject> driverInfo, List<ControlAndDefine> controlAndDefines, List<DependencyObject> mappedControls, List<string> names, string fileName)
        {
            var logical = WPFUtility.GetLogicalTreeDescendants(target, true, true, 0);
            var visual = WPFUtility.GetVisualTreeDescendants(target, true, true, 0);
            var logicalForGetter = WPFUtility.GetLogicalTreeDescendants(target, false, false, 0);
            var visualForGetter = WPFUtility.GetVisualTreeDescendants(target, false, false, 0);
            foreach (var tree in new[] { logical, visual })
            {
                var cache = new BindingExpressionCache();
                foreach (var ctrl in tree)
                {
                    //すでにマップされているかチェック
                    if (CollectionUtility.HasReference(mappedControls, ctrl)) continue;

                    //コントロールドライバ検索
                    var driver = DriverCreatorUtils.GetDriverTypeFullName(ctrl, DriverCreatorAdapter.TypeFullNameAndControlDriver, DriverCreatorAdapter.TypeFullNameAndUserControlDriver);
                    if (!string.IsNullOrEmpty(driver))
                    {
                        var name = _customNameGenerator.MakeDriverPropName(ctrl, string.Empty, names);
                        var typeName = DriverCreatorUtils.GetTypeName(driver);
                        var nameSpace = DriverCreatorUtils.GetTypeNamespace(driver);
                        var getter = MakeCodeGetFromTree(logicalForGetter, visualForGetter, ctrl, cache, driverInfo.Usings, out var nogood);
                        var code = $"public {typeName} {name} => new {typeName}({getter});";
                        if (nogood)
                        {
                            code += $" {TodoComment}";
                        }
                        controlAndDefines.Add(new ControlAndDefine(ctrl, name, code));
                        mappedControls.Add(ctrl);
                        DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, getter, ctrl);
                        if (!driverInfo.Usings.Contains(nameSpace)) driverInfo.Usings.Add(nameSpace);
                    }
                }
            }
        }

        private string MakeCodeGetFromTree(List<DependencyObject> logical, List<DependencyObject> visual, DependencyObject obj, BindingExpressionCache cache, List<string> usings, out bool nogood)
        {
            nogood = false;

            //独自の取得方法
            var preIdentify = "Core.LogicalTree()";
            foreach (var tree in new[] { logical, visual })
            {
                if (Exist(tree, obj))
                {
                    var code = _customIdentify.Generate(obj, tree, usings);
                    if (!string.IsNullOrEmpty(code)) return preIdentify + code;
                }
                preIdentify = "Core.VisualTree()";
            }

            preIdentify = "Core.LogicalTree()";
            foreach (var tree in new[] { logical, visual })
            {
                if (Exist(tree, obj))
                {
                    var identifyCode = TryIdentifyFromBinding(tree, obj, cache);
                    if (!string.IsNullOrEmpty(identifyCode))
                    {
                        return $"{preIdentify}{identifyCode}.Single()";
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
                preIdentify = "Core.VisualTree()";
            }

            nogood = true;

            //特定できなかったのでインデックスで行く
            preIdentify = "Core.LogicalTree()";
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
                preIdentify = "Core.VisualTree()";
            }

            return string.Empty;
        }

        private static bool Exist(List<DependencyObject> tree, DependencyObject obj)
        {
            foreach (var e in tree)
            {
                if (ReferenceEquals(e, obj)) return true;
            }
            return false;
        }

        private static string TryIdentifyFromBinding(List<DependencyObject> tree, DependencyObject obj, BindingExpressionCache cache)
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

        private string GenerateCode(DependencyObject root, DependencyObject targetControl, string nameSpace, string driverClassName, List<string> usings, List<string> members, List<Type> getFromControlTreeOnly)
        {
            var code = new List<string>
            {
                "using Codeer.Friendly.Dynamic;",
                "using Codeer.Friendly;",
                "using Codeer.Friendly.Windows;",
                "using Codeer.Friendly.Windows.Grasp;",
                "using Codeer.TestAssistant.GeneratorToolKit;"
            };

            if (!usings.Contains("RM.Friendly.WPFStandardControls")) usings.Add("RM.Friendly.WPFStandardControls");
            foreach (var e in usings)
            {
                code.Add($"using {e};");
            }
            int nextUsingIndex = code.Count;

            var existMany = false;
            if (getFromControlTreeOnly.Contains(targetControl.GetType()))
            {
                existMany = WPFUtility.ExistMany(root, targetControl.GetType());
                if (existMany)
                {
                    code.Add("using System.Linq;");
                }
            }

            var attr = (targetControl is Window) ? "WindowDriver" : "UserControlDriver";

            code.Add(string.Empty);
            code.Add($"namespace {nameSpace}");
            code.Add("{");
            code.Add($"{Indent}[{attr}(TypeFullName = \"{targetControl.GetType().FullName}\")]");
            code.Add($"{Indent}public class {driverClassName}");
            code.Add($"{Indent}{{");

            if (targetControl is Window)
            {
                code.Add($"{Indent}{Indent}public WindowControl Core {{ get; }}");
                foreach (var e in members)
                {
                    code.Add($"{Indent}{Indent}{e}");
                }
                code.Add(string.Empty);
                code.Add($"{Indent}{Indent}public {driverClassName}(WindowControl core)");
                code.Add($"{Indent}{Indent}{{");
                code.Add($"{Indent}{Indent}{Indent}Core = core;");
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
                code.Add($"{Indent}{Indent}public {driverClassName}(AppVar core)");
                code.Add($"{Indent}{Indent}{{");
                code.Add($"{Indent}{Indent}{Indent}Core = new WPFUserControl(core);");
                code.Add($"{Indent}{Indent}}}");
            }
            code.Add($"{Indent}}}");

            if (getFromControlTreeOnly.Contains(targetControl.GetType()) && !ReferenceEquals(root, targetControl))
            {
                code.Add(string.Empty);
                code.Add($"{Indent}public static class {driverClassName}_Extensions");
                code.Add($"{Indent}{{");
                var funcName = GetFuncName(driverClassName);
                var rootDriver = _driverTypeNameManager.MakeDriverType(root, out var rootNameSpace);
                if (!string.IsNullOrEmpty(rootNameSpace) && !usings.Contains(rootNameSpace) && (rootNameSpace != nameSpace))
                {
                    code.Insert(nextUsingIndex, $"using {rootNameSpace};");
                }
                if (existMany)
                {
                    code.Add($"{Indent}{Indent}{TodoComment}");
                    code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                    code.Add($"{Indent}{Indent}public static {driverClassName} {funcName}(this {rootDriver} window, int index)");
                    code.Add($"{Indent}{Indent}{Indent}=> new {driverClassName}(window.Core.VisualTree().ByType(\"{targetControl.GetType().FullName}\")[index]);");
                    code.Add(string.Empty);
                    code.Add($"{Indent}{Indent}public static void TryGet(this {rootDriver} window, out int[] indices)");
                    code.Add($"{Indent}{Indent}{Indent}=> indices = Enumerable.Range(0, window.Core.VisualTree().ByType(\"{targetControl.GetType().FullName}\").Count).ToArray();");
                }
                else
                {
                    code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                    code.Add($"{Indent}{Indent}public static {driverClassName} {funcName}(this {rootDriver} window)");
                    code.Add($"{Indent}{Indent}{Indent}=> new {driverClassName}(window.Core.VisualTree().ByType(\"{targetControl.GetType().FullName}\").Single());");
                }
                code.Add($"{Indent}}}");
            }
            else if (targetControl is Window)
            {
                code.Add(string.Empty);
                code.Add($"{Indent}public static class {driverClassName}_Extensions");
                code.Add($"{Indent}{{");
                code.Add($"{Indent}{Indent}[WindowDriverIdentify(TypeFullName = \"{targetControl.GetType().FullName}\")]");
                code.Add($"{Indent}{Indent}public static {driverClassName} {GetFuncName(driverClassName)}(this WindowsAppFriend app)");
                code.Add($"{Indent}{Indent}{Indent}=> new {driverClassName}(app.WaitForIdentifyFromTypeFullName(\"{targetControl.GetType().FullName}\"));");
                code.Add($"{Indent}}}");
            }
            code.Add("}");
            return string.Join(Environment.NewLine, code.ToArray());
        }

        private string GetFuncName(string driverClassName)
        {
            return $"Attach_{driverClassName.Substring(0, driverClassName.Length - DriverCreatorUtils.Suffix.Length)}";
        }

        private static ControlAndFieldName<DependencyObject>[] GetFields(object obj)
            => DriverCreatorUtils.GetFields<DependencyObject>(obj, typeof(Window), typeof(UserControl), typeof(Page), typeof(DependencyObject));
    }
}
