using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// VisualTree and LogicalTree utility.
    /// In order to run inside the target process, you will need to injection the RM.Friendly.WPFStandardControls.3.5.dll.
    /// Use the RM.Friendly.WPFStandardControls.WPFStandardControls_3_5.Injection method.
    /// </summary>
#else
    /// <summary>
    /// VisualTreeとLogicalTreeのユーティリティー。
    /// 対象プロセス内部で実行するためには、RM.Friendly.WPFStandardControls.3.5.dllをインジェクションする必要があります。
    /// RM.Friendly.WPFStandardControls.WPFStandardControls_3_5.Injectionメソッドを利用してください。
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
        /// Enumerate DependencyObject continuing to VisualTree. (Include popup)
        /// </summary>
        /// <param name="start">Start DependencyObject.</param>
        /// <returns>Enumerated DependencyObject.</returns>
#else
        /// <summary>
        /// VisualTreeに連なるDependencyObjectを列挙（Popupを含める）。
        /// </summary>
        /// <param name="start">列挙を開始するDependencyObject。</param>
        /// <returns>列挙されたDependencyObject。</returns>
#endif
        public static IEnumerable<DependencyObject> VisualTreeWithPopup(this DependencyObject start)
        {
            return TreeUtilityInTarget.VisualTreeWithPopup(start);
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
