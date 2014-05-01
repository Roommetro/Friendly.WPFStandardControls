using System;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Properties;

namespace RM.Friendly.WPFStandardControls.Inside
{
    /// <summary>
    /// ローカライズ済みリソース。
    /// </summary>
    [Serializable]
    class ResourcesLocal4
    {
        static internal ResourcesLocal4 Instance;

        internal string DataGridErrorNotTextBoxCell;
        internal string DataGridErrorNotComboBoxCell;
        internal string DataGridErrorNotCheckBoxCell;
        internal string DataGridErrorHasNotTextProperty;
        internal string DataGridErrorNotSupportedStruct;

        /// <summary>
        /// 初期化。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        internal static void Initialize(WindowsAppFriend app)
        {
            Instance = new ResourcesLocal4();
            Instance.Initialize();
            app[typeof(ResourcesLocal4), "Instance"](Instance);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        void Initialize()
        {
            DataGridErrorNotTextBoxCell = Resources.DataGridErrorNotTextBoxCell;
            DataGridErrorNotComboBoxCell = Resources.DataGridErrorNotComboBoxCell;
            DataGridErrorNotCheckBoxCell = Resources.DataGridErrorNotCheckBoxCell;
            DataGridErrorHasNotTextProperty = Resources.DataGridErrorHasNotTextProperty;
            DataGridErrorNotSupportedStruct = Resources.DataGridErrorNotSupportedStruct;
        }
    }
}
