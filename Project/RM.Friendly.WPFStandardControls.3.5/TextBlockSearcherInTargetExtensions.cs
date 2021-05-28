using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search for TextBlock.
    /// In order to run inside the target process, you will need to injection the RM.Friendly.WPFStandardControls.3.dll.
    /// Use the RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injection method.
    /// </summary>
#else
    /// <summary>
    /// TextBlockを検索するためのユーティリティー
    /// 対象プロセス内部で実行するためには、RM.Friendly.WPFStandardControls.3.dllをインジェクションする必要があります。
    /// RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injectionメソッドを利用してください。
    /// </summary>
#endif
    public static class TextBlockSearcherInTargetExtensions
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
        public static IEnumerable<T> ByText<T>(this IEnumerable<T> collection, string contentText) where T : TextBlock
            => TextBlockSearcherInTarget.ByText(collection, contentText);
    }
}