﻿using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows;

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
    public static class TreeUtilityExtensions
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
        public static IWPFDependencyObjectCollection<DependencyObject> VisualTree(this AppVar start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            var app = (WindowsAppFriend)start.App;
            return TreeUtility.VisualTree(start, direction);
        }

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
        public static IWPFDependencyObjectCollection<DependencyObject> VisualTree(this IAppVarOwner start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            var app = (WindowsAppFriend)start.AppVar.App;
            return TreeUtility.VisualTree(start.AppVar, direction);
        }

#if ENG
        /// <summary>
        /// Enumerate DependencyObject continuing to VisualTree. (Include popup)
        /// </summary>
        /// <param name="start">Start DependencyObject.</param>
        /// <param name="direction">Run direction.</param>
        /// <returns>Enumerated DependencyObject.</returns>
#else
        /// <summary>
        /// VisualTreeに連なるDependencyObjectを列挙（Popupを含める）。
        /// </summary>
        /// <param name="start">列挙を開始するDependencyObject。</param>
        /// <param name="direction">走査方向。</param>
        /// <returns>列挙されたDependencyObject。</returns>
#endif
        public static IWPFDependencyObjectCollection<DependencyObject> VisualTreeIncludePopup(this AppVar start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            var app = (WindowsAppFriend)start.App;
            return TreeUtility.VisualTreeIncludePopup(start, direction);
        }

#if ENG
        /// <summary>
        /// Enumerate DependencyObject continuing to VisualTree. (Include popup)
        /// </summary>
        /// <param name="start">Start DependencyObject.</param>
        /// <param name="direction">Run direction.</param>
        /// <returns>Enumerated DependencyObject.</returns>
#else
        /// <summary>
        /// VisualTreeに連なるDependencyObjectを列挙（Popupを含める）。
        /// </summary>
        /// <param name="start">列挙を開始するDependencyObject。</param>
        /// <param name="direction">走査方向。</param>
        /// <returns>列挙されたDependencyObject。</returns>
#endif
        public static IWPFDependencyObjectCollection<DependencyObject> VisualTreeIncludePopup(this IAppVarOwner start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            var app = (WindowsAppFriend)start.AppVar.App;
            return TreeUtility.VisualTreeIncludePopup(start.AppVar, direction);
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
        public static IWPFDependencyObjectCollection<DependencyObject> LogicalTree(this AppVar start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            var app = (WindowsAppFriend)start.App;
            return TreeUtility.LogicalTree(start, direction);
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
        public static IWPFDependencyObjectCollection<DependencyObject> LogicalTree(this IAppVarOwner start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            var app = (WindowsAppFriend)start.AppVar.App;
            return TreeUtility.LogicalTree(start.AppVar, direction);
        }
    }
}
