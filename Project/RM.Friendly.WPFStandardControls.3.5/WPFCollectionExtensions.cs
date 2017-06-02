using Codeer.Friendly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// IWPFDependencyObjectCollection utility.
    /// </summary>
#else
    /// <summary>
    /// IWPFDependencyObjectCollectionのユーティリティー。
    /// </summary>
#endif
    public static class WPFCollectionExtensions
    {
#if ENG
        /// <summary>
        /// Make IWPFDependencyObjectCollection enumerable.
        /// </summary>
        /// <typeparam name="T">The enumerating object type.</typeparam>
        /// <param name="self">IWPFDependencyObjectCollection object.</param>
        /// <returns>The object which converted enumerable.</returns>
#else
        /// <summary>
        /// IWPFDependencyObjectCollectionを列挙可能な型に変換する。
        /// </summary>
        /// <typeparam name="T">反復するオブジェクトの型。</typeparam>
        /// <param name="self">IWPFDependencyObjectCollectionオブジェクト。</param>
        /// <returns>列挙可能な形式に変換されたオブジェクト。</returns>
#endif
        public static IEnumerable<AppVar> ToEnumerable<T>(this IWPFDependencyObjectCollection<T> self) where T : DependencyObject
        {
            for (int i = 0; i < self.Count; i++)
            {
                yield return self[i];
            }
        }
    }
}
