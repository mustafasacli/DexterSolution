namespace Dexter.Extensions
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public static class DxConnectionAsyncExtension
    {
        #region [ ExecuteAsync method ]

        public static Task<int> ExecuteAsync(this IDbConnection mConn,
            string sql,
            CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionExtension.Execute(mConn,
            sql,
            cmdType,
            mTrans,
             inputArgs,
             outputArgs);
            });

            return resultTask;
        }

        #endregion [ ExecuteAsync method ]

        #region [ ExecuteReaderAsync method ]

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionExtension.ExecuteReader(mConn,
            sql,
            cmdType,
            mTrans,
             inputArgs,
             outputArgs);
            });

            return resultTask;
        }

        #endregion [ ExecuteReaderAsync method ]

        #region [ ExecuteScalarAsync method ]

        public static Task<object> ExecuteScalarAsync(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionExtension.ExecuteScalar(mConn,
            sql,
            cmdType,
            mTrans,
             inputArgs,
             outputArgs);
            });

            return resultTask;
        }

        #endregion [ ExecuteScalarAsync method ]

        #region [ GetResultSetAsync method ]

        public static Task<DataSet> GetResultSetAsync(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionExtension.GetResultSet(mConn,
            sql,
            cmdType,
            mTrans,
             inputArgs,
             outputArgs);
            });

            return resultTask;
        }

        #endregion [ GetResultSetAsync method ]

        #region [ GetDynamicResultSetAsync method ]

        public static Task<List<dynamic>> GetDynamicResultSetAsync(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionExtension.GetDynamicResultSet(mConn,
            sql,
            cmdType,
            mTrans,
             inputArgs,
             outputArgs);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetAsync method ]

        #region [ GetDynamicResultSetWithPagingAsync method ]

        public static Task<List<dynamic>> GetDynamicResultSetWithPagingAsync(this IDbConnection mConn,
            string sql, CommandType cmdType,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null,
            uint pageNumber = 1, uint pageItemCount = 10)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionExtension.GetDynamicResultSetWithPaging(mConn,
            sql,
            cmdType,
            mTrans,
             inputArgs,
             outputArgs);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetWithPagingAsync method ]

        #region [ GetMultiDynamicResultSetAsync method ]

        public static Task<List<List<dynamic>>> GetMultiDynamicResultSetAsync(this IDbConnection mConn,
            string sql, CommandType cmdType = CommandType.Text,
            IDbTransaction mTrans = null,
            Dictionary<string, object> inputArgs = null,
            Dictionary<string, object> outputArgs = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionExtension.GetMultiDynamicResultSet(mConn,
            sql,
            cmdType,
            mTrans,
             inputArgs,
             outputArgs);
            });

            return resultTask;
        }

        #endregion [ GetMultiDynamicResultSetAsync method ]
    }
}