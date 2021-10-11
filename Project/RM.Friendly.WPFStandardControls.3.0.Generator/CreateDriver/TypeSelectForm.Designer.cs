namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    partial class TypeSelectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._listBox = new System.Windows.Forms.ListBox();
            this._buttonOK = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            this._dataGridViewEventName = new System.Windows.Forms.DataGridView();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewEventName)).BeginInit();
            this.SuspendLayout();
            // 
            // _listBox
            // 
            this._listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBox.FormattingEnabled = true;
            this._listBox.ItemHeight = 15;
            this._listBox.Location = new System.Drawing.Point(0, 0);
            this._listBox.Margin = new System.Windows.Forms.Padding(4);
            this._listBox.Name = "_listBox";
            this._listBox.Size = new System.Drawing.Size(372, 77);
            this._listBox.TabIndex = 0;
            this._listBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxMouseDoubleClick);
            // 
            // _buttonOK
            // 
            this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOK.Location = new System.Drawing.Point(176, 229);
            this._buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(100, 29);
            this._buttonOK.TabIndex = 1;
            this._buttonOK.Text = "OK";
            this._buttonOK.UseVisualStyleBackColor = true;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(284, 229);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(100, 29);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _splitContainer
            // 
            this._splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._splitContainer.Location = new System.Drawing.Point(12, 12);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._listBox);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._dataGridViewEventName);
            this._splitContainer.Panel2.Controls.Add(this.label1);
            this._splitContainer.Panel2.Controls.Add(this._textBoxFilter);
            this._splitContainer.Size = new System.Drawing.Size(372, 210);
            this._splitContainer.SplitterDistance = 77;
            this._splitContainer.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(225, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Filter";
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilter.Location = new System.Drawing.Point(271, 3);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(100, 22);
            this._textBoxFilter.TabIndex = 9;
            this._textBoxFilter.TextChanged += new System.EventHandler(this._textBoxFilter_TextChanged);
            // 
            // _dataGridViewEventName
            // 
            this._dataGridViewEventName.AllowUserToAddRows = false;
            this._dataGridViewEventName.AllowUserToDeleteRows = false;
            this._dataGridViewEventName.AllowUserToResizeRows = false;
            this._dataGridViewEventName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridViewEventName.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dataGridViewEventName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewEventName.ColumnHeadersVisible = false;
            this._dataGridViewEventName.Location = new System.Drawing.Point(0, 31);
            this._dataGridViewEventName.MultiSelect = false;
            this._dataGridViewEventName.Name = "_dataGridViewEventName";
            this._dataGridViewEventName.RowHeadersVisible = false;
            this._dataGridViewEventName.RowHeadersWidth = 51;
            this._dataGridViewEventName.RowTemplate.Height = 24;
            this._dataGridViewEventName.Size = new System.Drawing.Size(372, 98);
            this._dataGridViewEventName.TabIndex = 11;
            // 
            // TypeSelectForm
            // 
            this.AcceptButton = this._buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(396, 270);
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TypeSelectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Type / Event";
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.Panel2.PerformLayout();
            this._splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewEventName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _listBox;
        private System.Windows.Forms.Button _buttonOK;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxFilter;
        private System.Windows.Forms.DataGridView _dataGridViewEventName;
    }
}