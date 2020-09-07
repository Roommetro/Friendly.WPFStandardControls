using Codeer.Friendly;
using System.Windows;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Collection of DependencyObject in target app.
    /// </summary>
#else
    /// <summary>
    /// DependencyObjectのコレクションです。
    /// </summary>
#endif
    public interface IWPFDependencyObjectCollection<out T> : IAppVarOwner where T : DependencyObject
    {
#if ENG
        /// <summary>
        /// Count.
        /// </summary>
#else
        /// <summary>
        /// コレクションの数。
        /// </summary>
#endif
        int Count { get; }

#if ENG
        /// <summary>
        /// DependencyObject of index in target app .
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>DependencyObject of index in target app .</returns>
#else
        /// <summary>
        /// 対象プロセス内での指定のインデックスのDependencyObject。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>対象プロセス内での指定のインデックスのDependencyObject。</returns>
#endif
        AppVar this[int index] { get; }

#if ENG
        /// <summary>
        /// Get only one item.
        /// </summary>
        /// <returns></returns>
#else
        /// <summary>
        /// コレクションの要素が一つであることを確認してそれを取得する。
        /// </summary>
        /// <returns>DependencyObject.</returns>
#endif
        AppVar Single();

#if ENG
        /// <summary>
        /// Single or Default.
        /// </summary>
        /// <returns>DependencyObject.</returns>
#else
        /// <summary>
        /// コレクションの要素が一つであることを確認してそれを取得する。なければnullが返る。
        /// 複数ある場合は例外が発生する。
        /// </summary>
        /// <returns>DependencyObject.</returns>
#endif
        AppVar SingleOrDefault();

#if ENG
        /// <summary>
        /// First
        /// </summary>
        /// <returns>DependencyObject.</returns>
#else
        /// <summary>
        /// コレクションの要素の一つ目を取得する。
        /// </summary>
        /// <returns>DependencyObject.</returns>
#endif
        AppVar First();

#if ENG
        /// <summary>
        /// First or Default.
        /// </summary>
        /// <returns>DependencyObject.</returns>
#else
        /// <summary>
        /// コレクションの要素の一つ目を取得する。なければnullが返る。
        /// </summary>
        /// <returns>DependencyObject.</returns>
#endif
        AppVar FirstOrDefault();

#if ENG
        /// <summary>
        /// ToArray.
        /// If there are a large number of elements, they are heavy, so filter them according to the conditions and reduce them sufficiently before using.
        /// </summary>
        /// <returns>AppVar[].</returns>
#else
        /// <summary>
        /// AppVarの配列が返る。要素が大量にある場合は重いので条件でフィルタして十分に少なくしてから使うこと。
        /// </summary>
        /// <returns>AppVar[].</returns>
#endif
        AppVar[] ToArray();
    }
}
