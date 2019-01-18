namespace Dexter.Auto
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Dexter Connection Factory class. Generates IDbConnection object with given connection name.
    /// </summary>
    public sealed class DxAutoConnectionFactory
    {
        private static Lazy<DxAutoConnectionFactory> instance = new Lazy<DxAutoConnectionFactory>(() => new DxAutoConnectionFactory());

        private Dictionary<string, Type> connObjs = null;
        private Dictionary<string, string> connStrPairs = null;

        private readonly object lockErrObj = new object();
        private string errFileName;

        private readonly object lockEvtObj = new object();
        private string evtFileName;

        /// <summary>
        /// private DxConnectionFactory Ctor.
        /// </summary>
        private DxAutoConnectionFactory()
        {
            connStrPairs = new Dictionary<string, string>();
            connObjs = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Gets DxConnectionFactory instance for IDbConnection object list.
        /// </summary>
        public static DxAutoConnectionFactory Instance
        { get { return instance.Value; } }

        /// <summary>
        /// Error List.
        /// </summary>
        public List<Exception> Errors
        { get; private set; }

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
                            errFileName = DateTime.Now.ToString(AutoAppValues.ErrorFileDateFormat);
                            errFileName = string.Format(AutoAppValues.ErrorLogFileNameFormat, errFileName);
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
                            evtFileName = DateTime.Now.ToString(AutoAppValues.LogFileDateFormat);
                            evtFileName = string.Format(AutoAppValues.LogFileNameFormat, evtFileName);
                        }
                    }
                }

                return evtFileName;
            }
        }

        public bool IsWriteErrorLog
        { get; set; }

        public bool IsWriteEventLog
        { get; set; }

        /// <summary>
        /// Registers IDbConnection with given conn name key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connName"></param>
        /// <param name="t"></param>
        public void Register<T>(string connName, T t) where T : class, IDbConnection
        {
            if (string.IsNullOrWhiteSpace(connName))
                throw new ArgumentException(nameof(connName));

            if (connObjs.ContainsKey(connName))
                throw new Exception($"Connection with \"{connName}\" name is already defined.");

            connObjs[connName] = typeof(T);
            connStrPairs[connName] = t.ConnectionString;
        }

        /// <summary>
        /// Get IDbConnection from configuration file for given connection name.
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
                    throw new Exception($"Connection with \"{connName}\" name should be defined.");

                Type t;
                t = connObjs[connName];

                conn = Activator.CreateInstance(t) as IDbConnection;
                conn.ConnectionString = connStrPairs[connName];
            }
            catch (Exception e)
            {
                //Exceptin handling
                if (this.IsWriteErrorLog)
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
                if (this.IsWriteErrorLog)
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

                    //string fileName = DateTime.Now.ToString(AutoAppValues.ErrorFileDateFormat);
                    //fileName = string.Format(AutoAppValues.ErrorLogFileNameFormat, fileName);
                    string folderName = $"{AssemblyDirectory}/{AutoAppValues.ErrorFolderName}";

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
                        $"Time : {dt.ToString(AutoAppValues.GeneralDateFormat)}",
                        $"Assembly : {assName}",
                        $"Class : {className}",
                        $"Method Name : {methodName}",
                        $"Line : {line}",
                        $"Column : {col}",
                        $"Message : {e.Message}",
                        $"Stack Trace : {e.StackTrace}",
                        AutoAppValues.Lines
                    };

                    FileOperator.Instance.Write(fileName, rows);
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
                if (this.IsWriteEventLog)
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

                    //string fileName = DateTime.Now.ToString(AutoAppValues.LogFileDateFormat);
                    //fileName = string.Format(AutoAppValues.LogFileNameFormat, fileName);
                    string folderName = $"{AssemblyDirectory}/{AutoAppValues.EventFolderName}";

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
                        $"Time : {dt.ToString(AutoAppValues.GeneralDateFormat)}",
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

                    rows.Add(AutoAppValues.Lines);

                    FileOperator.Instance.Write(fileName, rows);
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