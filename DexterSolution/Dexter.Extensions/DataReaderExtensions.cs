namespace Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;

    public static class DataReaderExtensions
    {
        #region [ GetDynamicResultSet method ]

        /// <summary>
        /// Bind IDataReader content to dynamic list.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="closeAtFinal"></param>
        /// <returns>Returns dynamic object list.</returns>
        public static List<dynamic> GetDynamicResultSet(
            this IDataReader reader, bool closeAtFinal = false)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            List<dynamic> list = new List<dynamic>();

            if (reader.IsClosed)
            {
                return list;
            }

            try
            {
                IDictionary<string, object> expando;
                object obj;
                string col;
                int colCounter = 1;
                int fieldCount = reader.FieldCount;

                while (reader.Read())
                {
                    expando = new ExpandoObject();

                    for (int counter = 0; counter < fieldCount; counter++)
                    {
                        col = reader.GetName(counter);
                        obj = reader.GetValue(counter);
                        obj = obj == DBNull.Value ? null : obj;

                        if (expando.ContainsKey(col))
                        {
                            colCounter = 1;
                            while (expando.ContainsKey(col))
                            {
                                col = $"{col}_{colCounter}";
                                colCounter++;
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
                if (reader != null && closeAtFinal)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetDynamicResultSet method ]

        #region [ GetDynamicResultSetWithPaging method ]

        public static List<dynamic> GetDynamicResultSetWithPaging(this IDataReader reader,
            uint pageNumber = 1, uint pageItemCount = 10, bool closeAtFinal = false)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            List<dynamic> list = new List<dynamic>();

            if (reader.IsClosed)
            {
                return list;
            }

            try
            {
                IDictionary<string, object> expando;

                uint cntr = 1;
                uint max = pageNumber * pageItemCount;
                uint min = (pageNumber - 1) * pageItemCount;

                object obj;
                string col;
                int colCounter = 1;
                int fieldCount = reader.FieldCount;

                while (reader.Read())
                {
                    if (cntr <= min)
                        continue;

                    if (cntr > max)
                        break;

                    cntr++;
                    expando = new ExpandoObject();

                    for (int counter = 0; counter < fieldCount; counter++)
                    {
                        col = reader.GetName(counter);
                        obj = reader.GetValue(counter);
                        obj = obj == DBNull.Value ? null : obj;

                        if (expando.ContainsKey(col))
                        {
                            colCounter = 1;
                            while (expando.ContainsKey(col))
                            {
                                col = $"{col}_{colCounter}";
                                colCounter++;
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
                if (reader != null && closeAtFinal)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetMultiDynamicResultSet method ]

        #region [ GetMultipleDynamicResultSet method ]

        public static List<List<dynamic>> GetMultiDynamicResultSet(
            this IDataReader reader)
        {
            List<List<dynamic>> objDynList = new List<List<dynamic>>();

            try
            {
                List<dynamic> resultSet = null;

                do
                {
                    resultSet = new List<dynamic>();
                    resultSet = reader.GetDynamicResultSet();
                    objDynList.Add(resultSet);
                } while (reader.NextResult());
            }
            catch (Exception)
            {
                throw;
            }

            return objDynList;
        }

        #endregion
    }
}