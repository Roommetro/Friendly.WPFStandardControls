using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Inside
{
    static class CastUtility
    {
        internal static void CastList(IEnumerable<DependencyObject> src, IList dst)
        {
            foreach (var e in src)
            {
                dst.Add(e);
            }
        }

        internal static IEnumerable<TDst> CastList<TSrc, TDst>(IEnumerable<TSrc> src)
        {
            var dst = new List<TDst>();
            foreach (var e in src)
            {
                dst.Add((TDst)(object)e);
            }
            return dst;
        }
    }
}
