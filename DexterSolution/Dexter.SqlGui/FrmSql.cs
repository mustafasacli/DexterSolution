//using Dexter.ConnectionExtensions;
using Dexter.Extensions;
using Dexter.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dexter.SqlGui
{
    public partial class FrmSql : Form
    {
        public FrmSql()
        {
            InitializeComponent();
            this.LoadCombo();
        }

        private void arrangeData(DataTable dt, bool includeBlobs = false)
        {
            if (includeBlobs)
            {
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        List<string> strs = new List<string>();
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (column.DataType == typeof(byte[]))
                            {
                                strs.Add(column.ColumnName);
                            }
                        }
                        foreach (string str in strs)
                        {
                            dt.Columns.Remove(str);
                        }
                    }
                }
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.GetData();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtConnStr.Text))
            {
                try
                {
                    using (IDbConnection connection = DxConnectionFactory.Instance.GetConnection(this.cmbxConnTypes.GetItemText(cmbxConnTypes.SelectedItem)))
                    {
                        connection.ConnectionString = this.txtConnStr.Text;
                        connection.Open();
                        connection.Close();
                    }
                    MessageBox.Show("Connection successed.");
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show(string.Format("Message : {0}\nStack Trace : {1}", exception.Message, exception.StackTrace));
                }
            }
            else
            {
                MessageBox.Show("Connection string should be defined.");
            }
        }

        private void cmbxConnTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.connType = (this.cmbxConnTypes.SelectedIndex < 0 ? ConnectionTypes.Sql : (ConnectionTypes)this.cmbxConnTypes.SelectedItem);
        }

        private void GetData()
        {
            DataTable table = null;
            this.grdTable.DataSource = table;
            this.grdTable.Refresh();
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtConnStr.Text))
                {
                    MessageBox.Show("Connection string should be Defined.");
                    return;
                }
                else if (this.txtQuery.Text.Length > 0)
                {
                    table = this.GetTableAsync(this.cmbxConnTypes.GetItemText(cmbxConnTypes.SelectedItem), this.txtConnStr.Text, this.txtQuery.Text);
                    //table = this.GetTable(this.cmbxConnTypes.GetItemText(cmbxConnTypes.SelectedItem), this.txtConnStr.Text, this.txtQuery.Text);
                }
            }
            catch (Exception exception1)
            {
                //Exception exception = exception1;
                MessageBox.Show(string.Format("Message : {0}\nStack Trace : {1}", exception1.Message, exception1.StackTrace));
            }
            if (this.chkRemoveBlobs.Checked)
            {
                this.arrangeData(table, this.chkRemoveBlobs.Checked);
            }
            this.grdTable.DataSource = table;
            this.grdTable.Refresh();
        }

        public DataTable GetTable(string ConnType, string connectionString, string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (IDbConnection connection = DxConnectionFactory.Instance.GetConnection(ConnType))
                {
                    connection.ConnectionString = connectionString;
                    dataTable = connection.GetResultSet(query, CommandType.Text).Tables[0];
                }
            }
            catch (Exception exception)
            {
                throw;
            }
            return dataTable;
        }

        public DataTable GetTableAsync(string ConnType, string connectionString, string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (IDbConnection connection = DxConnectionFactory.Instance.GetConnection(ConnType))
                {
                    connection.ConnectionString = connectionString;
                    dataTable = connection.GetResultSetAsync(query, CommandType.Text).Result.Tables[0];
                }
            }
            catch (Exception exception)
            {
                throw;
            }
            return dataTable;
        }

        private void grdTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.loadColAndValue(e.ColumnIndex);
        }

        private void grdTable_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.loadColAndValue(e.ColumnIndex);
        }

        private void loadColAndValue(int colIndex)
        {
            this.txtValue.Text = string.Empty;
            this.txtColumn.Text = string.Empty;
            if (this.grdTable.SelectedCells.Count > 0)
            {
                this.txtColumn.Text = this.grdTable.Columns[colIndex].HeaderText;
                this.txtValue.Text = string.Format("{0}", this.grdTable.SelectedCells[0].Value);
            }
        }

        private void LoadCombo()
        {
            try
            {
                this.cmbxConnTypes.Items.Clear();
                this.cmbxConnTypes.Items.AddRange(DxConnectionFactory.Instance.ConnectionKeys.ToArray());

                this.cmbxConnTypes.Refresh();
                this.cmbxConnTypes.SelectedIndex = (this.cmbxConnTypes.Items.Count > 0 ? 1 : -1);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(string.Format("Message : {0}\nStack Trace : {1}", exception.Message, exception.StackTrace));
            }
        }
    }
}