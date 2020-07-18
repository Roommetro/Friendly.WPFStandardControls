using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class DriverCreatorUtils
    {
        internal const string Suffix = "Driver";

        internal static string GetTypeName(string driver)
        {
            var sp = driver.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return sp.Length == 0 ? string.Empty : sp[sp.Length - 1];
        }

        internal static string GetTypeNamespace(string driver)
        {
            var index = driver.LastIndexOf(".");
            if (index == -1) return driver;
            return driver.Substring(0, index);
        }

        public static string[] GetDriverTypeFullNames<T>(T ctrl, Dictionary<string, List<ControlDriverInfo>> ctrls, Dictionary<string, List<UserControlDriverInfo>> userControls, Dictionary<string, List<WindowDriverInfo>> windows)
        {
            var types = new List<string>();

            {
                var info = GetDriverInfo(ctrl, ctrls);
                if (info != null)
                {
                    foreach (var e in info)
                    {
                        types.Add(e.ControlDriverTypeFullName);

                        if (e.ControlDriverTypeFullName == "RM.Friendly.WPFStandardControls.WPFListBox")
                        {
                            types.Add($"{e.ControlDriverTypeFullName}<T>");
                            foreach (var x in GetDriverInfo(typeof(ListBoxItem), userControls))
                            {
                                types.Add($"{e.ControlDriverTypeFullName}<{x.DriverTypeFullName}>");
                            }
                        }
                        else if (e.ControlDriverTypeFullName == "RM.Friendly.WPFStandardControls.WPFListView")
                        {
                            types.Add($"{e.ControlDriverTypeFullName}<T>");
                            foreach (var x in GetDriverInfo(typeof(ListViewItem), userControls))
                            {
                                types.Add($"{e.ControlDriverTypeFullName}<{x.DriverTypeFullName}>");
                            }
                        }

                        else if (e.ControlDriverTypeFullName == "RM.Friendly.WPFStandardControls.WPFTreeView")
                        {
                            types.Add($"{e.ControlDriverTypeFullName}<T>");
                            foreach (var x in GetDriverInfo(typeof(TreeViewItem), userControls))
                            {
                                types.Add($"{e.ControlDriverTypeFullName}<{x.DriverTypeFullName}>");
                            }
                        }
                    }
                }
            }

            {
                var info = GetDriverInfo(ctrl, userControls);
                if (info != null)
                {
                    foreach (var e in info)
                    {
                        types.Add(e.DriverTypeFullName);
                    }
                }
            }

            {
                var info = GetDriverInfo(ctrl, windows);
                if (info != null)
                {
                    foreach (var e in info)
                    {
                        types.Add(e.DriverTypeFullName);
                    }
                }
            }


            //★ ListCtrlとかあった場合 それぞれのItemに対応するUserControlがあったらそれを候補にだす

            types.Add("Codeer.Friendly.AppVar");

            return types.ToArray();
        }

        static List<DriverInfo> GetDriverInfo<DriverInfo>(Type ctrlType, Dictionary<string, List<DriverInfo>> netTypeAndDriverType) where DriverInfo : class
        {
            for (var type = ctrlType; type != null; type = type.BaseType)
            {
                if (netTypeAndDriverType.TryGetValue(type.FullName, out var drivers)) return drivers;
            }
            return new List<DriverInfo>();
        }

        static DriverInfo GetDriverInfo<T, DriverInfo>(T ctrl, Dictionary<string, DriverInfo> netTypeAndDriverType) where DriverInfo : class
        {
            for (var type = ctrl.GetType(); type != null; type = type.BaseType)
            {
                if (netTypeAndDriverType.TryGetValue(type.FullName, out var driver)) return driver;
            }
            return null;
        }

        public static string GetDriverTypeFullName<T>(T ctrl, Dictionary<string, ControlDriverInfo> ctrls, Dictionary<string, UserControlDriverInfo> userControls, Dictionary<string, WindowDriverInfo> windows, out bool searchDescendantUserControls)
        {
            searchDescendantUserControls = true;

            var info = GetDriverInfo(ctrl, ctrls);
            if (info != null)
            {
                searchDescendantUserControls = info.SearchDescendantUserControls;
                if (info.DriverMappingEnabled) return info.ControlDriverTypeFullName;
            }

            var driver = GetDriverTypeFullName(ctrl, userControls);
            if (!string.IsNullOrEmpty(driver)) return driver;

            return GetDriverTypeFullName(ctrl, windows);
        }

        internal static string GetDriverTypeFullName<T>(T ctrl, Dictionary<string, ControlDriverInfo> netTypeAndDriverType, Dictionary<string, UserControlDriverInfo> typeFullNameAndUserControlDriver)
        {
            var controlDriverInfo = GetDriverInfo(ctrl, netTypeAndDriverType);
            var driver = (controlDriverInfo == null) || !controlDriverInfo.DriverMappingEnabled ? string.Empty : controlDriverInfo.ControlDriverTypeFullName;
            if (!string.IsNullOrEmpty(driver)) return driver;

            //カスタムコントロール対応
            var userControlDriverInfo = GetDriverInfo(ctrl, typeFullNameAndUserControlDriver);
            return userControlDriverInfo == null ? string.Empty : userControlDriverInfo.DriverTypeFullName;
        }

        static string GetDriverTypeFullName<T>(T ctrl, Dictionary<string, WindowDriverInfo> netTypeAndDriverType)
        {
            var info = GetDriverInfo(ctrl, netTypeAndDriverType);
            return info == null ? string.Empty : info.DriverTypeFullName;
        }

        static string GetDriverTypeFullName<T>(T ctrl, Dictionary<string, UserControlDriverInfo> netTypeAndDriverType)
        {
            var info = GetDriverInfo(ctrl, netTypeAndDriverType);
            return info == null ? string.Empty : info.DriverTypeFullName;
        }

        internal static ControlDriverInfo GetDriverInfo<T>(T ctrl, Dictionary<string, ControlDriverInfo> netTypeAndDriverType)
        {
            for (var type = ctrl.GetType(); type != null; type = type.BaseType)
            {
                if (netTypeAndDriverType.TryGetValue(type.FullName, out var driver)) return driver;
            }
            return null;
        }

        internal static WindowDriverInfo GetDriverInfo<T>(T ctrl, Dictionary<string, WindowDriverInfo> netTypeAndDriverType)
        {
            for (var type = ctrl.GetType(); type != null; type = type.BaseType)
            {
                if (netTypeAndDriverType.TryGetValue(type.FullName, out var driver)) return driver;
            }
            return null;
        }

        internal static UserControlDriverInfo GetDriverInfo<T>(T ctrl, Dictionary<string, UserControlDriverInfo> netTypeAndDriverType)
        {
            for (var type = ctrl.GetType(); type != null; type = type.BaseType)
            {
                if (netTypeAndDriverType.TryGetValue(type.FullName, out var driver)) return driver;
            }
            return null;
        }

        internal static ControlAndFieldName<T>[] GetFields<T>(object obj, params Type[] endTypesSrc) where T : class
        {
            var endTypes = new List<Type>(endTypesSrc);
            var list = new List<ControlAndFieldName<T>>();
            for (var type = obj.GetType(); (type != null) && !endTypes.Contains(type); type = type.BaseType)
            {
                foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    T ctrl = field.GetValue(obj) as T;
                    if (ctrl == null) continue;
                    list.Add(new ControlAndFieldName<T>(ctrl, field.Name));
                }
            }
            return list.ToArray();
        }
    }
}
