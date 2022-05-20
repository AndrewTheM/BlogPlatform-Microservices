using Comments.DataAccess.Database.Contracts;
using Comments.DataAccess.Factories;
using Comments.DataAccess.Factories.Contracts;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Comments.DataAccess.Database;

public class DatabaseGenerator : IGenerator
{
    private const string DatabaseName = "BlogPlatform_Comments";

    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<DatabaseGenerator> _logger;

    public DatabaseGenerator(IConnectionFactory connectionFactory, ILogger<DatabaseGenerator> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task GenerateAsync(string scriptPath)
    {
        try
        {
            using var connection = _connectionFactory.OpenConnection;
            _logger.LogInformation("Database exists, generation cancelled.");
        }
        catch (DbException)
        {
            string dbCreate = $"CREATE DATABASE {DatabaseName}";
            string dbScript = File.ReadAllText(scriptPath);
            string connString = _connectionFactory.ConnectionString;

            string masterConnStr = Regex.Replace(connString, DatabaseName, "master");
            var masterFactory = new SqlConnectionFactory(masterConnStr);
            using (var masterConnection = masterFactory.OpenConnection)
            {
                await masterConnection.ExecuteAsync(dbCreate);
                await Task.Delay(5000);
            }

            const RegexOptions regexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            string[] commands = Regex.Split(dbScript, @"\bgo\b", regexOptions);
            using var connection = _connectionFactory.OpenConnection;

            foreach (string command in commands)
            {
                await connection.ExecuteAsync(command);
            }

            _logger.LogInformation("Database generated.");
        }
    }
}
