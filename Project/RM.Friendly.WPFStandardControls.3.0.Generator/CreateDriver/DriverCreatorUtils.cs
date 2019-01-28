using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class DriverCreatorUtils
    {
        internal const string Suffix = "_Driver";

        internal static string GetTypeName(string driver)
        {
            var sp = driver.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return sp[sp.Length - 1];
        }

        internal static string GetTypeNamespace(string driver)
        {
            var index = driver.LastIndexOf(".");
            if (index == -1) return driver;
            return driver.Substring(0, index);
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

        internal static ControlDriverInfo GetDriverInfo<T>(T ctrl, Dictionary<string, ControlDriverInfo> netTypeAndDriverType)
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
