using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search to ButtonBase.
    /// </summary>
#else
    /// <summary>
    /// ボタンを検索するためのユーティリティー
    /// </summary>
#endif
    public static class ButtonSearcher
    {
#if ENG
        /// <summary>
        /// Search by Command from ButtonBase collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ButtonBase collection.</param>
        /// <param name="ownerType">Owner type.</param>
        /// <param name="name">Name.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// コマンドから要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="ownerType">オーナータイプ。</param>
        /// <param name="name">コマンド名。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByCommand<T>(IWPFDependencyObjectCollection<T> collection, string ownerType, string name) where T: ButtonBase
        {
            var app = ((IAppVarOwner)collection).AppVar.App;
            WPFStandardControls_3.Injection((WindowsAppFriend)app);
            var command = app[ownerType + "." + name]();
            var ret = app[typeof(ButtonSearcherInTarget), "ByCommandCore"](AdjustCollection(collection, app), command);
            return new WPFDependencyObjectCollection<T>(ret);
        }
#if ENG
        /// <summary>
        /// Search by Command from ButtonBase collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ButtonBase collection.</param>
        /// <param name="command">Command.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// コマンドから要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="command">コマンド。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByCommand<T>(IWPFDependencyObjectCollection<T> collection, RoutedCommand command) where T : ButtonBase
        {
            return ByCommand(collection, command.OwnerType.FullName, command.Name);
        }

#if ENG
        /// <summary>
        /// Search by CommandParameter from ButtonBase collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ButtonBase collection.</param>
        /// <param name="commandParameter">Command parameter.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// コマンドパラメータから要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="commandParameter">コマンドパラメータ。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByCommandParameter<T>(IWPFDependencyObjectCollection<T> collection, object commandParameter) where T : ButtonBase
        {
            var app = ((IAppVarOwner)collection).AppVar.App;
            WPFStandardControls_3.Injection((WindowsAppFriend)app);
            var ret = app[typeof(ButtonSearcherInTarget), "ByCommandParameterCore"](AdjustCollection(collection, app), commandParameter);
            return new WPFDependencyObjectCollection<T>(ret);
        }

#if ENG
        /// <summary>
        /// Search by CommandParameter from ButtonBase collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ButtonBase collection.</param>
        /// <param name="commandParameter">Command parameter.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// コマンドパラメータから要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="commandParameter">コマンドパラメータ。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByCommandParameter<T>(IWPFDependencyObjectCollection<T> collection, ExplicitAppVar commandParameter) where T : ButtonBase
        {
            return ByCommandParameter(collection, (object)commandParameter);
        }

        static AppVar AdjustCollection<T>(IWPFDependencyObjectCollection<T> collection, AppFriend app) where T : ButtonBase
        {
            //.net3.0対応
            var adjustCollection = app.Dim(new NewInfo<List<ButtonBase>>());
            app[typeof(CastUtility), "CastList"](collection, adjustCollection);
            return adjustCollection;
        }
    }
}
