namespace Dexter.ConnectionExtensions
{
    using Dexter.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class ConnectionExtension
    {
        #region [ Execute method ]

        public static int Execute(this IDbConnection mConn,
            string sql,
            CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            int res = 0;

            try
            {
                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    Dictionary<string, object> inputs =
                        inputArgs ?? new Dictionary<string, object>();

                    Dictionary<string, object> outputs =
                        outputArgs ?? new Dictionary<string, object>();

                    //IDbDataParameter parameter;
                    foreach (var key in inputs)
                    {
                        var parameter = cmd.CreateParameter();
                        parameter.ParameterName = key.Key;
                        parameter.Value = key.Value;

                        parameter.Direction = outputs.ContainsKey(key.Key) ?
                            ParameterDirection.InputOutput : ParameterDirection.Input;
                        //parameter.DbType = DbType.Object;
                        cmd.Parameters.Add(parameter);
                    }

                    foreach (var key in outputs)
                    {
                        if (!inputs.ContainsKey(key.Key))
                        {
                            var parameter = cmd.CreateParameter();
                            parameter.ParameterName = key.Key;
                            parameter.Direction = ParameterDirection.Output;
                            parameter.DbType = DbType.Int64;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    res = cmd.ExecuteNonQuery();

                    if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                    {
                        IDbDataParameter prm;
                        outputArgs = new Dictionary<string, object>();
                        foreach (var item in cmd.Parameters)
                        {
                            prm = item as IDbDataParameter;
                            if (prm.Direction.IsMember(ParameterDirection.Output, ParameterDirection.InputOutput))
                            {
                                if (!prm.DbType.ToString().Contains("Cursor"))
                                    outputArgs[prm.ParameterName] = prm.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion [ Execute method ]

        #region [ ExecuteReader method ]

        public static IDataReader ExecuteReader(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            IDataReader reader = null;

            try
            {
                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    Dictionary<string, object> inputs =
                        inputArgs ?? new Dictionary<string, object>();

                    Dictionary<string, object> outputs =
                        outputArgs ?? new Dictionary<string, object>();
                    IDbDataParameter parameter;

                    foreach (var key in inputs)
                    {
                        parameter = cmd.CreateParameter();
                        parameter.ParameterName = key.Key;
                        parameter.Value = key.Value;

                        parameter.Direction = outputs.ContainsKey(key.Key) ?
                            ParameterDirection.InputOutput : ParameterDirection.Input;
                        cmd.Parameters.Add(parameter);
                    }

                    foreach (var key in outputs)
                    {
                        if (!inputs.ContainsKey(key.Key))
                        {
                            parameter = cmd.CreateParameter();
                            parameter.ParameterName = key.Key;
                            parameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    reader = cmd.ExecuteReader();

                    if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                    {
                        IDbDataParameter prm;
                        outputArgs = new Dictionary<string, object>();
                        foreach (var item in cmd.Parameters)
                        {
                            prm = item as IDbDataParameter;
                            if (prm.Direction.IsMember(ParameterDirection.Output, ParameterDirection.InputOutput))
                            {
                                outputArgs[prm.ParameterName] = prm.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return reader;
        }

        #endregion [ ExecuteReader method ]

        #region [ ExecuteScalar method ]

        public static object ExecuteScalar(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            object res = null;

            try
            {
                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    Dictionary<string, object> inputs =
                        inputArgs ?? new Dictionary<string, object>();

                    Dictionary<string, object> outputs =
                        outputArgs ?? new Dictionary<string, object>();

                    IDbDataParameter parameter;
                    foreach (var key in inputs)
                    {
                        parameter = cmd.CreateParameter();
                        parameter.ParameterName = key.Key;
                        parameter.Value = key.Value;

                        parameter.Direction = outputs.ContainsKey(key.Key) ?
                            ParameterDirection.InputOutput : ParameterDirection.Input;
                        cmd.Parameters.Add(parameter);
                    }

                    foreach (var key in outputs)
                    {
                        if (!inputs.ContainsKey(key.Key))
                        {
                            parameter = cmd.CreateParameter();
                            parameter.ParameterName = key.Key;
                            parameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    res = cmd.ExecuteScalar();

                    if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                    {
                        IDbDataParameter prm;
                        outputArgs = new Dictionary<string, object>();
                        foreach (var item in cmd.Parameters)
                        {
                            prm = item as IDbDataParameter;
                            if (prm.Direction.IsMember(ParameterDirection.Output, ParameterDirection.InputOutput))
                            {
                                outputArgs[prm.ParameterName] = prm.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion [ ExecuteScalar method ]

        #region [ GetResultSet method ]

        public static DataSet GetResultSet(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            DataSet ds = null;

            try
            {
                Type adapterTyp =
                    mConn.GetType().Assembly.GetExportedTypes().Where(
                        typ => typ.IsClass && typ.GetInterfaces().Contains(typeof(IDbDataAdapter))
                               && typ.IsAbstract == false && typeof(IDbDataAdapter).IsAssignableFrom(typ)).First();

                if (adapterTyp != null)
                {
                    IDbDataAdapter d = null;
                    d = Activator.CreateInstance(adapterTyp) as IDbDataAdapter;

                    using (IDbCommand cmd = mConn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = cmdType;

                        if (mTrans != null)
                            cmd.Transaction = mTrans;

                        Dictionary<string, object> inputs =
                            inputArgs ?? new Dictionary<string, object>();

                        Dictionary<string, object> outputs =
                            outputArgs ?? new Dictionary<string, object>();

                        IDbDataParameter parameter;
                        foreach (var key in inputs)
                        {
                            parameter = cmd.CreateParameter();
                            parameter.ParameterName = key.Key;
                            parameter.Value = key.Value;

                            parameter.Direction = outputs.ContainsKey(key.Key) ?
                                ParameterDirection.InputOutput : ParameterDirection.Input;
                            cmd.Parameters.Add(parameter);
                        }

                        foreach (var key in outputs)
                        {
                            if (!inputs.ContainsKey(key.Key))
                            {
                                parameter = cmd.CreateParameter();
                                parameter.ParameterName = key.Key;
                                parameter.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(parameter);
                            }
                        }

                        d.SelectCommand = cmd;
                        ds = new DataSet();
                        var result = d.Fill(ds);

                        if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                        {
                            IDbDataParameter prm;
                            outputArgs = new Dictionary<string, object>();
                            foreach (var item in cmd.Parameters)
                            {
                                prm = item as IDbDataParameter;
                                if (prm.Direction.IsMember(ParameterDirection.Output, ParameterDirection.InputOutput))
                                {
                                    outputArgs[prm.ParameterName] = prm.Value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return ds;
        }

        #endregion [ GetResultSet method ]

        #region [ GetDynamicResultSet method ]

        public static List<dynamic> GetDynamicResultSet(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            List<dynamic> list = new List<dynamic>();
            IDataReader reader = null;

            try
            {
                /*
                reader = ExecuteReader(mConn: mConn,
                    sql: sql, cmdType: cmdType, mTrans: mTrans,
                    inputArgs: inputArgs, outputArgs: outputArgs);
                */

                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    Dictionary<string, object> inputs =
                        inputArgs ?? new Dictionary<string, object>();

                    Dictionary<string, object> outputs =
                        outputArgs ?? new Dictionary<string, object>();
                    IDbDataParameter parameter;

                    foreach (var key in inputs)
                    {
                        parameter = cmd.CreateParameter();
                        parameter.ParameterName = key.Key;
                        parameter.Value = key.Value;

                        parameter.Direction = outputs.ContainsKey(key.Key) ?
                            ParameterDirection.InputOutput : ParameterDirection.Input;
                        cmd.Parameters.Add(parameter);
                    }

                    foreach (var key in outputs)
                    {
                        if (!inputs.ContainsKey(key.Key))
                        {
                            parameter = cmd.CreateParameter();
                            parameter.ParameterName = key.Key;
                            parameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    reader = cmd.ExecuteReader();

                    if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                    {
                        IDbDataParameter prm;
                        outputArgs = new Dictionary<string, object>();
                        foreach (var item in cmd.Parameters)
                        {
                            prm = item as IDbDataParameter;
                            if (prm.Direction.IsMember(ParameterDirection.Output, ParameterDirection.InputOutput))
                            {
                                outputArgs[prm.ParameterName] = prm.Value;
                            }
                        }
                    }

                    list = reader.GetDynamicResultSet(closeAtFinal: true);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetDynamicResultSet method ]

        #region [ GetDynamicResultSetWithPaging method ]

        public static List<dynamic> GetDynamicResultSetWithPaging(this IDbConnection mConn,
            string sql, CommandType cmdType,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null,
            uint pageNumber = 1, uint pageItemCount = 10)
        {
            List<dynamic> list = new List<dynamic>();
            IDataReader reader = null;

            try
            {
                /*
                reader = ExecuteReader(mConn: mConn,
                    sql: sql, cmdType: cmdType, mTrans: mTrans,
                    inputArgs: inputArgs, outputArgs: outputArgs);
                */

                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    Dictionary<string, object> inputs =
                        inputArgs ?? new Dictionary<string, object>();

                    Dictionary<string, object> outputs =
                        outputArgs ?? new Dictionary<string, object>();
                    IDbDataParameter parameter;

                    foreach (var key in inputs)
                    {
                        parameter = cmd.CreateParameter();
                        parameter.ParameterName = key.Key;
                        parameter.Value = key.Value;

                        parameter.Direction = outputs.ContainsKey(key.Key) ?
                            ParameterDirection.InputOutput : ParameterDirection.Input;
                        cmd.Parameters.Add(parameter);
                    }

                    foreach (var key in outputs)
                    {
                        if (!inputs.ContainsKey(key.Key))
                        {
                            parameter = cmd.CreateParameter();
                            parameter.ParameterName = key.Key;
                            parameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    reader = cmd.ExecuteReader();

                    if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                    {
                        IDbDataParameter prm;
                        outputArgs = new Dictionary<string, object>();
                        foreach (var item in cmd.Parameters)
                        {
                            prm = item as IDbDataParameter;
                            if (prm.Direction.IsMember(ParameterDirection.Output, ParameterDirection.InputOutput))
                            {
                                outputArgs[prm.ParameterName] = prm.Value;
                            }
                        }
                    }

                    list = reader.GetDynamicResultSetWithPaging(pageNumber: pageNumber, pageItemCount: pageItemCount, closeAtFinal: false);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetDynamicResultSetWithPaging method ]

        #region [ GetMultiDynamicResultSet method ]

        public static List<List<dynamic>> GetMultiDynamicResultSet(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            List<List<dynamic>> list = new List<List<dynamic>>();
            IDataReader reader = null;

            try
            {
                /*
                reader = ExecuteReader(mConn: mConn,
                    sql: sql, cmdType: cmdType, mTrans: mTrans,
                    inputArgs: inputArgs, outputArgs: outputArgs);
                */

                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    Dictionary<string, object> inputs =
                        inputArgs ?? new Dictionary<string, object>();

                    Dictionary<string, object> outputs =
                        outputArgs ?? new Dictionary<string, object>();
                    IDbDataParameter parameter;

                    foreach (var key in inputs)
                    {
                        parameter = cmd.CreateParameter();
                        parameter.ParameterName = key.Key;
                        parameter.Value = key.Value;

                        parameter.Direction = outputs.ContainsKey(key.Key) ?
                            ParameterDirection.InputOutput : ParameterDirection.Input;
                        cmd.Parameters.Add(parameter);
                    }

                    foreach (var key in outputs)
                    {
                        if (!inputs.ContainsKey(key.Key))
                        {
                            parameter = cmd.CreateParameter();
                            parameter.ParameterName = key.Key;
                            parameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    reader = cmd.ExecuteReader();

                    if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                    {
                        IDbDataParameter prm;
                        outputArgs = new Dictionary<string, object>();
                        foreach (var item in cmd.Parameters)
                        {
                            prm = item as IDbDataParameter;
                            if (prm.Direction.IsMember(ParameterDirection.Output, ParameterDirection.InputOutput))
                            {
                                outputArgs[prm.ParameterName] = prm.Value;
                            }
                        }
                    }

                    list = reader.GetMultiDynamicResultSet();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetMultiDynamicResultSet method ]
    }
}