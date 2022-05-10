using BlogPlatform.Comments.DataAccess.Factories.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace BlogPlatform.Comments.DataAccess.Factories;

public class SqlConnectionFactory : IConnectionFactory
{
    private string _connectionString;

    public IDbConnection OpenConnection
    {
        get
        {
            SqlConnection connection = new(_connectionString);
            connection.Open();
            return connection;
        }
    }

    public SqlConnectionFactory(string connectionString)
    {
        SetupConnection(connectionString);
    }

    public void SetupConnection(string connectionString)
    {
        _connectionString = connectionString;
    }
}
