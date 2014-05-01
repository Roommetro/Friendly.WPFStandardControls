using System;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Properties;

namespace RM.Friendly.WPFStandardControls.Inside
{
    /// <summary>
    /// ローカライズ済みリソース。
    /// </summary>
    [Serializable]
    class ResourcesLocal3
    {
        static internal ResourcesLocal3 Instance;

        internal string ErrorNotFoundItem;
        internal string ErrorNotFoundElement;

        /// <summary>
        /// 初期化。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        internal static void Initialize(WindowsAppFriend app)
        {
            Instance = new ResourcesLocal3();
            Instance.Initialize();
            app[typeof(ResourcesLocal3), "Instance"](Instance);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        void Initialize()
        {
            ErrorNotFoundItem = Resources.ErrorNotFoundItem;
            ErrorNotFoundElement = Resources.ErrorNotFoundElement;
        }
    }
}
