using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dexter.AssemblyGui
{
    public partial class Form1 : Form
    {
        //string nodeFormat = "<add name=\"mysql{0}\" typename=\"MySql.Data.MySqlClient.MySqlConnection\" namespace=\"MySql.Data, Version=6.10.5.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d\" dialect=\"MySql\"/>";
        private string nodeFormat = "<add name=\"{0}\" typename=\"{1}\" namespace=\"{2}\" dialect=\"{3}\"/>";

        public Form1()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            btnGetFile.Enabled = false;
            //(new Thread(new ThreadStart(this.GetFile))).Start();
            GetFile();
        }

        private void GetFile()
        {
            try
            {
                txtConfigNode.ResetText();
                txtLog.ResetText();
                txtFileName.ResetText();

                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "dll files (*.dll)|*.dll" })
                {
                    DialogResult dr = ofd.ShowDialog();
                    if (dr == DialogResult.OK || dr == DialogResult.Yes)
                    {
                        txtFileName.ResetText();
                        txtConfigNode.ResetText();
                        txtFileName.Text = ofd.FileName;
                        Assembly asm = Assembly.LoadFile(ofd.FileName);
                        List<Type> types = asm.GetExportedTypes().Where(ty => ty.IsClass && ty.GetInterfaces().Contains(typeof(IDbConnection))
                                                              && ty.IsAbstract == false
                                                              && typeof(IDbConnection).IsAssignableFrom(ty)).ToList();
                        types = types ?? new List<Type>();
                        if (types.Count < 1)
                            txtConfigNode.AppendText("No IDbConnection has found.");

                        string s;
                        foreach (Type t in types)
                        {
                            s = t.Name;
                            s = s.Replace("Connection", string.Empty);
                            s = string.Format(nodeFormat, s.ToLowerInvariant(), t.FullName, asm.FullName, s);
                            txtConfigNode.AppendText(s);
                            txtConfigNode.AppendText(Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                txtConfigNode.AppendText("Error. Please Check the log.");
            }
            finally
            {
                btnGetFile.Enabled = true;
            }
        }

        private void LogError(Exception e)
        {
            try
            {
                txtLog.ResetText();
                DateTime dt = DateTime.Now;
                StackFrame frm = new StackFrame(1, true);
                MethodBase mthd = frm.GetMethod();
                int line = frm.GetFileLineNumber();
                int col = frm.GetFileColumnNumber();

                string assName = mthd.Module.Assembly.FullName;
                string className = mthd.ReflectedType.Name;
                string assFileName = frm.GetFileName();
                string methodName = mthd.Name;

                List<string> rows = new List<string>
                {
                    $"Time : {dt.ToString(AppGuiValues.GeneralDateFormat)}",
                    $"Assembly : {assName}",
                    $"Class : {className}",
                    $"Method Name : {methodName}",
                    $"Line : {line}",
                    $"Column : {col}",
                    $"Message : {e.Message}",
                    $"Stack Trace : {e.StackTrace}",
                    AppGuiValues.Lines
                };

                rows.ForEach(s => txtLog.AppendText(s + Environment.NewLine));
            }
            catch (Exception ee)
            {
            }
        }
    }
}