using System.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search to FrameworkElement.
    /// </summary>
#else
    /// <summary>
    /// FrameworkElementを検索するためのユーティリティー
    /// </summary>
#endif
    public static class FrameworkElementSearcherExtensions
    {
#if ENG
        /// <summary>
        /// Search by Name from FrameworkElement collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">FrameworkElement collection.</param>
        /// <param name="name">Name.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// 名前から要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">FrameworkElementのコレクション。</param>
        /// <param name="name">名前。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByName<T>(this IWPFDependencyObjectCollection<T> collection, string name) where T: FrameworkElement
        {
            return FrameworkElementSearcher.ByName<T>(collection, name);
        }
    }
}
