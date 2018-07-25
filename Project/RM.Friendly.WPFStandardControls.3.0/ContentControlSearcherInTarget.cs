using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search for ContentControl.
    /// In order to run inside the target process, you will need to injection the RM.Friendly.WPFStandardControls.3.dll.
    /// Use the RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injection method.
    /// </summary>
#else
    /// <summary>
    /// ContentControlを検索するためのユーティリティー
    /// 対象プロセス内部で実行するためには、RM.Friendly.WPFStandardControls.3.dllをインジェクションする必要があります。
    /// RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injectionメソッドを利用してください。
    /// </summary>
#endif
    public static class ContentControlSearcherInTarget
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
        public static IEnumerable<T> ByContentText<T>(IEnumerable<T> collection, string contentText) where T : ContentControl
        {
            return CastUtility.CastList<DependencyObject, T>(ByContentTextCore(CastUtility.CastList<T, DependencyObject>(collection), contentText));
        }

        static IEnumerable<DependencyObject> ByContentTextCore(IEnumerable<DependencyObject> collection, string contentText)
        {
            List<DependencyObject> result = new List<DependencyObject>();
            foreach (ContentControl e in collection)
            {
                if (e.Content != null && e.Content.ToString() == contentText)
                {
                    result.Add(e);
                }
            }
            return result;
        }
    }
}
