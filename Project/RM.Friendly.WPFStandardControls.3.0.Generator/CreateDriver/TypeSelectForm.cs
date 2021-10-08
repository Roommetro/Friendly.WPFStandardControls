using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public partial class TypeSelectForm : Form
    {
        /// <summary>
        /// 設定種別
        /// </summary>
        public enum SettingType
        {
            None = 0,
            /// <summary>
            /// 型一覧を表示
            /// </summary>
            Type,
            /// <summary>
            /// イベント一覧を表示
            /// </summary>
            Event,
        }

        public Type SelectedType => (Type)_listBox.SelectedItem;

        public TypeSelectForm()
        {
            InitializeComponent();
        }

        public void SetSettingType(SettingType type)
        {
            bool visible1 = (type & SettingType.Type) != 0;
            bool visible2 = (type & SettingType.Event) != 0;

            _splitContainer.Panel1Collapsed = !visible1;
            _splitContainer.Panel2Collapsed = !visible2;

            // タイトル文言の設定
            var titleType = visible1 ? "Type" : "";
            titleType += (0 < titleType.Length) ? " / " : "";
            titleType += visible2 ? "Event" : "";
            Text = "Setting " + titleType + " - Driver generator setting";
        }

        public void SetTypeList(Type[] types)
        {
            _listBox.Items.AddRange(types);
            _listBox.SelectedIndex = 0;
        }

        public void AddEventName(string name)
        {
            _checkedListBoxEventName.Items.Add(name, false);
        }

        public string[] GetSelectedEventName()
        {
            var dst = new List<string>();

            foreach (var item in _checkedListBoxEventName.CheckedItems)
            {
                dst.Add(item.ToString());
            }

            return (dst.Count <= 0) ? null : dst.ToArray();
        }

        void ListBoxMouseDoubleClick(object sender, MouseEventArgs e)
            => DialogResult = DialogResult.OK;
    }
}
