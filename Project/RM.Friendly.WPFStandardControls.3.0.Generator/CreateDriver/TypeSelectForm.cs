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

            {
                var viewColumn = new DataGridViewColumn();
                viewColumn.HeaderText = "Name";
                viewColumn.CellTemplate = new CheckBoxAndTextCell();
                _dataGridViewEventName.Columns.Add(viewColumn);
            }
        }

        public void SetTypeList(Type[] types)
        {
            _listBox.Items.AddRange(types);
            _listBox.SelectedIndex = 0;
        }

        public void AddEventName(string name)
        {
            var row = new DataGridViewRow();
            {
                var cell = new CheckBoxAndTextCell();
                cell.Text = name;
                cell.Value = false;
                row.Cells.Add(cell);
            }
            _dataGridViewEventName.Rows.Add(row);
        }

        public string[] GetSelectedEventName()
        {
            var dst = new List<string>();

            foreach (DataGridViewRow row in _dataGridViewEventName.Rows)
            {
                var item = row.Cells[0] as CheckBoxAndTextCell;
                if ((bool)item.Value != true)
                {
                    continue;
                }
                dst.Add(item.Text);
            }

            return (dst.Count <= 0) ? null : dst.ToArray();
        }

        void ListBoxMouseDoubleClick(object sender, MouseEventArgs e)
            => DialogResult = DialogResult.OK;

        private void _textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            SetVisibleRow(_dataGridViewEventName, _textBoxFilter.Text);
        }

        /// <summary>
        /// フィルタの内容に応じて行を表示するか決める
        /// </summary>
        /// <param name="grid">対象グリッド</param>
        /// <param name="filterText">フィルタテキスト</param>
        void SetVisibleRow(DataGridView grid, string filterText)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                bool visible = true;
                if (!string.IsNullOrEmpty(filterText))
                {
                    var cell = row.Cells[0] as CheckBoxAndTextCell;
                    visible = 0 <= cell.Text.IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase);
                }
                row.Visible = visible;
            }
        }
    }
}
