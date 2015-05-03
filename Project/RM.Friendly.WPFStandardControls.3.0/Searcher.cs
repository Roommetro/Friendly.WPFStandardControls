using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;

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
    public static class Searcher
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
        public static WPFDependencyObjectCollection ByBinding(WPFDependencyObjectCollection collection, string path, object dataItem = null)
        {
            var app = (WindowsAppFriend)collection.AppVar.App;
            WPFStandardControls_3.Injection(app);
            return new WPFDependencyObjectCollection(app[typeof(SearcherInTarget), "ByBinding"](collection, path, dataItem));
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
        public static WPFDependencyObjectCollection ByType<T>(WPFDependencyObjectCollection collection)
        {
            return ByType(collection, typeof(T).FullName);
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
        public static WPFDependencyObjectCollection ByType(WPFDependencyObjectCollection collection, string typeFullName)
        {
            var app = (WindowsAppFriend)collection.AppVar.App;
            WPFStandardControls_3.Injection(app);
            return new WPFDependencyObjectCollection(app[typeof(SearcherInTarget), "ByBinding"](collection, typeFullName));
        }
    }
}
