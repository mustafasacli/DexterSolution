namespace Dexter.Core.Objects
{
    using System.Collections.Generic;
    using System.Data;

    public class MainSqlCommand
    {
        public string CommandText { get; set; }
        public CommandType SqlCommandType { get; set; }
        public Dictionary<string, object> CommandInputArgs { get; set; }
        public Dictionary<string, object> CommandOutputArgs { get; set; }
    }
}