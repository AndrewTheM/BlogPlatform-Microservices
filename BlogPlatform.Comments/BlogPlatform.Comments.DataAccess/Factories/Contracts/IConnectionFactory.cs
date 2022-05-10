using System.Data;

namespace BlogPlatform.Comments.DataAccess.Factories.Contracts;

public interface IConnectionFactory
{
    IDbConnection OpenConnection { get; }

    void SetupConnection(string connectionString);
}
