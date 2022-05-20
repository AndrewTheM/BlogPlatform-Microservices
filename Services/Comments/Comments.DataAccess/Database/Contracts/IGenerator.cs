namespace Comments.DataAccess.Database.Contracts;

public interface IGenerator
{
    Task GenerateAsync(string scriptPath);
}
