CREATE DATABASE BlogPlatform_Comments
GO

USE BlogPlatform_Comments
GO

CREATE TABLE Comments
(
	Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
	PostId uniqueidentifier NOT NULL,
	AuthorId uniqueidentifier NOT NULL,
	Content nvarchar(max) NOT NULL,
	UpvoteCount int DEFAULT 0,
	CreatedOn datetime2(7) DEFAULT GETDATE(),
	UpdatedOn datetime2(7) DEFAULT GETDATE(),
)
GO

CREATE PROC Comments_GetAll
AS
SELECT * FROM Comments
GO

CREATE PROC Comments_Get
    @Id uniqueidentifier
AS
SELECT TOP 1 * FROM Comments
WHERE Id = @Id
GO

CREATE PROC Comments_Create
    @PostId uniqueidentifier,
	@AuthorId uniqueidentifier,
	@Content nvarchar(max)
AS
BEGIN
    DECLARE @tmp TABLE(id uniqueidentifier);

    INSERT INTO Comments (PostId, AuthorId, Content)
    OUTPUT inserted.Id INTO @tmp
    VALUES (@PostId, @AuthorId, @Content);

    SELECT id from @tmp;
END
GO

CREATE PROC Comments_Update
    @Id uniqueidentifier,
    @Content nvarchar(max),
    @UpvoteCount int
AS
BEGIN
    DECLARE @oldContent nvarchar(max);
    SELECT @oldContent = Content FROM Comments WHERE Id = @Id;

    IF @oldContent != @Content
    BEGIN
        UPDATE Comments
        SET Content = @Content, UpdatedOn = GETDATE()
        WHERE Id = @Id
    END
    ELSE
    BEGIN
        UPDATE Comments
        SET UpvoteCount = @UpvoteCount
        WHERE Id = @Id
    END
END
GO

CREATE PROC Comments_Delete
    @Id uniqueidentifier
AS
DELETE FROM Comments
WHERE Id = @Id
GO

CREATE PROC Comments_GetAllByPostFiltered
    @PostId uniqueidentifier,
    @Content nvarchar(max)
AS
SELECT * FROM Comments
WHERE PostId = @PostId
    AND (@Content IS NULL OR Content LIKE '%' + @Content + '%')
GO
