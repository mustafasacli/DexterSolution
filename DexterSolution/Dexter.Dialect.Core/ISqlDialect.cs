namespace Dexter.Dialect.Core
{
    public interface ISqlDialect
    {
        string ParameterPrefix { get; }

        string ColumnPrefix { get; }

        string TableNamePrefix { get; }

        string SchemaNamePrefix { get; }

        string ParameterPostfix { get; }

        string ColumnPostfix { get; }

        string TableNamePostfix { get; }

        string SchemaNamePostfix { get; }

        //string ParameterPrefix { get; }
        //string ParameterPrefix { get; }

    }
}
