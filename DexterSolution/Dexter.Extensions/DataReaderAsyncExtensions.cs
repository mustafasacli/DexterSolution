namespace Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Threading.Tasks;

    public static class DataReaderAsyncExtensions
    {

        #region [ GetDynamicResultSetAsync method ]

        /// <summary>
        /// Bind IDataReader content to dynamic list.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="closeAtFinal"></param>
        /// <returns>Returns dynamic object list.</returns>
        public static Task<List<dynamic>> GetDynamicResultSetAsync(
            this IDataReader reader, bool closeAtFinal = false)
        {
            Task<List<dynamic>> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.GetDynamicResultSet(reader, closeAtFinal);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSet method ]

        #region [ GetDynamicResultSetWithPagingAsync method ]
        public static Task<List<dynamic>> GetDynamicResultSetWithPagingAsync(this IDataReader reader,
            uint pageNumber = 1, uint pageItemCount = 10, bool closeAtFinal = false)
        {
            Task<List<dynamic>> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.GetDynamicResultSetWithPaging(reader, pageNumber, pageItemCount, closeAtFinal);
            });

            return resultTask;
        }

        #endregion [ GetMultiDynamicResultSet method ]

        #region [ GetMultiDynamicResultSetAsync method ]
        public static Task<List<List<dynamic>>> GetMultiDynamicResultSetAsync(
           this IDataReader reader)
        {
            Task<List<List<dynamic>>> resultTask = Task.Factory.StartNew(() =>
        {
            return DataReaderExtensions.GetMultiDynamicResultSet(reader);
        });

            return resultTask;
        }

        #endregion
    }
}
