namespace Dexter.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Data;

    public interface IDxConnection : IDxConnectionOperations
    {
        //Open Connection
        void Open();

        //Begin Transaction
        void Begin();

        //Begin Transaction
        void Begin(IsolationLevel isoLevel);

        //Commit Transaction
        void Commit();

        //Rollback Transaction
        void Rollback();

        //Close Connection
        void Close();

        string ConnectionString { get; set; }

        IDataReader ExecuteReader(string sql, CommandType cmdType, Dictionary<string, object> inputArgs = null, Dictionary<string, object> outputArgs = null);


    }
}
