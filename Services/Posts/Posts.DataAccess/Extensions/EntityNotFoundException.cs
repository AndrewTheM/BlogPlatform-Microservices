﻿namespace Posts.DataAccess.Extensions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
        : base("Entity not found.")
    {
    }

    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
