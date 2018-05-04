namespace Dexter.Factory
{
    using Dexter.Configuraton;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    public sealed class ConnectionFactory
    {
        private static Lazy<ConnectionFactory> instance = new Lazy<ConnectionFactory>(() => new ConnectionFactory());

        private Dictionary<string, Type> connObjs = null;

        private ConnectionFactory()
        {
            connObjs = new Dictionary<string, Type>();
            this.Errors = new List<Exception> { };
            AddTypes();
        }

        public static ConnectionFactory Instance { get { return instance.Value; } }

        public List<Exception> Errors { get; private set; }

        object lockErrObj = new object();
        string errFileName;

        object lockEvtObj = new object();
        string evtFileName;

        private string ErrorFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(errFileName))
                {
                    lock (lockErrObj)
                    {
                        if (string.IsNullOrWhiteSpace(errFileName))
                        {
                            errFileName = DateTime.Now.ToString(AppValues.ErrorFileDateFormat);
                            errFileName = string.Format(AppValues.ErrorLogFileNameFormat, errFileName);
                        }
                    }
                }

                return errFileName;
            }
        }

        private string EventFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(evtFileName))
                {
                    lock (lockEvtObj)
                    {
                        if (string.IsNullOrWhiteSpace(evtFileName))
                        {
                            evtFileName = DateTime.Now.ToString(AppValues.LogFileDateFormat);
                            evtFileName = string.Format(AppValues.LogFileNameFormat, evtFileName);
                        }
                    }
                }

                return evtFileName;
            }
        }



        private void AddTypes()
        {
            XmlNodeList nodeList = DxConfiguratonHelper.GetNodeList();

            if (nodeList == null)
                return;

            if (nodeList.Count < 1)
                return;

            string name;
            Assembly asm;
            Type typ;

            foreach (XmlNode nod in nodeList)
            {
                try
                {
                    name = nod.Attributes[AppValues.ConnectionNodeName].Value;// "name"].Value;
                    asm = Assembly.Load(nod.Attributes[AppValues.AssemblyNodeName].Value);// "namespace"].Value);
                    typ = asm.GetType(nod.Attributes[AppValues.TypeNodeName].Value);// "typename"].Value);
                    if (typ.IsClass && typ.GetInterfaces().Contains(typeof(IDbConnection))
                                                        && typ.IsAbstract == false
                                                        && typeof(IDbConnection).IsAssignableFrom(typ))
                    {
                        connObjs[name] = typ;
                        LogEvent($"Connection Name : {name}", $"Assembly : {asm.FullName}", $"Type Name : {typ.FullName}");
                    }
                }
                catch (Exception ex)
                {
                    //Exception handling
                    LogError(ex);
                    //throw;
                }
            }
        }

        public IDbConnection GetConnection(string connName)
        {
            IDbConnection conn = null;

            try
            {
                if (string.IsNullOrWhiteSpace(connName))
                    throw new ArgumentException(nameof(connName));

                if (!connObjs.ContainsKey(connName))
                    throw new Exception($"Connection with {connName} name should be defined.");

                Type t;
                t = connObjs[connName];

                conn = Activator.CreateInstance(t) as IDbConnection;
            }
            catch (Exception e)
            {
                //Exceptin handling
                LogError(e);
                throw;
            }

            return conn;
        }

        public IList<string> ConnectionKeys
        {
            get
            {
                return connObjs.Keys.ToList();
            }
        }

        private void LogError(Exception e)
        {
            try
            {
                DateTime dt = DateTime.Now;
                StackFrame frm = new StackFrame(1, true);
                MethodBase mthd = frm.GetMethod();
                int line = frm.GetFileLineNumber();
                int col = frm.GetFileColumnNumber();

                string assName = mthd.Module.Assembly.FullName;
                string className = mthd.ReflectedType.Name;
                string assFileName = frm.GetFileName();
                string methodName = mthd.Name;

                //string fileName = DateTime.Now.ToString(AppValues.ErrorFileDateFormat);
                //fileName = string.Format(AppValues.ErrorLogFileNameFormat, fileName);
                string folderName = $"{AssemblyDirectory}/{AppValues.ErrorFolderName}";

                try
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
                catch (Exception)
                {
                }
                string fileName = $"{folderName}/{ErrorFileName}";

                List<string> rows = new List<string>
                {
                    $"Time : {dt.ToString(AppValues.GeneralDateFormat)}",
                    $"Assembly : {assName}",
                    $"Class : {className}",
                    $"Method Name : {methodName}",
                    $"Line : {line}",
                    $"Column : {col}",
                    $"Message : {e.Message}",
                    $"Stack Trace : {e.StackTrace}",
                    AppValues.Lines
                };

                FileOperator.Instance.Write(fileName, rows);
            }
            catch (Exception ee)
            {
            }
        }

        private void LogEvent(params string[] messages)
        {
            try
            {
                DateTime dt = DateTime.Now;
                StackFrame frm = new StackFrame(1, true);
                MethodBase mthd = frm.GetMethod();
                int line = frm.GetFileLineNumber();
                int col = frm.GetFileColumnNumber();

                string assName = mthd.Module.Assembly.FullName;
                string className = mthd.ReflectedType.Name;
                string assFileName = frm.GetFileName();
                string methodName = mthd.Name;

                //string fileName = DateTime.Now.ToString(AppValues.LogFileDateFormat);
                //fileName = string.Format(AppValues.LogFileNameFormat, fileName);
                string folderName = $"{AssemblyDirectory}/{AppValues.EventFolderName}";

                try
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
                catch (Exception)
                {
                }
                string fileName = $"{folderName}/{EventFileName}";

                List<string> rows = new List<string>
                {
                    $"Time : {dt.ToString(AppValues.GeneralDateFormat)}",
                    $"Assembly : {assName}",
                    $"Class : {className}",
                    $"Method Name : {methodName}",
                    $"Line : {line}",
                    $"Column : {col}",
                    "Messages : ",
                };

                if (messages != null)
                    foreach (var item in messages)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                            rows.Add(item);
                    }

                rows.Add(AppValues.Lines);

                FileOperator.Instance.Write(fileName, rows);
            }
            catch (Exception e)
            {
            }
        }

        private static string AssemblyDirectory
        {
            get
            {
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                return dir;
            }
        }
    }
}
