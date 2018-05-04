namespace Dexter.SqlGui
{
    partial class FrmSql
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region [ Components ]

        private System.Windows.Forms.DataGridView grdTable;

        private System.Windows.Forms.Label lblConnStr;

        private System.Windows.Forms.Label lblQuery;

        private System.Windows.Forms.TextBox txtConnStr;

        private System.Windows.Forms.TextBox txtQuery;

        private System.Windows.Forms.Button btnGetData;

        private System.Windows.Forms.ComboBox cmbxConnTypes;

        private System.Windows.Forms.Label lblConnType;

        private System.Windows.Forms.TableLayoutPanel tblLytMain;

        private System.Windows.Forms.TableLayoutPanel tblLytSubPanel;

        private System.Windows.Forms.TableLayoutPanel tblLytButtons;

        private System.Windows.Forms.TabControl tbCtrlMain;

        private System.Windows.Forms.TabPage tbPgMain;

        private System.Windows.Forms.TabPage tbPgLog;

        private System.Windows.Forms.TextBox txtLog;

        private System.Windows.Forms.Button btnTestConnection;

        private System.Windows.Forms.TableLayoutPanel tblLytDetail;

        private System.Windows.Forms.RichTextBox txtValue;

        private System.Windows.Forms.TableLayoutPanel tblLytDetailColAndVal;

        private System.Windows.Forms.Label lblValue;

        private System.Windows.Forms.TableLayoutPanel tblLytCol;

        private System.Windows.Forms.Label lblCol;

        private System.Windows.Forms.TextBox txtColumn;

        private System.Windows.Forms.CheckBox chkRemoveBlobs;

        #endregion


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
            this.grdTable = new System.Windows.Forms.DataGridView();
            this.lblConnStr = new System.Windows.Forms.Label();
            this.lblQuery = new System.Windows.Forms.Label();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.cmbxConnTypes = new System.Windows.Forms.ComboBox();
            this.lblConnType = new System.Windows.Forms.Label();
            this.tblLytMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblLytButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.chkRemoveBlobs = new System.Windows.Forms.CheckBox();
            this.tblLytDetail = new System.Windows.Forms.TableLayoutPanel();
            this.tblLytSubPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tblLytDetailColAndVal = new System.Windows.Forms.TableLayoutPanel();
            this.txtValue = new System.Windows.Forms.RichTextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.tblLytCol = new System.Windows.Forms.TableLayoutPanel();
            this.lblCol = new System.Windows.Forms.Label();
            this.txtColumn = new System.Windows.Forms.TextBox();
            this.tbCtrlMain = new System.Windows.Forms.TabControl();
            this.tbPgMain = new System.Windows.Forms.TabPage();
            this.tbPgLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdTable)).BeginInit();
            this.tblLytMain.SuspendLayout();
            this.tblLytButtons.SuspendLayout();
            this.tblLytDetail.SuspendLayout();
            this.tblLytSubPanel.SuspendLayout();
            this.tblLytDetailColAndVal.SuspendLayout();
            this.tblLytCol.SuspendLayout();
            this.tbCtrlMain.SuspendLayout();
            this.tbPgMain.SuspendLayout();
            this.tbPgLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdTable
            // 
            this.grdTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTable.Location = new System.Drawing.Point(4, 4);
            this.grdTable.Margin = new System.Windows.Forms.Padding(4);
            this.grdTable.MultiSelect = false;
            this.grdTable.Name = "grdTable";
            this.grdTable.ReadOnly = true;
            this.grdTable.RowHeadersWidth = 10;
            this.grdTable.Size = new System.Drawing.Size(1778, 646);
            this.grdTable.TabIndex = 0;
            this.grdTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTable_CellClick);
            this.grdTable.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTable_CellEnter);
            // 
            // lblConnStr
            // 
            this.lblConnStr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnStr.AutoSize = true;
            this.lblConnStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblConnStr.Location = new System.Drawing.Point(10, 0);
            this.lblConnStr.Margin = new System.Windows.Forms.Padding(10, 0, 4, 0);
            this.lblConnStr.Name = "lblConnStr";
            this.lblConnStr.Size = new System.Drawing.Size(106, 35);
            this.lblConnStr.TabIndex = 1;
            this.lblConnStr.Text = "Connection String :";
            // 
            // lblQuery
            // 
            this.lblQuery.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQuery.AutoSize = true;
            this.lblQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblQuery.Location = new System.Drawing.Point(10, 83);
            this.lblQuery.Margin = new System.Windows.Forms.Padding(10, 0, 4, 0);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(69, 22);
            this.lblQuery.TabIndex = 1;
            this.lblQuery.Text = "Query :";
            // 
            // txtConnStr
            // 
            this.txtConnStr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtConnStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtConnStr.Location = new System.Drawing.Point(170, 4);
            this.txtConnStr.Margin = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.Size = new System.Drawing.Size(903, 27);
            this.txtConnStr.TabIndex = 2;
            // 
            // txtQuery
            // 
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtQuery.Location = new System.Drawing.Point(170, 39);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(903, 110);
            this.txtQuery.TabIndex = 2;
            // 
            // btnGetData
            // 
            this.btnGetData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGetData.Location = new System.Drawing.Point(825, 3);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(130, 28);
            this.btnGetData.TabIndex = 3;
            this.btnGetData.Text = "Get Data";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // cmbxConnTypes
            // 
            this.cmbxConnTypes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbxConnTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxConnTypes.FormattingEnabled = true;
            this.cmbxConnTypes.Location = new System.Drawing.Point(170, 156);
            this.cmbxConnTypes.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cmbxConnTypes.Name = "cmbxConnTypes";
            this.cmbxConnTypes.Size = new System.Drawing.Size(386, 28);
            this.cmbxConnTypes.TabIndex = 4;
            this.cmbxConnTypes.SelectedIndexChanged += new System.EventHandler(this.cmbxConnTypes_SelectedIndexChanged);
            // 
            // lblConnType
            // 
            this.lblConnType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnType.AutoSize = true;
            this.lblConnType.Location = new System.Drawing.Point(10, 153);
            this.lblConnType.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblConnType.Name = "lblConnType";
            this.lblConnType.Size = new System.Drawing.Size(147, 35);
            this.lblConnType.TabIndex = 5;
            this.lblConnType.Text = "Connection Type :";
            // 
            // tblLytMain
            // 
            this.tblLytMain.ColumnCount = 1;
            this.tblLytMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.Controls.Add(this.grdTable, 0, 0);
            this.tblLytMain.Controls.Add(this.tblLytButtons, 0, 2);
            this.tblLytMain.Controls.Add(this.tblLytDetail, 0, 1);
            this.tblLytMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytMain.Location = new System.Drawing.Point(3, 3);
            this.tblLytMain.Name = "tblLytMain";
            this.tblLytMain.RowCount = 3;
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblLytMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLytMain.Size = new System.Drawing.Size(1786, 894);
            this.tblLytMain.TabIndex = 6;
            // 
            // tblLytButtons
            // 
            this.tblLytButtons.ColumnCount = 5;
            this.tblLytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblLytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblLytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblLytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLytButtons.Controls.Add(this.btnTestConnection, 1, 0);
            this.tblLytButtons.Controls.Add(this.btnGetData, 2, 0);
            this.tblLytButtons.Controls.Add(this.chkRemoveBlobs, 3, 0);
            this.tblLytButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytButtons.Location = new System.Drawing.Point(3, 857);
            this.tblLytButtons.Name = "tblLytButtons";
            this.tblLytButtons.RowCount = 1;
            this.tblLytButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytButtons.Size = new System.Drawing.Size(1780, 34);
            this.tblLytButtons.TabIndex = 6;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTestConnection.Location = new System.Drawing.Point(662, 3);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(136, 28);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // chkRemoveBlobs
            // 
            this.chkRemoveBlobs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkRemoveBlobs.AutoSize = true;
            this.chkRemoveBlobs.Location = new System.Drawing.Point(973, 4);
            this.chkRemoveBlobs.Name = "chkRemoveBlobs";
            this.chkRemoveBlobs.Size = new System.Drawing.Size(154, 26);
            this.chkRemoveBlobs.TabIndex = 5;
            this.chkRemoveBlobs.Text = "Remove Blob Columns";
            this.chkRemoveBlobs.UseVisualStyleBackColor = true;
            // 
            // tblLytDetail
            // 
            this.tblLytDetail.ColumnCount = 2;
            this.tblLytDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 691F));
            this.tblLytDetail.Controls.Add(this.tblLytSubPanel, 0, 0);
            this.tblLytDetail.Controls.Add(this.tblLytDetailColAndVal, 1, 0);
            this.tblLytDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytDetail.Location = new System.Drawing.Point(3, 657);
            this.tblLytDetail.Name = "tblLytDetail";
            this.tblLytDetail.RowCount = 1;
            this.tblLytDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytDetail.Size = new System.Drawing.Size(1780, 194);
            this.tblLytDetail.TabIndex = 7;
            // 
            // tblLytSubPanel
            // 
            this.tblLytSubPanel.ColumnCount = 2;
            this.tblLytSubPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblLytSubPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytSubPanel.Controls.Add(this.cmbxConnTypes, 1, 2);
            this.tblLytSubPanel.Controls.Add(this.txtConnStr, 1, 0);
            this.tblLytSubPanel.Controls.Add(this.lblConnType, 0, 2);
            this.tblLytSubPanel.Controls.Add(this.txtQuery, 1, 1);
            this.tblLytSubPanel.Controls.Add(this.lblConnStr, 0, 0);
            this.tblLytSubPanel.Controls.Add(this.lblQuery, 0, 1);
            this.tblLytSubPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytSubPanel.Location = new System.Drawing.Point(3, 3);
            this.tblLytSubPanel.Name = "tblLytSubPanel";
            this.tblLytSubPanel.RowCount = 3;
            this.tblLytSubPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblLytSubPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytSubPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblLytSubPanel.Size = new System.Drawing.Size(1083, 188);
            this.tblLytSubPanel.TabIndex = 1;
            // 
            // tblLytDetailColAndVal
            // 
            this.tblLytDetailColAndVal.ColumnCount = 1;
            this.tblLytDetailColAndVal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytDetailColAndVal.Controls.Add(this.txtValue, 0, 2);
            this.tblLytDetailColAndVal.Controls.Add(this.lblValue, 0, 1);
            this.tblLytDetailColAndVal.Controls.Add(this.tblLytCol, 0, 0);
            this.tblLytDetailColAndVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytDetailColAndVal.Location = new System.Drawing.Point(1092, 3);
            this.tblLytDetailColAndVal.Name = "tblLytDetailColAndVal";
            this.tblLytDetailColAndVal.RowCount = 3;
            this.tblLytDetailColAndVal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLytDetailColAndVal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLytDetailColAndVal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytDetailColAndVal.Size = new System.Drawing.Size(685, 188);
            this.tblLytDetailColAndVal.TabIndex = 3;
            // 
            // txtValue
            // 
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Location = new System.Drawing.Point(3, 73);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(679, 112);
            this.txtValue.TabIndex = 2;
            this.txtValue.Text = "";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(3, 44);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(66, 22);
            this.lblValue.TabIndex = 3;
            this.lblValue.Text = "Value :";
            // 
            // tblLytCol
            // 
            this.tblLytCol.ColumnCount = 2;
            this.tblLytCol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblLytCol.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytCol.Controls.Add(this.lblCol, 0, 0);
            this.tblLytCol.Controls.Add(this.txtColumn, 1, 0);
            this.tblLytCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLytCol.Location = new System.Drawing.Point(3, 3);
            this.tblLytCol.Name = "tblLytCol";
            this.tblLytCol.RowCount = 1;
            this.tblLytCol.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLytCol.Size = new System.Drawing.Size(679, 34);
            this.tblLytCol.TabIndex = 4;
            // 
            // lblCol
            // 
            this.lblCol.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCol.AutoSize = true;
            this.lblCol.Location = new System.Drawing.Point(3, 0);
            this.lblCol.Name = "lblCol";
            this.lblCol.Size = new System.Drawing.Size(71, 34);
            this.lblCol.TabIndex = 0;
            this.lblCol.Text = "Column :";
            // 
            // txtColumn
            // 
            this.txtColumn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtColumn.Location = new System.Drawing.Point(83, 3);
            this.txtColumn.Name = "txtColumn";
            this.txtColumn.ReadOnly = true;
            this.txtColumn.Size = new System.Drawing.Size(587, 27);
            this.txtColumn.TabIndex = 1;
            // 
            // tbCtrlMain
            // 
            this.tbCtrlMain.Controls.Add(this.tbPgMain);
            this.tbCtrlMain.Controls.Add(this.tbPgLog);
            this.tbCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.tbCtrlMain.Name = "tbCtrlMain";
            this.tbCtrlMain.SelectedIndex = 0;
            this.tbCtrlMain.Size = new System.Drawing.Size(1800, 933);
            this.tbCtrlMain.TabIndex = 7;
            // 
            // tbPgMain
            // 
            this.tbPgMain.Controls.Add(this.tblLytMain);
            this.tbPgMain.Location = new System.Drawing.Point(4, 29);
            this.tbPgMain.Name = "tbPgMain";
            this.tbPgMain.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgMain.Size = new System.Drawing.Size(1792, 900);
            this.tbPgMain.TabIndex = 0;
            this.tbPgMain.Text = "Connection";
            this.tbPgMain.UseVisualStyleBackColor = true;
            // 
            // tbPgLog
            // 
            this.tbPgLog.Controls.Add(this.txtLog);
            this.tbPgLog.Location = new System.Drawing.Point(4, 29);
            this.tbPgLog.Name = "tbPgLog";
            this.tbPgLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgLog.Size = new System.Drawing.Size(1792, 900);
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
            this.txtLog.Size = new System.Drawing.Size(1786, 894);
            this.txtLog.TabIndex = 0;
            // 
            // FrmSql
            // 
            this.ClientSize = new System.Drawing.Size(1800, 933);
            this.Controls.Add(this.tbCtrlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmSql";
            this.Text = "Dapper Test App";
            ((System.ComponentModel.ISupportInitialize)(this.grdTable)).EndInit();
            this.tblLytMain.ResumeLayout(false);
            this.tblLytButtons.ResumeLayout(false);
            this.tblLytButtons.PerformLayout();
            this.tblLytDetail.ResumeLayout(false);
            this.tblLytSubPanel.ResumeLayout(false);
            this.tblLytSubPanel.PerformLayout();
            this.tblLytDetailColAndVal.ResumeLayout(false);
            this.tblLytDetailColAndVal.PerformLayout();
            this.tblLytCol.ResumeLayout(false);
            this.tblLytCol.PerformLayout();
            this.tbCtrlMain.ResumeLayout(false);
            this.tbPgMain.ResumeLayout(false);
            this.tbPgLog.ResumeLayout(false);
            this.tbPgLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

