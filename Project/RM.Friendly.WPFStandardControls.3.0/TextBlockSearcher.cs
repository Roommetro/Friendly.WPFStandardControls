using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
using System.Windows.Controls;

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
    public static class TextBlockSearcher
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
        public static IWPFDependencyObjectCollection<T> ByText<T>(IWPFDependencyObjectCollection<T> collection, string contentText) where T : TextBlock
        {
            var app = ((IAppVarOwner)collection).AppVar.App;
            WPFStandardControls_3.Injection((WindowsAppFriend)app);
            var ret = app[typeof(TextBlockSearcherInTarget), "ByTextCore"](AdjustCollectionTextBlock(collection, app), contentText);
            return new WPFDependencyObjectCollection<T>(ret);
        }

        static AppVar AdjustCollectionTextBlock<T>(IWPFDependencyObjectCollection<T> collection, AppFriend app) where T : TextBlock
        {
            //.net3.0対応
            var adjustCollection = app.Dim(new NewInfo<List<TextBlock>>());
            app[typeof(CastUtility), "CastList"](collection, adjustCollection);
            return adjustCollection;
        }
    }
}
