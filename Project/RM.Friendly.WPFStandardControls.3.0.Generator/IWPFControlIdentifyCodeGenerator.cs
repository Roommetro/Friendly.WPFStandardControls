using System.Collections.Generic;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator
{
    /// <summary>
    /// Interface that generates code to identify WPF controls.
    /// </summary>
    public interface IWPFControlIdentifyCodeGenerator
    {
        /// <summary>
        /// Priority. The higher the value, the higher the priority.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Generate code that identifies the control of WPF.
        /// </summary>
        /// <param name="target">Object to be identified.</param>
        /// <param name="tree">A one-dimensional list of logical trees or visual trees.</param>
        /// <returns>Code specific information.</returns>
        WPFIdentifyCodeInfo GenerateIdentifyCode(DependencyObject target, IEnumerable<DependencyObject> tree);
    }
}
