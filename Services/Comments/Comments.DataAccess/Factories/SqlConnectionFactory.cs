using Comments.DataAccess.Factories.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace Comments.DataAccess.Factories;

public class SqlConnectionFactory : IConnectionFactory
{
    public SqlConnectionFactory(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string ConnectionString { get; }

    public IDbConnection OpenConnection
    {
        get
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
