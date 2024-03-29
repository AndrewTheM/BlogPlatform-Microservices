﻿using BlogPlatform.Shared.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogPlatform.Shared.Web.Filters;

public class NotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is not EntityNotFoundException)
        {
            return;
        }

        context.Result = new NotFoundResult();
        context.ExceptionHandled = true;
    }
}
