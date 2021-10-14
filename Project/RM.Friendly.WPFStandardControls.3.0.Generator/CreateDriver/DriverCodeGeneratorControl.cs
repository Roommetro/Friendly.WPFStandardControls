using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class DriverCodeGeneratorControl : UserControl
    {
        public delegate void UpdateCodeRequestDelegate();
        public UpdateCodeRequestDelegate DelegateUpdateCodeRequest = null;

        public DriverCodeGeneratorControl()
        {
            InitializeComponent();

            var viewColumn = new DataGridViewColumn();
            viewColumn.HeaderText = "Name";
            viewColumn.CellTemplate = new CheckBoxAndTextCell();
            _dataGridViewEventName.Columns.Add(viewColumn);
        }

        private void DriverCodeGeneratorControl_Load(object sender, EventArgs e)
        {
            _dataGridViewEventName.CurrentCellDirtyStateChanged += _dataGridViewEventName_CurrentCellDirtyStateChanged;
            _dataGridViewEventName.CellValueChanged += _dataGridViewEventName_CellValueChanged;
        }

        private void _textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            SetVisibleRow(_dataGridViewEventName, _textBoxFilter.Text);
        }

        private void _dataGridViewEventName_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (_dataGridViewEventName.CurrentCellAddress.X == 0 && _dataGridViewEventName.IsCurrentCellDirty)
            {
                _dataGridViewEventName.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void _dataGridViewEventName_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DelegateUpdateCodeRequest.Invoke();
            }
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
