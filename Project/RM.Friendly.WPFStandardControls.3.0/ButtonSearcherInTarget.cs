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
    }
}
