using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Inside
{
    /// <summary>
    /// VisualTreeUtility
    /// </summary>
    public static class VisualTreeUtility
    {
        /// <summary>
        /// Enumerate visual tree children.
        /// </summary>
        /// <param name="v">visual</param>
        /// <returns>children</returns>
        public static IEnumerable<Visual> GetChildren(Visual v)
        {
            if (v != null)
            {
                int count = VisualTreeHelper.GetChildrenCount(v);
                for (int i = 0; i < count; i++)
                {
                    Visual next = VisualTreeHelper.GetChild(v, i) as Visual;
                    if (next != null)
                    {
                        yield return next;
                    }
                }
            }
        }

        /// <summary>
        /// IsMatchParam
        /// </summary>
        /// <typeparam name="T">Param type.</typeparam>
        /// <param name="v">Visual.</param>
        /// <param name="param">Param.</param>
        /// <returns>Is match.</returns>
        public delegate bool IsMatchParam<T>(Visual v, T param);

        /// <summary>
        /// FindVisualItem
        /// </summary>
        /// <typeparam name="T">Param type.</typeparam>
        /// <param name="visual">Visual.</param>
        /// <param name="param">Param.</param>
        /// <param name="isMatchParam">IsMatchParam.</param>
        /// <returns>Match object.</returns>
        public static object FindVisualItem<T>(Visual visual, T param, IsMatchParam<T> isMatchParam)
        {
            foreach (var v in VisualTreeUtility.GetChildren(visual))
            {
                if (isMatchParam(v, param))
                {
                    return v;
                }
                object o = FindVisualItem<T>(v, param, isMatchParam);
                if (o != null)
                {
                    return o;
                }
            }
            return null;
        }

        /// <summary>
        /// GetCoreElement.
        /// </summary>
        /// <param name="visual">Root.</param>
        /// <param name="typeFullName">Type full name.</param>
        /// <returns>Match visual.</returns>
        public static Visual GetCoreElement(Visual visual, string typeFullName)
        {
            foreach (var v in VisualTreeUtility.GetChildren(visual))
            {
                if (v.GetType().FullName == typeFullName)
                {
                    return v;
                }
                Visual o = GetCoreElement(v, typeFullName);
                if (o != null)
                {
                    return o;
                }
            }
            return null;
        }
    }
}
