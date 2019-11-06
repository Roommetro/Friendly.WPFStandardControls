using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RM.Friendly.WPFStandardControls.Generator
{
    /// <summary>
    /// Class that generates code to identify WPF controls.
    /// </summary>
    public class WPFControlIdentifyCodeGenerator : IWPFControlIdentifyCodeGenerator
    {
        public int Priority => 0;

        /// <summary>
        /// Generate code that identifies the control of WPF.
        /// </summary>
        /// <param name="target"> Object to be identified.</param>
        /// <param name="tree">A one-dimensional list of logical trees or visual trees.</param>
        /// <returns>Code specific information.</returns>
        public WPFIdentifyCodeInfo GenerateIdentifyCode(DependencyObject target, IEnumerable<DependencyObject> tree)
        {
            var content = target as ContentControl;
            if (content != null)
            {
                var text = content.Content?.ToString();
                if (text != null && Where(OfType<ContentControl>(tree), e => text.Equals(e.Content)).Count == 1)
                {
                    return new WPFIdentifyCodeInfo
                    {
                        IdentifyCode = "ByType<ContentControl>().ByContentText(\"" + text + "\").Single().Dynamic()",
                        AddUsings = new[] { "System.Windows.Controls" }
                    };
                }
            }

            var button = target as Button;
            if (button != null)
            {
                if (button.IsCancel && Where(OfType<Button>(tree), e => e.IsCancel).Count == 1)
                {
                    return new WPFIdentifyCodeInfo
                    {
                        IdentifyCode = "ByType<Button>().ByIsCancel().Single().Dynamic()",
                        AddUsings = new[] { "System.Windows.Controls" }
                    };
                }
            }

            var buttonBase = target as ButtonBase;
            if (buttonBase != null)
            {
                var text = buttonBase.CommandParameter?.ToString();
                if (Where(OfType<ButtonBase>(tree), e => text == e.CommandParameter?.ToString()).Count == 1)
                {
                    return new WPFIdentifyCodeInfo
                    {
                        IdentifyCode = "ByType<ButtonBase>().ByCommandParameterText(\"" + text + "\").Single().Dynamic()",
                        AddUsings = new[] { "System.Windows.Controls.Primitives" }
                    };
                }
            }
            return null;
        }

        static List<T> OfType<T>(IEnumerable src) where T : class
        {
            var dst = new List<T>();
            foreach (var e in src)
            {
                var obj = e as T;
                if (obj != null) dst.Add(obj);
            }
            return dst;
        }

        delegate bool Check<T>(T obj);
        static List<T> Where<T>(List<T> src, Check<T> check)
        {
            var dst = new List<T>();
            foreach (var e in src)
            {
                if (check(e)) dst.Add(e);
            }
            return dst;
        }
    }
}