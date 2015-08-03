using Codeer.Friendly;
using System.Collections.Generic;
using System.Windows.Controls;
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
    public static class ButtonSearcherExtensions
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
        public static IWPFDependencyObjectCollection<T> ByCommand<T>(this IWPFDependencyObjectCollection<T> collection, string ownerType, string name) where T: ButtonBase
        {
            return ButtonSearcher.ByCommand<T>(collection, ownerType, name);
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
        public static IWPFDependencyObjectCollection<T> ByCommand<T>(this IWPFDependencyObjectCollection<T> collection, RoutedCommand command) where T : ButtonBase
        {
            return ButtonSearcher.ByCommand<T>(collection, command);
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
        public static IWPFDependencyObjectCollection<T> ByCommandParameter<T>(this IWPFDependencyObjectCollection<T> collection, object commandParameter) where T : ButtonBase
        {
            return ButtonSearcher.ByCommandParameter<T>(collection, commandParameter);
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
        public static IWPFDependencyObjectCollection<T> ByCommandParameter<T>(this IWPFDependencyObjectCollection<T> collection, ExplicitAppVar commandParameter) where T : ButtonBase
        {
            return ButtonSearcher.ByCommandParameter<T>(collection, commandParameter);
        }

#if ENG
        /// <summary>
        /// Search by CommandParameter.ToString() from ButtonBase collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ButtonBase collection.</param>
        /// <param name="commandParameter">Command parameter.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// コマンドパラメータをToString()で文字列化した文字列から要素を検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="commandParameterText">文字列。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByCommandParameterText<T>(this IWPFDependencyObjectCollection<T> collection, string commandParameterText) where T : ButtonBase
        {
            return ButtonSearcher.ByCommandParameterText<T>(collection, commandParameterText);
        }

#if ENG
        /// <summary>
        /// Search by flag of IsCancel.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ButtonBase collection.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// IsCancelフラグが立っているボタンを検索。
        /// </summary>
        /// <typeparam name="T">コレクションのタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IWPFDependencyObjectCollection<T> ByIsCancel<T>(this IWPFDependencyObjectCollection<T> collection) where T : Button
        {
            return ButtonSearcher.ByIsCancel<T>(collection);
        }
    }
}
