using Codeer.Friendly.DotNetExecutor;
using RM.Friendly.WPFStandardControls.Inside;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search for ButtonBase.
    /// In order to run inside the target process, you will need to injection the RM.Friendly.WPFStandardControls.3.dll.
    /// Use the RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injection method.
    /// </summary>
#else
    /// <summary>
    /// ボタンを検索するためのユーティリティー
    /// 対象プロセス内部で実行するためには、RM.Friendly.WPFStandardControls.3.dllをインジェクションする必要があります。
    /// RM.Friendly.WPFStandardControls.WPFStandardControls_3.Injectionメソッドを利用してください。
    /// </summary>
#endif
    public static class ButtonSearcherInTarget
    {
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
        public static IEnumerable<T> ByCommand<T>(IEnumerable<T> collection, ICommand command) where T : ButtonBase
        {
            return CastUtility.CastList<DependencyObject, T>(ByCommandCore(CastUtility.CastList<T, DependencyObject>(collection), command));
        }

        static IEnumerable<DependencyObject> ByCommandCore(IEnumerable<DependencyObject> collection, ICommand command)
        {
            List<DependencyObject> result = new List<DependencyObject>();
            foreach (ButtonBase e in collection)
            {
                if (ReferenceEquals(command, e.Command))
                {
                    result.Add(e);
                }
            }
            return result;
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
        public static IEnumerable<T> ByCommandParameter<T>(IEnumerable<T> collection, object commandParameter) where T : ButtonBase
        {
            return CastUtility.CastList<DependencyObject, T>(ByCommandParameterCore(CastUtility.CastList<T, DependencyObject>(collection), commandParameter));
        }

        static IEnumerable<DependencyObject> ByCommandParameterCore(IEnumerable<DependencyObject> collection, object commandParameter)
        {
            List<DependencyObject> result = new List<DependencyObject>();
            foreach (ButtonBase e in collection)
            {
                if ((commandParameter == null && e.CommandParameter == null) ||
                    commandParameter.Equals(e.CommandParameter))
                {
                    result.Add(e);
                }
            }
            return result;
        }

#if ENG
        /// <summary>
        /// Search by CommandParameter.ToString() from ButtonBase collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">ButtonBase collection.</param>
        /// <param name="commandParameterText">Command parameter.</param>
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
        public static IEnumerable<T> ByCommandParameterText<T>(IEnumerable<T> collection, string commandParameterText) where T : ButtonBase
        {
            return CastUtility.CastList<DependencyObject, T>(ByCommandParameterTextCore(CastUtility.CastList<T, DependencyObject>(collection), commandParameterText));
        }

        static IEnumerable<DependencyObject> ByCommandParameterTextCore(IEnumerable<DependencyObject> collection, string commandParameterText)
        {
            List<DependencyObject> result = new List<DependencyObject>();
            foreach (ButtonBase e in collection)
            {
                if (e.CommandParameter != null && e.CommandParameter.ToString() == commandParameterText)
                {
                    result.Add(e);
                }
            }
            return result;
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
        public static IEnumerable<T> ByIsCancel<T>(IEnumerable<T> collection) where T : Button
        {
            return CastUtility.CastList<DependencyObject, T>(ByIsCancelCore(CastUtility.CastList<T, DependencyObject>(collection)));
        }

        static IEnumerable<DependencyObject> ByIsCancelCore(IEnumerable<DependencyObject> collection)
        {
            List<DependencyObject> result = new List<DependencyObject>();
            foreach (Button e in collection)
            {
                if (e.IsCancel)
                {
                    result.Add(e);
                }
            }
            return result;
        }
    }
}
