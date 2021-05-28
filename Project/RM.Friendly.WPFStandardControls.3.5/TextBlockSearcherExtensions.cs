using Codeer.Friendly;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search to TextBlock.
    /// </summary>
#else
    /// <summary>
    /// TextBlockを検索するためのユーティリティー
    /// </summary>
#endif
    public static class TextBlockSearcherExtensions
    {
#if ENG
        /// <summary>
        /// Search by Text from TextBlock collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">TextBlock collection.</param>
        /// <param name="contentText">Content text.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// Textから要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="contentText">文字列。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByText<T>(this IWPFDependencyObjectCollection<T> collection, string contentText) where T : TextBlock
            => TextBlockSearcher.ByText<T>(collection, contentText);
    }
}