using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
using System.Windows.Controls;

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
    public static class ContentControlSearcher
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
# endif
        public static IWPFDependencyObjectCollection<T> ByContentText<T>(IWPFDependencyObjectCollection<T> collection, string contentText) where T : ContentControl
        {
            var app = ((IAppVarOwner)collection).AppVar.App;
            WPFStandardControls_3.Injection((WindowsAppFriend)app);
            var ret = app[typeof(ContentControlSearcherInTarget), "ByContentTextCore"](AdjustCollectionContentControl(collection, app), contentText);
            return new WPFDependencyObjectCollection<T>(ret);
        }

        static AppVar AdjustCollectionContentControl<T>(IWPFDependencyObjectCollection<T> collection, AppFriend app) where T : ContentControl
        {
            //.net3.0対応
            var adjustCollection = app.Dim(new NewInfo<List<ContentControl>>());
            app[typeof(CastUtility), "CastList"](collection, adjustCollection);
            return adjustCollection;
        }
    }
}
