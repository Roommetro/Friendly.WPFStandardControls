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
    public static class TreeUtilityInTarget
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
        public static IEnumerable<DependencyObject> VisualTree(DependencyObject start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            switch (direction)
            {
                case TreeRunDirection.Descendants:
                    return GetVisualTreDescendants(start);
                case TreeRunDirection.Ancestors:
                    return GetVisualTreeAncestor(start);
                default:
                    throw new NotSupportedException("?");
            }
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
        public static IEnumerable<DependencyObject> LogicalTree(DependencyObject start, TreeRunDirection direction = TreeRunDirection.Descendants)
        {
            switch (direction)
            {
                case TreeRunDirection.Descendants:
                    return GetLogicalTreDescendants(start);
                case TreeRunDirection.Ancestors:
                    return GetLogicalTreeAncestor(start);
                default:
                    throw new NotSupportedException("?");
            }
        }

        static IEnumerable<DependencyObject> GetVisualTreDescendants(DependencyObject obj)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            list.Add(obj);
            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                list.AddRange(GetVisualTreDescendants(VisualTreeHelper.GetChild(obj, i)));
            }
            return list;
        }

        static DependencyObject[] GetVisualTreeAncestor(DependencyObject obj)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            while (obj != null)
            {
                list.Add(obj);
                obj = VisualTreeHelper.GetParent(obj);
            }
            return list.ToArray();
        }

        static DependencyObject[] GetLogicalTreDescendants(DependencyObject obj)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            list.Add(obj);
            foreach (var e in LogicalTreeHelper.GetChildren(obj))
            {
                DependencyObject d = e as DependencyObject;
                if (d != null)
                {
                    list.AddRange(GetLogicalTreDescendants(d));
                }
            }
            return list.ToArray();
        }

        static DependencyObject[] GetLogicalTreeAncestor(DependencyObject obj)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            while (obj != null)
            {
                list.Add(obj);
                obj = LogicalTreeHelper.GetParent(obj);
            }
            return list.ToArray();
        }
    }
}
