namespace Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    public static class DxConnectionExtension
    {
        #region [ Execute method ]

        public static int Execute(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
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

                    res = cmd.ExecuteNonQuery();

                    if (outputArgs != null)
                    {
                        foreach (var key in outputArgs)
                        {
                            outputArgs[key.Key] =
                                (cmd.Parameters[key.Key] as IDbDataParameter)?.Value;
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

                    if (outputArgs != null)
                    {
                        foreach (var key in outputArgs)
                        {
                            outputArgs[key.Key] =
                                (cmd.Parameters[key.Key] as IDbDataParameter)?.Value;
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

                    if (outputArgs != null)
                    {
                        foreach (var key in outputArgs)
                        {
                            outputArgs[key.Key] =
                                (cmd.Parameters[key.Key] as IDbDataParameter)?.Value;
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
                    //IDataAdapter dd;

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

                        if (outputArgs != null)
                        {
                            foreach (var key in outputArgs)
                            {
                                outputArgs[key.Key] =
                                    (cmd.Parameters[key.Key] as IDbDataParameter)?.Value;
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
                reader = ExecuteReader(mConn: mConn,
                    sql: sql, cmdType: cmdType, mTrans: mTrans,
                    inputArgs: inputArgs, outputArgs: outputArgs);

                IDictionary<string, object> expando;

                while (reader.Read())
                {
                    object obj;
                    string col;
                    int colCounter = 2;
                    expando = new ExpandoObject();
                    for (int counter = 0; counter < reader.FieldCount; counter++)
                    {
                        col = reader.GetName(counter);
                        obj = reader.GetValue(counter);
                        obj = obj == DBNull.Value ? null : obj;

                        if (expando.ContainsKey(col))
                        {
                            colCounter = 1;
                            while (expando.ContainsKey(col))
                            {
                                colCounter++;
                                col = $"{col}_{colCounter}";
                            }
                        }

                        expando[col] = obj;
                    }

                    dynamic d = expando;
                    list.Add(d);
                }

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reader != null)
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
                reader = ExecuteReader(mConn: mConn,
                    sql: sql, cmdType: cmdType, mTrans: mTrans,
                    inputArgs: inputArgs, outputArgs: outputArgs);

                IDictionary<string, object> expando;

                uint cntr = 1;
                uint max = pageNumber * pageItemCount;
                uint min = (pageNumber - 1) * pageItemCount;

                while (reader.Read())
                {
                    if (cntr <= min)
                        continue;

                    if (cntr > max)
                        break;

                    cntr++;

                    object obj;
                    string col;
                    int colCounter = 2;
                    expando = new ExpandoObject();

                    for (int counter = 0; counter < reader.FieldCount; counter++)
                    {
                        col = reader.GetName(counter);
                        obj = reader.GetValue(counter);
                        obj = obj == DBNull.Value ? null : obj;

                        if (expando.ContainsKey(col))
                        {
                            colCounter = 1;
                            while (expando.ContainsKey(col))
                            {
                                colCounter++;
                                col = $"{col}_{colCounter}";
                            }
                        }

                        expando[col] = obj;
                    }

                    dynamic d = expando;
                    list.Add(d);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetDynamicResultSetWithPaging method ]

    }
}
