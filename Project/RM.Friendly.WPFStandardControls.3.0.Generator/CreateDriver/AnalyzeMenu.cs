using Codeer.TestAssistant.GeneratorToolKit;
using System.Collections.Generic;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class AnalyzeMenu : IWindowAnalysisMenuAction
    {
        public Dictionary<string, MenuAction> GetAction(object target, WindowAnalysisTreeInfo info)
        {
            var dic = new Dictionary<string, MenuAction>();
            if (target is UIElement uiElement)
            {
                dic["Pickup Children(&P)"] = () => ElementPicker.PickupChildren(uiElement);
                dic["Create Control Driver(&D)"] = () => DriverDesigner.CreateControlDriver((UIElement)target);
                dic["Show Base Class(&B)"] = () =>
                {
                    AnalyzeWindow.Output.Show();
                    var type = target.GetType();
                    AnalyzeWindow.Output.WriteLine(string.Empty);
                    while (type != null)
                    {
                        AnalyzeWindow.Output.WriteLine(type.FullName);
                        type = type.BaseType;
                    }
                };
            }
            return dic;
        }
    }
}
