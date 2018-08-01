namespace DexterCfg.Factory
{
    using DexterCfg.Configuraton;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    /// <summary>
    /// Dexter Connection Factory class. Generates IDbConnection object instance with given connection name.
    /// </summary>
    public sealed class DxCfgConnectionFactory
    {
        private static Lazy<DxCfgConnectionFactory> instance =
            new Lazy<DxCfgConnectionFactory>(
            () => new DxCfgConnectionFactory());

        private Dictionary<string, Type> connObjs = null;

        private object lockErrObj = new object();
        private string errFileName;

        private object lockEvtObj = new object();
        private string evtFileName;

        /// <summary>
        /// private DxCfgConnectionFactory Ctor.
        /// </summary>
        private DxCfgConnectionFactory()
        {
            connObjs = new Dictionary<string, Type>();
            this.Errors = new List<Exception> { };
            AddTypes();
        }

        /// <summary>
        /// Gets DxCfgConnectionFactory instance for IDbConnection object list.
        /// </summary>
        public static DxCfgConnectionFactory Instance { get { return instance.Value; } }

        /// <summary>
        /// Error List.
        /// </summary>
        public List<Exception> Errors { get; private set; }

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
                            errFileName = DateTime.Now.ToString(AppCfgValues.ErrorFileDateFormat);
                            errFileName = string.Format(AppCfgValues.ErrorLogFileNameFormat, errFileName);
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
                            evtFileName = DateTime.Now.ToString(AppCfgValues.LogFileDateFormat);
                            evtFileName = string.Format(AppCfgValues.LogFileNameFormat, evtFileName);
                        }
                    }
                }

                return evtFileName;
            }
        }

        private void AddTypes()
        {
            XmlNodeList nodeList = DxCfgConfiguratonHelper.GetConnectionNodeList();

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
                    name = nod.Attributes[AppCfgValues.ConnectionNodeName].Value;
                    asm = Assembly.Load(nod.Attributes[AppCfgValues.AssemblyNodeName].Value);
                    typ = asm.GetType(nod.Attributes[AppCfgValues.TypeNodeName].Value);
                    if (typ.IsClass && typ.GetInterfaces().Contains(typeof(IDbConnection))
                                                        && typ.IsAbstract == false
                                                        && typeof(IDbConnection).IsAssignableFrom(typ))
                    {
                        connObjs[name] = typ;

                        if (DxCfgConfiguratonHelper.IsWriteEventLog)
                            LogEvent($"Connection Name : {name}", $"Assembly : {asm.FullName}", $"Type Name : {typ.FullName}");
                    }
                }
                catch (Exception ex)
                {
                    //Exception handling
                    if (DxCfgConfiguratonHelper.IsWriteErrorLog)
                        LogError(ex);
                    //throw;
                }
            }
        }

        /// <summary>
        /// Get IDbConnection from configuration file for given conection name.
        /// </summary>
        /// <param name="connName">Connection name</param>
        /// <returns>Returns IDbConnection instance for given name.</returns>
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
                if (DxCfgConfiguratonHelper.IsWriteErrorLog)
                    LogError(e);

                throw;
            }

            return conn;
        }

        /// <summary>
        /// Connection Name List in configuration file.
        /// </summary>
        public IList<string> ConnectionKeys
        {
            get
            {
                return connObjs?.Keys?.ToList() ?? new List<string> { };
            }
        }

        private void LogError(Exception e)
        {
            try
            {
                if (DxCfgConfiguratonHelper.IsWriteErrorLog)
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

                    string folderName = $"{AssemblyDirectory}/{AppCfgValues.ErrorFolderName}";

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
                        $"Time : {dt.ToString(AppCfgValues.GeneralDateFormat)}",
                        $"Assembly : {assName}",
                        $"Class : {className}",
                        $"Method Name : {methodName}",
                        $"Line : {line}",
                        $"Column : {col}",
                        $"Message : {e.Message}",
                        $"Stack Trace : {e.StackTrace}",
                        AppCfgValues.Lines
                    };

                    CfgFileOperator.Instance.Write(fileName, rows);
                }

            }
            catch (Exception ee)
            {
            }
        }

        private void LogEvent(params string[] messages)
        {
            try
            {
                if (DxCfgConfiguratonHelper.IsWriteEventLog)
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

                    string folderName = $"{AssemblyDirectory}/{AppCfgValues.EventFolderName}";

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
                    $"Time : {dt.ToString(AppCfgValues.GeneralDateFormat)}",
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

                    rows.Add(AppCfgValues.Lines);

                    CfgFileOperator.Instance.Write(fileName, rows);
                }

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