using System.Data;

namespace Comments.DataAccess.Factories.Contracts;

public interface IConnectionFactory
{
    string ConnectionString { get; }

    IDbConnection OpenConnection { get; }
}
