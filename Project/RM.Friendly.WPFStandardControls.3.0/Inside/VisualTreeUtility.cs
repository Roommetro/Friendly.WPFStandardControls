using System.Collections.Generic;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Inside
{
    static class VisualTreeUtility
    {
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

        public delegate bool IsMatchParam<T>(Visual v, T param);

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
