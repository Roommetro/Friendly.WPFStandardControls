using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search by binding.
    /// </summary>
#else
    /// <summary>
    /// Binding情報から要素を取得するためのユーティリティー
    /// @@@
    /// </summary>
#endif
    public static class SearcherInTargetExtensions
    {
#if ENG
        /// <summary>
        /// Search by binding from DependencyObject collection.
        /// </summary>
        /// <param name="collection">DependencyObject collection.</param>
        /// <param name="path">Binding path.</param>
        /// <param name="dataItem">DataItem.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// Binding情報から要素を検索。
        /// </summary>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="path">バインディングパス。</param>
        /// <param name="dataItem">DataItem。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IEnumerable<DependencyObject> ByBinding(this IEnumerable<DependencyObject> collection, string path, object dataItem = null)
        {
            return SearcherInTarget.ByBinding(collection, path, dataItem);
        }

#if ENG
        /// <summary>
        /// Search by Type from DependencyObject collection.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="collection">DependencyObject collection.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// タイプから要素を検索。
        /// </summary>
        /// <typeparam name="T">検索対象のタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IEnumerable<DependencyObject> ByType<T>(this IEnumerable<DependencyObject> collection)
        {
            return SearcherInTarget.ByType<T>(collection);
        }

#if ENG
        /// <summary>
        /// Search by Type from DependencyObject collection.
        /// </summary>
        /// <param name="collection">DependencyObject collection.</param>
        /// <param name="typeFullName">Target type.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// タイプから要素を検索。
        /// </summary>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="typeFullName">検索対象のタイプ。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IEnumerable<DependencyObject> ByType(this IEnumerable<DependencyObject> collection, string typeFullName)
        {
            return SearcherInTarget.ByType(collection, typeFullName);
        }
    }
}
