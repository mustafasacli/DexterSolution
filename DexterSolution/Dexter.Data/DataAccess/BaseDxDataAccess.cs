namespace Dexter.Data.DataAccess
{
    using Dexter.Core.Connection;
    using Dexter.Core.Interfaces;
    using Dexter.Core.Objects;
    using Dexter.Factory;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;

    public class BaseDxDataAccess : IDxConnectionOperations
    {
        protected readonly int NullCommandResult = -100;
        protected readonly int NullCommandTextResult = -101;

        protected IDxConnection mConn = null;

        public BaseDxDataAccess(string confName, string connectionString)
        {
            IDbConnection conn = ConnectionFactory.Instance.GetConnection(confName);
            //conn.ConnectionString = connectionString;
            mConn = new BaseDxConnection(conn);
            mConn.ConnectionString = connectionString;
        }

        public int Execute(string sql, CommandType cmdType,
            Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null)
        {
            int result = 0;

            try
            {
                if (string.IsNullOrWhiteSpace(sql))
                    return NullCommandTextResult;

                mConn.Open();
                mConn.Begin();
                result = mConn.Execute(sql, cmdType, inputArgs, outputArgs);
                mConn.Commit();
            }
            catch (Exception e)
            {
                mConn.Rollback();
            }
            finally
            {
                mConn.Close();
            }

            return result;
        }

        public int Execute(MainSqlCommand cmd)
        {
            int result = 0;

            try
            {
                if (cmd == null)
                    return NullCommandResult;

                if (string.IsNullOrWhiteSpace(cmd.CommandText))
                    return NullCommandTextResult;

                mConn.Open();
                mConn.Begin();
                result = mConn.Execute(cmd.CommandText, cmd.SqlCommandType, cmd.CommandInputArgs, cmd.CommandOutputArgs);
                mConn.Commit();
            }
            catch (Exception e)
            {
                mConn.Rollback();
                throw;
            }
            finally
            {
                mConn.Close();
            }

            return result;
        }

        public List<int> ExecuteList(List<MainSqlCommand> cmdList)
        {
            List<int> result = new List<int>();

            try
            {
                List<MainSqlCommand> commands = cmdList ?? new List<MainSqlCommand>();
                mConn.Open();
                mConn.Begin();

                int rs;
                foreach (var cmd in commands)
                {
                    if (cmd == null)
                    {
                        result.Add(NullCommandResult);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(cmd.CommandText))
                    {
                        result.Add(NullCommandTextResult);
                        continue;
                    }

                    rs = mConn.Execute(cmd.CommandText, cmd.SqlCommandType, cmd.CommandInputArgs, cmd.CommandOutputArgs);
                    result.Add(rs);
                }

                mConn.Commit();
            }
            catch (Exception e)
            {
                mConn.Rollback();
                throw;
            }
            finally
            {
                mConn.Close();
            }

            return result;
        }

        public object ExecuteScalar(string sql, CommandType cmdType,
            Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null)
        {
            object result = new object();

            try
            {
                if (string.IsNullOrWhiteSpace(sql))
                    return NullCommandTextResult;

                mConn.Open();
                mConn.Begin();
                result = mConn.ExecuteScalar(sql, cmdType, inputArgs, outputArgs);
                mConn.Commit();
            }
            catch (Exception e)
            {
                mConn.Rollback();
                throw;
            }
            finally
            {
                mConn.Close();
            }

            return result;
        }

        public object ExecuteScalar(MainSqlCommand cmd)
        {
            object result = new object();

            try
            {
                if (cmd == null)
                    return NullCommandResult;

                if (string.IsNullOrWhiteSpace(cmd.CommandText))
                    return NullCommandTextResult;

                mConn.Open();
                mConn.Begin();
                result = mConn.ExecuteScalar(cmd.CommandText, cmd.SqlCommandType, cmd.CommandInputArgs, cmd.CommandOutputArgs);
                mConn.Commit();
            }
            catch (Exception e)
            {
                mConn.Rollback();
                throw;
            }
            finally
            {
                mConn.Close();
            }

            return result;
        }

        public List<object> ExecuteScalarList(List<MainSqlCommand> cmdList)
        {
            List<object> result = new List<object>();

            try
            {
                mConn.Open();
                mConn.Begin();
                List<MainSqlCommand> commands = cmdList ?? new List<MainSqlCommand>();

                object rs;
                foreach (var cmd in commands)
                {
                    if (cmd == null)
                    {
                        result.Add(NullCommandResult);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(cmd.CommandText))
                    {
                        result.Add(NullCommandTextResult);
                        continue;
                    }

                    rs = mConn.ExecuteScalar(cmd.CommandText, cmd.SqlCommandType, cmd.CommandInputArgs, cmd.CommandOutputArgs);
                    result.Add(rs);
                }

                mConn.Commit();
            }
            catch (Exception e)
            {
                mConn.Rollback();
                throw;
            }
            finally
            {
                mConn.Close();
            }

            return result;
        }

        public List<dynamic> GetDynamicResultSet(string sql, CommandType cmdType,
            Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null)
        {
            List<dynamic> list = new List<dynamic>();
            IDataReader reader = null;

            try
            {
                mConn.Open();
                reader = mConn.ExecuteReader(sql, cmdType, inputArgs, outputArgs);

                IDictionary<string, object> expando;
                object obj;
                string col;
                int colCounter = 2;

                while (reader.Read())
                {
                    colCounter = 2;
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
                try
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
                catch (Exception)
                {
                }

                mConn.Close();
            }

            return list;
        }

        public List<dynamic> GetDynamicResultSetWithPaging(string sql, CommandType cmdType,
            Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null,
            uint pageNumber = 1, uint pageItemCount = 10)
        {
            List<dynamic> list = new List<dynamic>();
            IDataReader reader = null;
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageItemCount = pageItemCount < 10 ? 10 : pageItemCount;

            try
            {
                mConn.Open();
                reader = mConn.ExecuteReader(sql, cmdType, inputArgs, outputArgs);
                int rowCount = reader.RecordsAffected;
                IDictionary<string, object> expando;

                uint cntr = 1;
                uint max = pageNumber * pageItemCount;
                uint min = (pageNumber - 1) * pageItemCount;

                if (rowCount < min)
                {
                    if (max > (min + (uint)rowCount))
                        max = min + (uint)rowCount;

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
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
                catch (Exception)
                {
                }

                mConn.Close();
            }

            return list;
        }

        public DataSet GetResultSet(string sql, CommandType cmdType,
            Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null)
        {
            DataSet ds = null;

            try
            {
                ds = mConn.GetResultSet(sql, cmdType, inputArgs, outputArgs);
            }
            catch (Exception e)
            {
                throw;
            }

            return ds;
        }
    }
}
