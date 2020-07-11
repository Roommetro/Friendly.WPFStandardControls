using Codeer.TestAssistant.GeneratorToolKit;
using System.Collections.Generic;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    static class ElementPicker
    {
        internal static void PickupChildren(UIElement ctrl)
        {
            foreach (UIElement e in GetChildren(ctrl))
            {
                var driver = DriverCreatorUtils.GetDriverTypeFullName(e, DriverCreatorAdapter.TypeFullNameAndControlDriver, DriverCreatorAdapter.TypeFullNameAndUserControlDriver, DriverCreatorAdapter.TypeFullNameAndWindowDriver, out var searchDescendantUserControls);
                if (!string.IsNullOrEmpty(driver))
                {
                    DriverCreatorAdapter.AddDriverElements(e);
                }
            }
        }

        static IEnumerable<UIElement> GetChildren(UIElement ctrl)
        {
            var list = new List<UIElement>();

            foreach (var e in WPFUtility.GetLogicalTreeDescendants(ctrl, true, true, 0))
            {
                var element = e as UIElement;
                if (element == null) continue;
                if (!list.Contains(element)) list.Add(element);
            }
            foreach (var e in WPFUtility.GetVisualTreeDescendants(ctrl, true, true, 0))
            {
                var element = e as UIElement;
                if (element == null) continue;
                if (!list.Contains(element)) list.Add(element);
            }
            return list;
        }
    }
}
