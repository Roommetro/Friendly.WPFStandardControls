using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// VisualTree and LogicalTree utility.
    /// </summary>
#else
    /// <summary>
    /// VisualTreeとLogicalTreeのユーティリティー。
    /// </summary>
#endif
    public static class TreeUtilityInTargetExtensions
    {
#if ENG
        /// <summary>
        /// Enumerate DependencyObject continuing to VisualTree.
        /// </summary>
        /// <param name="start">Start DependencyObject.</param>
        /// <param name="direction">Run direction.</param>
        /// <returns>Enumerated DependencyObject.</returns>
#else
        /// <summary>
        /// VisualTreeに連なるDependencyObjectを列挙。
        /// </summary>
        /// <param name="start">列挙を開始するDependencyObject。</param>
        /// <param name="direction">走査方向。</param>
        /// <returns>列挙されたDependencyObject。</returns>
#endif
        public static IEnumerable<DependencyObject> VisualTree(this DependencyObject start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            return TreeUtilityInTarget.VisualTree(start, direction);
        }

#if ENG
        /// <summary>
        /// Enumerate DependencyObject continuing to LogicalTree.
        /// </summary>
        /// <param name="start">Start DependencyObject.</param>
        /// <param name="direction">Run direction.</param>
        /// <returns>Enumerated DependencyObject.</returns>
#else
        /// <summary>
        /// LogicalTreeに連なるDependencyObjectを列挙。
        /// </summary>
        /// <param name="start">列挙を開始するDependencyObject。</param>
        /// <param name="direction">走査方向。</param>
        /// <returns>列挙されたDependencyObject。</returns>
#endif
        public static IEnumerable<DependencyObject> LogicalTree(this DependencyObject start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            return TreeUtilityInTarget.LogicalTree(start, direction);
        }
    }
}
