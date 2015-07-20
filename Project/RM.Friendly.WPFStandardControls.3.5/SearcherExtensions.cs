using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search by binding.
    /// </summary>
#else
    /// <summary>
    /// Binding情報から要素を取得するためのユーティリティー
    /// </summary>
#endif
    public static class SearcherExtensions
    {
#if ENG
        /// <summary>
        /// Search by binding from DependencyObject collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">DependencyObject collection.</param>
        /// <param name="path">Binding path.</param>
        /// <param name="dataItem">DataItem.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// Binding情報から要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="path">バインディングパス。</param>
        /// <param name="dataItem">DataItem。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByBinding<T>(this IWPFDependencyObjectCollection<T> collection, string path, ExplicitAppVar dataItem = null) where T : DependencyObject
        {
            return Searcher.ByBinding<T>(collection, path, dataItem);
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
        public static IWPFDependencyObjectCollection<T> ByType<T>(this  IWPFDependencyObjectCollection<DependencyObject> collection) where T : DependencyObject
        {
            return Searcher.ByType<T>(collection);
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
        public static IWPFDependencyObjectCollection<DependencyObject> ByType(this IWPFDependencyObjectCollection<DependencyObject> collection, string typeFullName)
        {
            return Searcher.ByType(collection, typeFullName);
        }
    }
}
