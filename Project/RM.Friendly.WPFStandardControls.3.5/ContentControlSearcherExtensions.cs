using Codeer.Friendly;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search to ContentControl.
    /// </summary>
#else
    /// <summary>
    /// ContentControlを検索するためのユーティリティー
    /// </summary>
#endif
    public static class ContentControlSearcherExtensions
    {
#if ENG
        /// <summary>
        /// Search by Content.ToString() from ContentControl collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ContentControl collection.</param>
        /// <param name="contentText">Content text.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// ContentをToString()で文字列化した文字列から要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="contentText">文字列。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByContentText<T>(this IWPFDependencyObjectCollection<T> collection, string contentText) where T : ContentControl
            => ContentControlSearcher.ByContentText<T>(collection, contentText);
    }
}