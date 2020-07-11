using Codeer.TestAssistant.GeneratorToolKit;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

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
            }

            if (target is ItemsControl itemsControl)
            {
                dic["Create Items Control Driver(&I)"] = () =>
                {
                    using (var form = new InputDriverNameForm())
                    {
                        if (form.ShowDialog() != DialogResult.OK) return;
                        using (var dom = CodeDomProvider.CreateProvider("CSharp"))
                        {
                            new WPFDriverCreator(dom).CreateItemsControlDriver(form.DriverName, itemsControl);
                        }
                    }
                };
            }

            if (target is UIElement)
            {
                dic["Create Control Driver(&D)"] = () =>
                {
                    using (var dom = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        new WPFDriverCreator(dom).CreateControlDriver((UIElement)target);
                    }
                };
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
