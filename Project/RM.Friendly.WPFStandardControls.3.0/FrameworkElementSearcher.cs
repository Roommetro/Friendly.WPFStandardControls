using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
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
    public static class FrameworkElementSearcher
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
        public static IWPFDependencyObjectCollection<T> ByName<T>(IWPFDependencyObjectCollection<T> collection, string name) where T: FrameworkElement
        { 
            var app = ((IAppVarOwner)collection).AppVar.App;
            WPFStandardControls_3.Injection((WindowsAppFriend)app);
            var ret = app[typeof(FrameworkElementSearcherInTarget), "ByNameCore"](AdjustCollectionFrameworkElement(collection, app), name);
            return new WPFDependencyObjectCollection<T>(ret);
        }

        static AppVar AdjustCollectionFrameworkElement<T>(IWPFDependencyObjectCollection<T> collection, AppFriend app) where T : FrameworkElement
        {
            //.net3.0対応
            var adjustCollection = app.Dim(new NewInfo<List<FrameworkElement>>());
            app[typeof(CastUtility), "CastList"](collection, adjustCollection);
            return adjustCollection;
        }
    }
}
