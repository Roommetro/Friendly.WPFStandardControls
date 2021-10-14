
namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    partial class DriverCodeGeneratorControl
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
            this._dataGridViewEventName = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewEventName)).BeginInit();
            this.SuspendLayout();
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
            this._dataGridViewEventName.Location = new System.Drawing.Point(3, 34);
            this._dataGridViewEventName.MultiSelect = false;
            this._dataGridViewEventName.Name = "_dataGridViewEventName";
            this._dataGridViewEventName.RowHeadersVisible = false;
            this._dataGridViewEventName.RowHeadersWidth = 51;
            this._dataGridViewEventName.RowTemplate.Height = 24;
            this._dataGridViewEventName.Size = new System.Drawing.Size(329, 216);
            this._dataGridViewEventName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Filter";
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxFilter.Location = new System.Drawing.Point(172, 6);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(160, 22);
            this._textBoxFilter.TabIndex = 12;
            this._textBoxFilter.TextChanged += new System.EventHandler(this._textBoxFilter_TextChanged);
            // 
            // DriverCodeGeneratorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dataGridViewEventName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBoxFilter);
            this.Name = "DriverCodeGeneratorControl";
            this.Size = new System.Drawing.Size(335, 250);
            this.Load += new System.EventHandler(this.DriverCodeGeneratorControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewEventName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _dataGridViewEventName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxFilter;
    }
}
