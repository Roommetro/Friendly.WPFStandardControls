using System;
using System.Collections.Generic;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal static class CollectionUtility
    {
        public static bool HasReference<T>(List<T> mappedControls, T control)
        {
            foreach (var e in mappedControls)
            {
                if (ReferenceEquals(e, control)) return true;
            }
            return false;
        }

        public static List<T> OfType<T>(List<T> src, Type type)
        {
            var dst = new List<T>();
            foreach (var e in src)
            {
                if (type.IsAssignableFrom(e.GetType())) dst.Add(e);
            }
            return dst;
        }
    }
}
