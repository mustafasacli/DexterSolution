namespace Dexter.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Data;

    public interface IDxConnectionOperations
    {
        DataSet GetResultSet(string sql, CommandType cmdType, Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null);

        object ExecuteScalar(string sql, CommandType cmdType, Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null);

        int Execute(string sql, CommandType cmdType, Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null);

        List<dynamic> GetDynamicResultSet(string sql, CommandType cmdType, Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null);

        List<dynamic> GetDynamicResultSetWithPaging(string sql, CommandType cmdType, Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null, uint pageNumber = 1, uint pageItemCount = 10);
    }
}