namespace Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;

    public static class DataReaderExtensions
    {
        #region [ FirstRow method ]

        public static dynamic FirstRow(this IDataReader dataReader)
        {
            if (dataReader == null)
                throw new ArgumentNullException(nameof(dataReader));

            dynamic d = new ExpandoObject();

            if (dataReader.IsClosed)
            {
                return d;
            }

            try
            {
                while (dataReader.Read())
                {
                    d = GetDynamicFromDataReader(dataReader);
                    break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }

            return d;
        }

        #endregion [ First method ]

        #region [ LastRow method ]

        public static dynamic LastRow(this IDataReader dataReader)
        {
            if (dataReader == null)
                throw new ArgumentNullException(nameof(dataReader));

            dynamic d = new ExpandoObject();

            if (dataReader.IsClosed)
            {
                return d;
            }

            try
            {
                while (dataReader.Read())
                {
                    d = GetDynamicFromDataReader(dataReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }

            return d;
        }

        #endregion [ Last method ]

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
                while (reader.Read())
                {
                    dynamic d = GetDynamicFromDataReader(reader);
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

                    dynamic d = GetDynamicFromDataReader(reader);
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

        #endregion [ GetDynamicResultSetWithPaging method ]

        #region [ GetMultipleDynamicResultSet method ]

        public static List<List<dynamic>> GetMultiDynamicResultSet(
            this IDataReader reader, bool closeAtFinal = false)
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
            finally
            {
                if (reader != null && closeAtFinal)
                    reader.Close();
            }

            return objDynList;
        }

        #endregion [ GetMultipleDynamicResultSet method ]

        #region [ GetDynamicFromDataReader method ]

        internal static dynamic GetDynamicFromDataReader(IDataReader dataReader)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            var fieldCount = dataReader.FieldCount;
            var columnName = string.Empty;
            object value;
            var colCounter = 1;

            for (int counter = 0; counter < fieldCount; counter++)
            {
                columnName = dataReader.GetName(counter);
                value = dataReader.GetValue(counter);
                value = value == DBNull.Value ? null : value;

                if (expando.ContainsKey(columnName))
                {
                    colCounter = 1;
                    while (expando.ContainsKey(columnName))
                    {
                        columnName = $"{columnName}_{colCounter}";
                        colCounter++;
                    }
                }

                expando[columnName] = value;
            }

            dynamic d = expando;
            return d;
        }

        #endregion [ GetDynamicFromDataReader method ]
    }
}