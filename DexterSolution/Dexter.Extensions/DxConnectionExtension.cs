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

                    DxDbCommandHelper.SetCommandParameters(cmd, inputArgs, outputArgs);

                    res = cmd.ExecuteNonQuery();

                    outputArgs = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(cmd);
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

                    DxDbCommandHelper.SetCommandParameters(cmd, inputArgs, outputArgs);

                    reader = cmd.ExecuteReader();

                    outputArgs = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(cmd);
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

                    DxDbCommandHelper.SetCommandParameters(cmd, inputArgs, outputArgs);

                    res = cmd.ExecuteScalar();

                    outputArgs = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(cmd);
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
                Type adapterTyp = null;
                var adapterTypes =
                     mConn.GetType().Assembly.GetExportedTypes().Where(
                         typ => typ.IsClass && typ.GetInterfaces().Contains(typeof(IDbDataAdapter))
                                && typ.IsAbstract == false && typeof(IDbDataAdapter).IsAssignableFrom(typ));

                if (adapterTypes.Count() > 1)
                {
                    var connTypeName = mConn.GetType().Name;
                    connTypeName = connTypeName.Substring(0, connTypeName.Length - 10).ToLower();
                    adapterTyp = adapterTypes.Where(typ => typ.Name.ToLower().StartsWith(connTypeName)).First();
                }
                else
                {
                    adapterTyp = adapterTypes.First();
                }

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

                        DxDbCommandHelper.SetCommandParameters(cmd, inputArgs, outputArgs);

                        d.SelectCommand = cmd;
                        ds = new DataSet();
                        var result = d.Fill(ds);

                        outputArgs = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(cmd);
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
                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    DxDbCommandHelper.SetCommandParameters(cmd, inputArgs, outputArgs);

                    reader = cmd.ExecuteReader();

                    outputArgs = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(cmd);

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
                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    DxDbCommandHelper.SetCommandParameters(cmd, inputArgs, outputArgs);

                    reader = cmd.ExecuteReader();

                    outputArgs = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(cmd);

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
                using (IDbCommand cmd = mConn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;

                    if (mTrans != null)
                        cmd.Transaction = mTrans;

                    DxDbCommandHelper.SetCommandParameters(cmd, inputArgs, outputArgs);

                    reader = cmd.ExecuteReader();

                    outputArgs = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(cmd);

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

    public class DxDbCommandHelper
    {
        /// <summary>
        /// creates parameters and sets their values of IDbCommand.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="inputArgs"></param>
        /// <param name="outputArgs"></param>
        public static void SetCommandParameters(IDbCommand command,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            Dictionary<string, object> inputs =
                inputArgs ?? new Dictionary<string, object>();

            Dictionary<string, object> outputs =
                outputArgs ?? new Dictionary<string, object>();
            IDbDataParameter parameter;

            foreach (var key in inputs)
            {
                parameter = command.CreateParameter();
                parameter.ParameterName = key.Key;
                parameter.Value = key.Value;

                parameter.Direction = outputs.ContainsKey(key.Key) ?
                    ParameterDirection.InputOutput : ParameterDirection.Input;
                command.Parameters.Add(parameter);
            }

            foreach (var key in outputs)
            {
                if (!inputs.ContainsKey(key.Key))
                {
                    parameter = command.CreateParameter();
                    parameter.ParameterName = key.Key;
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// Get Output Values of IDbCommand
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetOutParametersOfCommand(IDbCommand command)
        {
            IDictionary<string, object> outputArgs = new Dictionary<string, object>();

            if (command.Parameters != null && command.Parameters.Count > 0)
            {
                IDbDataParameter prm;
                // outputArgs = new Dictionary<string, object>();
                foreach (var item in command.Parameters)
                {
                    prm = item as IDbDataParameter;
                    if (prm.Direction.IsMember(
                        ParameterDirection.Output,
                        ParameterDirection.InputOutput,
                        ParameterDirection.ReturnValue))
                    {
                        outputArgs[prm.ParameterName] = prm.Value;
                    }
                }
            }

            return outputArgs;
        }
    }
}