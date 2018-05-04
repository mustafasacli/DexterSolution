namespace Dexter.AssemblyGui
{
    partial class Form1
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
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtConfigNode = new System.Windows.Forms.TextBox();
            this.btnGetFile = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblConfigurationNode = new System.Windows.Forms.Label();
            this.tblLytMain = new System.Windows.Forms.TableLayoutPanel();
            this.tabCtrlMain = new System.Windows.Forms.TabControl();
            this.tbPgMain = new System.Windows.Forms.TabPage();
            this.tbPgLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tblLytUpper = new System.Windows.Forms.TableLayoutPanel();
            this.tblLytMain.SuspendLayout();
            this.tabCtrlMain.SuspendLayout();
            this.tbPgMain.SuspendLayout();
            this.tbPgLog.SuspendLayout();
            this.tblLytUpper.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.txtFileName.Location = new System.Drawing.Point(3, 63);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(506, 198);
            this.txtFileName.TabIndex = 0;
            // 
            // txtConfigNode
            // 
            this.txtConfigNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConfigNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.txtConfigNode.Location = new System.Drawing.Point(3, 327);
            this.txtConfigNode.Multiline = true;
            this.txtConfigNode.Name = "txtConfigNode";
            this.txtConfigNode.ReadOnly = true;
            this.txtConfigNode.Size = new System.Drawing.Size(506, 198);
            this.txtConfigNode.TabIndex = 1;
            // 
            // btnGetFile
            // 
            this.btnGetFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGetFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.btnGetFile.Location = new System.Drawing.Point(372, 3);
            this.btnGetFile.Name = "btnGetFile";
            this.btnGetFile.Size = new System.Drawing.Size(131, 48);
            this.btnGetFile.TabIndex = 2;
            this.btnGetFile.Text = "Get File";
            this.btnGetFile.UseVisualStyleBackColor = true;
            this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblFileName.Location = new System.Drawing.Point(3, 17);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(88, 20);
            this.lblFileName.TabIndex = 3;
            this.lblFileName.Text = "File Name :";
            // 
            // lblConfigurationNode
            // 
            this.lblConfigurationNode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConfigurationNode.AutoSize = true;
            this.lblConfigurationNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblConfigurationNode.Location = new System.Drawing.Point(3, 284);
            this.lblConfigurationNode.Name = "lblConfigurationNode";
            this.lblConfigurationNode.Size = new System.Drawing.Size(154, 20);
            this.lblConfigurationNode.TabIndex = 4;
            this.lblConfigurationNode.Text = "Configuration Node :";
            // 
            // tblLytMain
            // 
            this.tblLytMain.ColumnCount = 1;
            this.tblLytMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.Controls.Add(this.tblLytUpper, 0, 0);
            this.tblLytMain.Controls.Add(this.txtConfigNode, 0, 3);
            this.tblLytMain.Controls.Add(this.lblConfigurationNode, 0, 2);
            this.tblLytMain.Controls.Add(this.txtFileName, 0, 1);
            this.tblLytMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytMain.Location = new System.Drawing.Point(3, 3);
            this.tblLytMain.Name = "tblLytMain";
            this.tblLytMain.RowCount = 4;
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytMain.Size = new System.Drawing.Size(512, 528);
            this.tblLytMain.TabIndex = 5;
            // 
            // tabCtrlMain
            // 
            this.tabCtrlMain.Controls.Add(this.tbPgMain);
            this.tabCtrlMain.Controls.Add(this.tbPgLog);
            this.tabCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.tabCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlMain.Name = "tabCtrlMain";
            this.tabCtrlMain.SelectedIndex = 0;
            this.tabCtrlMain.Size = new System.Drawing.Size(526, 565);
            this.tabCtrlMain.TabIndex = 6;
            // 
            // tbPgMain
            // 
            this.tbPgMain.Controls.Add(this.tblLytMain);
            this.tbPgMain.Location = new System.Drawing.Point(4, 27);
            this.tbPgMain.Name = "tbPgMain";
            this.tbPgMain.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgMain.Size = new System.Drawing.Size(518, 534);
            this.tbPgMain.TabIndex = 0;
            this.tbPgMain.Text = "File";
            this.tbPgMain.UseVisualStyleBackColor = true;
            // 
            // tbPgLog
            // 
            this.tbPgLog.Controls.Add(this.txtLog);
            this.tbPgLog.Location = new System.Drawing.Point(4, 27);
            this.tbPgLog.Name = "tbPgLog";
            this.tbPgLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgLog.Size = new System.Drawing.Size(264, 358);
            this.tbPgLog.TabIndex = 1;
            this.tbPgLog.Text = "Log";
            this.tbPgLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(258, 352);
            this.txtLog.TabIndex = 0;
            // 
            // tblLytUpper
            // 
            this.tblLytUpper.ColumnCount = 2;
            this.tblLytUpper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytUpper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytUpper.Controls.Add(this.btnGetFile, 1, 0);
            this.tblLytUpper.Controls.Add(this.lblFileName, 0, 0);
            this.tblLytUpper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytUpper.Location = new System.Drawing.Point(3, 3);
            this.tblLytUpper.Name = "tblLytUpper";
            this.tblLytUpper.RowCount = 1;
            this.tblLytUpper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytUpper.Size = new System.Drawing.Size(506, 54);
            this.tblLytUpper.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 565);
            this.Controls.Add(this.tabCtrlMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tblLytMain.ResumeLayout(false);
            this.tblLytMain.PerformLayout();
            this.tabCtrlMain.ResumeLayout(false);
            this.tbPgMain.ResumeLayout(false);
            this.tbPgLog.ResumeLayout(false);
            this.tbPgLog.PerformLayout();
            this.tblLytUpper.ResumeLayout(false);
            this.tblLytUpper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtConfigNode;
        private System.Windows.Forms.Button btnGetFile;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblConfigurationNode;
        private System.Windows.Forms.TableLayoutPanel tblLytMain;
        private System.Windows.Forms.TabControl tabCtrlMain;
        private System.Windows.Forms.TabPage tbPgMain;
        private System.Windows.Forms.TabPage tbPgLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TableLayoutPanel tblLytUpper;
    }
}

