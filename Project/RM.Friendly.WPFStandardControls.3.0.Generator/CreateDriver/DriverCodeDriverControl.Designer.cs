
namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    partial class DriverCodeDriverControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._textBoxFilterProperty = new System.Windows.Forms.TextBox();
            this._dataGridViewProperty = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this._textBoxFilterMethod = new System.Windows.Forms.TextBox();
            this._dataGridViewMethod = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewMethod)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Property / Field";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this._textBoxFilterProperty);
            this.splitContainer1.Panel1.Controls.Add(this._dataGridViewProperty);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this._textBoxFilterMethod);
            this.splitContainer1.Panel2.Controls.Add(this._dataGridViewMethod);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(352, 291);
            this.splitContainer1.SplitterDistance = 146;
            this.splitContainer1.TabIndex = 1;
            // 
            // _textBoxFilterProperty
            // 
            this._textBoxFilterProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilterProperty.Location = new System.Drawing.Point(191, 1);
            this._textBoxFilterProperty.Name = "_textBoxFilterProperty";
            this._textBoxFilterProperty.Size = new System.Drawing.Size(160, 22);
            this._textBoxFilterProperty.TabIndex = 2;
            this._textBoxFilterProperty.TextChanged += new System.EventHandler(this._textBoxFilterProperty_TextChanged);
            // 
            // _dataGridViewProperty
            // 
            this._dataGridViewProperty.AllowUserToAddRows = false;
            this._dataGridViewProperty.AllowUserToDeleteRows = false;
            this._dataGridViewProperty.AllowUserToResizeRows = false;
            this._dataGridViewProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridViewProperty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewProperty.Location = new System.Drawing.Point(0, 29);
            this._dataGridViewProperty.MultiSelect = false;
            this._dataGridViewProperty.Name = "_dataGridViewProperty";
            this._dataGridViewProperty.RowHeadersVisible = false;
            this._dataGridViewProperty.RowHeadersWidth = 51;
            this._dataGridViewProperty.RowTemplate.Height = 24;
            this._dataGridViewProperty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._dataGridViewProperty.Size = new System.Drawing.Size(352, 115);
            this._dataGridViewProperty.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Filter";
            // 
            // _textBoxFilterMethod
            // 
            this._textBoxFilterMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilterMethod.Location = new System.Drawing.Point(191, 1);
            this._textBoxFilterMethod.Name = "_textBoxFilterMethod";
            this._textBoxFilterMethod.Size = new System.Drawing.Size(160, 22);
            this._textBoxFilterMethod.TabIndex = 4;
            this._textBoxFilterMethod.TextChanged += new System.EventHandler(this._textBoxFilterMethod_TextChanged);
            // 
            // _dataGridViewMethod
            // 
            this._dataGridViewMethod.AllowUserToAddRows = false;
            this._dataGridViewMethod.AllowUserToDeleteRows = false;
            this._dataGridViewMethod.AllowUserToResizeRows = false;
            this._dataGridViewMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridViewMethod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewMethod.Location = new System.Drawing.Point(0, 29);
            this._dataGridViewMethod.MultiSelect = false;
            this._dataGridViewMethod.Name = "_dataGridViewMethod";
            this._dataGridViewMethod.RowHeadersVisible = false;
            this._dataGridViewMethod.RowHeadersWidth = 51;
            this._dataGridViewMethod.RowTemplate.Height = 24;
            this._dataGridViewMethod.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._dataGridViewMethod.Size = new System.Drawing.Size(352, 112);
            this._dataGridViewMethod.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Method";
            // 
            // DriverCodeDriverControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DriverCodeDriverControl";
            this.Size = new System.Drawing.Size(358, 297);
            this.Load += new System.EventHandler(this.DriverCodeDriverControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewMethod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox _textBoxFilterProperty;
        private System.Windows.Forms.DataGridView _dataGridViewProperty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _textBoxFilterMethod;
        private System.Windows.Forms.DataGridView _dataGridViewMethod;
        private System.Windows.Forms.Label label2;
    }
}
