using BlogPlatform.Comments.DataAccess.Entities;
using BlogPlatform.Comments.DataAccess.Extensions;
using BlogPlatform.Comments.DataAccess.Factories.Contracts;
using BlogPlatform.Comments.DataAccess.Filters;
using BlogPlatform.Comments.DataAccess.Repositories.Contracts;
using Dapper;
using System.Data;

namespace BlogPlatform.Comments.DataAccess.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public CommentRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        const string procedureName = "Comments_GetAll";
        using var connection = _connectionFactory.OpenConnection;
        return await connection.QueryAsync<Comment>(
            procedureName,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<Comment> GetAsync(Guid id)
    {
        const string procedureName = "Comments_Get";
        using var connection = _connectionFactory.OpenConnection;
        var comment = await connection.QuerySingleOrDefaultAsync<Comment>(
            procedureName,
            param: new { id },
            commandType: CommandType.StoredProcedure
        );

        if (comment is null)
        {
            throw new EntityNotFoundException();
        }

        return comment;
    }

    public async Task<Guid> CreateAsync(Comment entity)
    {
        const string procedureName = "Comments_Create";
        using var connection = _connectionFactory.OpenConnection;
        return await connection.ExecuteScalarAsync<Guid>(
            procedureName,
            param: new { entity.PostId, entity.AuthorId, entity.Content },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task UpdateAsync(Guid id, Comment newEntity)
    {
        const string procedureName = "Comments_Update";
        using var connection = _connectionFactory.OpenConnection;
        int rows = await connection.ExecuteAsync(
            procedureName,
            param: new { id, newEntity.Content, newEntity.UpvoteCount },
            commandType: CommandType.StoredProcedure
        );
        
        if (rows == 0)
        {
            throw new EntityNotFoundException();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        const string procedureName = "Comments_Delete";
        using var connection = _connectionFactory.OpenConnection;
        int rows = await connection.ExecuteAsync(
            procedureName,
            param: new { id },
            commandType: CommandType.StoredProcedure
        );

        if (rows == 0)
        {
            throw new EntityNotFoundException();
        }
    }

    public async Task<IEnumerable<Comment>> GetFilteredCommentsOfPostAsync(Guid postId, CommentFilter filter)
    {
        const string procedureName = "Comments_GetAllByPostFiltered";
        using var connection = _connectionFactory.OpenConnection;
        return await connection.QueryAsync<Comment>(
            procedureName,
            param: new { postId, filter.Content },
            commandType: CommandType.StoredProcedure
        );
    }

    // TODO: work with other microservices
    public async Task<Comment> GetCommentWithAuthorAsync(Guid id)
    {
        return await GetAsync(id);
    }
}
