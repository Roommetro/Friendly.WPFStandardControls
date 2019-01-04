using Codeer.TestAssistant.GeneratorToolKit;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class CreateMenu : IWindowAnalysisMenuAction
    {
        public Dictionary<string, System.Windows.Forms.MethodInvoker> GetAction(object target, WindowAnalysisTreeInfo info)
        {
            var dic = new Dictionary<string, System.Windows.Forms.MethodInvoker>();
            if (target is DependencyObject ctrl)
            {
                dic["Create Driver(&C)"] = () =>
                {
                    using (var dom = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        new WPFDriverCreator(dom).CreateDriver(ctrl);
                    }
                };
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
            return dic;
        }
    }
}
