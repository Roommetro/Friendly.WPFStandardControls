using Codeer.TestAssistant.GeneratorToolKit;
using System.Collections.Generic;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class DriverTypeNameManager
    {
        string _selectedNamespace;
        Dictionary<string, WindowDriverInfo> _typeFullNameAndWindowDriver;
        Dictionary<string, UserControlDriverInfo> _typeFullNameAndUserControlDriver;

        internal DriverTypeNameManager(string selectedNamespace, Dictionary<string, WindowDriverInfo> typeFullNameAndWindowDriver, Dictionary<string, UserControlDriverInfo> typeFullNameAndUserControlDriver)
        {
            _selectedNamespace = selectedNamespace;
            _typeFullNameAndWindowDriver = new Dictionary<string, WindowDriverInfo>(typeFullNameAndWindowDriver);
            _typeFullNameAndUserControlDriver = new Dictionary<string, UserControlDriverInfo>(typeFullNameAndUserControlDriver);
        }

        internal string MakeDriverType(object control)
            => MakeDriverType(control, out _);

        internal string MakeDriverType(object control, out string nameSpace)
        {
            nameSpace = string.Empty;
            if (_typeFullNameAndWindowDriver.TryGetValue(control.GetType().FullName, out var info))
            {
                nameSpace = DriverCreatorUtils.GetTypeNamespace(info.DriverTypeFullName);
                return DriverCreatorUtils.GetTypeName(info.DriverTypeFullName);
            }

            var name = control.GetType().Name + DriverCreatorUtils.Suffix;
            var fullName = _selectedNamespace + "." + name;

            var nameList = new List<string>();
            foreach (var e in _typeFullNameAndWindowDriver)
            {
                nameList.Add(e.Value.DriverTypeFullName);
            }

            int index = 1;
            while (nameList.Contains(fullName))
            {
                name = control.GetType().Name + DriverCreatorUtils.Suffix + index++;
                fullName = _selectedNamespace + "." + name;
            }

            _typeFullNameAndWindowDriver[control.GetType().FullName] = new WindowDriverInfo { DriverTypeFullName = fullName };
            return name;
        }
    }
}
