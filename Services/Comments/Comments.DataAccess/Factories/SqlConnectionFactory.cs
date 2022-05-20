using Comments.DataAccess.Factories.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace Comments.DataAccess.Factories;

public class SqlConnectionFactory : IConnectionFactory
{
    public string ConnectionString { get; }

    public IDbConnection OpenConnection
    {
        get
        {
            SqlConnection connection = new(ConnectionString);
            connection.Open();
            return connection;
        }
    }

    public SqlConnectionFactory(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
