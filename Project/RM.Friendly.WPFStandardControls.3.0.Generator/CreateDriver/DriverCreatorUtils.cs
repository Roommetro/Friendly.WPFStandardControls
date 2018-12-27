using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal static class DriverCreatorUtils
    {
        public static string Suffix { get; } = "_Driver";

        public static string GetTypeName(string driver)
        {
            var sp = driver.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return sp[sp.Length - 1];
        }

        public static string GetTypeNamespace(string driver)
        {
            var index = driver.LastIndexOf(".");
            if (index == -1) return driver;
            return driver.Substring(0, index);
        }

        public static string MakeDriverType(object control, Dictionary<string, WindowDriverInfo> typeFullNameAndWindowDriver)
            => MakeDriverType(control, typeFullNameAndWindowDriver, out _);

        public static string MakeDriverType(object control, Dictionary<string, WindowDriverInfo> typeFullNameAndWindowDriver, out string nameSpace)
        {
            nameSpace = string.Empty;
            if (typeFullNameAndWindowDriver.TryGetValue(control.GetType().FullName, out var typeFullName))
            {
                nameSpace = GetTypeNamespace(typeFullName.DriverTypeFullName);
                return GetTypeName(typeFullName.DriverTypeFullName);
            }
            return control.GetType().Name + Suffix;
        }

        public static string GetDriverTypeFullName<T>(T ctrl, Dictionary<string, ControlDriverInfo> netTypeAndDriverType)
        {
            var info = GetDriverInfo(ctrl, netTypeAndDriverType);
            return (info == null) || !info.DriverMappingEnabled ? string.Empty : info.ControlDriverTypeFullName;
        }

        public static ControlDriverInfo GetDriverInfo<T>(T ctrl, Dictionary<string, ControlDriverInfo> netTypeAndDriverType)
        {
            for (var type = ctrl.GetType(); type != null; type = type.BaseType)
            {
                if (netTypeAndDriverType.TryGetValue(type.FullName, out var driver)) return driver;
            }
            return null;
        }

        public static ControlAndFieldName<T>[] GetFields<T>(object obj, params Type[] endTypesSrc) where T : class
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
