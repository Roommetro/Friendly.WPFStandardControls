using System;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Properties;

namespace RM.Friendly.WPFStandardControls.Inside
{
    /// <summary>
    /// ローカライズ済みリソース。
    /// </summary>
    [Serializable]
    public class ResourcesLocal4
    {
        static internal ResourcesLocal4 Instance;

        /// <summary>
        /// DataGridErrorNotTextBoxCell
        /// </summary>
        public string DataGridErrorNotTextBoxCell { get; set; }

        /// <summary>
        /// DataGridErrorNotComboBoxCell
        /// </summary>
        public string DataGridErrorNotComboBoxCell { get; set; }

        /// <summary>
        /// DataGridErrorNotCheckBoxCell
        /// </summary>
        public string DataGridErrorNotCheckBoxCell { get; set; }

        /// <summary>
        /// DataGridErrorHasNotTextProperty
        /// </summary>
        public string DataGridErrorHasNotTextProperty { get; set; }

        /// <summary>
        /// DataGridErrorNotSupportedItems
        /// </summary>
        public string DataGridErrorNotSupportedItems { get; set; }

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
            DataGridErrorNotSupportedItems = Resources.DataGridErrorNotSupportedItems;
        }
    }
}
