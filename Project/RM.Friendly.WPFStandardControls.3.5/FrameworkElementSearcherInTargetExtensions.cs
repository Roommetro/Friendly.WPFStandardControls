using Codeer.Friendly.DotNetExecutor;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search for FrameworkElement.
    /// In order to run inside the target process, you will need to injection the RM.Friendly.WPFStandardControls.3.dll.
    /// Use the RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injection method.
    /// </summary>
#else
    /// <summary>
    /// FrameworkElementを検索するためのユーティリティー
    /// 対象プロセス内部で実行するためには、RM.Friendly.WPFStandardControls.3.dllをインジェクションする必要があります。
    /// RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injectionメソッドを利用してください。
    /// </summary>
#endif
    public static class FrameworkElementSearcherInTargetExtensions
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
        public static IEnumerable<T> ByName<T>(this IEnumerable<T> collection, string name) where T : FrameworkElement
        {
            return FrameworkElementSearcherInTarget.ByName<T>(collection, name);
        }
    }
}
