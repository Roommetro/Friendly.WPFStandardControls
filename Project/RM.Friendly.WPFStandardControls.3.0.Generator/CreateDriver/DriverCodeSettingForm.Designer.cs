
namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    partial class DriverCodeSettingForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tabControlType = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._textBoxPreview = new System.Windows.Forms.TextBox();
            this._buttonOK = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._driverCodeDriverControl = new RM.Friendly.WPFStandardControls.Generator.CreateDriver.DriverCodeDriverControl();
            this._driverCodeGeneratorControl = new RM.Friendly.WPFStandardControls.Generator.CreateDriver.DriverCodeGeneratorControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._tabControlType.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tabControlType);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._textBoxPreview);
            this.splitContainer1.Size = new System.Drawing.Size(704, 302);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // _tabControlType
            // 
            this._tabControlType.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this._tabControlType.Controls.Add(this.tabPage1);
            this._tabControlType.Controls.Add(this.tabPage2);
            this._tabControlType.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControlType.Location = new System.Drawing.Point(0, 0);
            this._tabControlType.Margin = new System.Windows.Forms.Padding(2);
            this._tabControlType.Name = "_tabControlType";
            this._tabControlType.SelectedIndex = 0;
            this._tabControlType.Size = new System.Drawing.Size(384, 302);
            this._tabControlType.TabIndex = 1;
            this._tabControlType.SelectedIndexChanged += new System.EventHandler(this._tabControlType_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this._driverCodeDriverControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(376, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Driver";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this._driverCodeGeneratorControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(347, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Generator";
            // 
            // _textBoxPreview
            // 
            this._textBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBoxPreview.Location = new System.Drawing.Point(0, 0);
            this._textBoxPreview.Margin = new System.Windows.Forms.Padding(2);
            this._textBoxPreview.Multiline = true;
            this._textBoxPreview.Name = "_textBoxPreview";
            this._textBoxPreview.ReadOnly = true;
            this._textBoxPreview.Size = new System.Drawing.Size(317, 302);
            this._textBoxPreview.TabIndex = 0;
            // 
            // _buttonOK
            // 
            this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOK.Location = new System.Drawing.Point(596, 316);
            this._buttonOK.Margin = new System.Windows.Forms.Padding(2);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(56, 23);
            this._buttonOK.TabIndex = 1;
            this._buttonOK.Text = "OK";
            this._buttonOK.UseVisualStyleBackColor = true;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(657, 316);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(56, 23);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _driverCodeDriverControl
            // 
            this._driverCodeDriverControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._driverCodeDriverControl.Location = new System.Drawing.Point(2, 2);
            this._driverCodeDriverControl.Margin = new System.Windows.Forms.Padding(2);
            this._driverCodeDriverControl.Name = "_driverCodeDriverControl";
            this._driverCodeDriverControl.Size = new System.Drawing.Size(372, 272);
            this._driverCodeDriverControl.TabIndex = 16;
            // 
            // _driverCodeGeneratorControl
            // 
            this._driverCodeGeneratorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._driverCodeGeneratorControl.Location = new System.Drawing.Point(2, 2);
            this._driverCodeGeneratorControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._driverCodeGeneratorControl.Name = "_driverCodeGeneratorControl";
            this._driverCodeGeneratorControl.Size = new System.Drawing.Size(343, 272);
            this._driverCodeGeneratorControl.TabIndex = 1;
            // 
            // DriverCodeSettingForm
            // 
            this.AcceptButton = this._buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(722, 349);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOK);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DriverCodeSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DriverCodeSettingForm";
            this.Load += new System.EventHandler(this.DriverCodeSettingForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this._tabControlType.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox _textBoxPreview;
        private System.Windows.Forms.Button _buttonOK;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.TabControl _tabControlType;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private DriverCodeDriverControl _driverCodeDriverControl;
        private DriverCodeGeneratorControl _driverCodeGeneratorControl;
    }
}