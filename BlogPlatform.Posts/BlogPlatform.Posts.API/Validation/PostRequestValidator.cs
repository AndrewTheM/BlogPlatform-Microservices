using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.IO;

namespace BlogPlatform.Posts.API.Validation
{
    public class PostRequestValidator : AbstractValidator<PostRequest>
    {
        public PostRequestValidator(IStringLocalizer<ValidationResource> locale)
        {
            RuleFor(pr => pr.Title).NotEmpty()
                                   .Length(3, 200);

            RuleFor(pr => pr.ThumbnailPath).Must(BeValidPath)
                                           .When(pr => pr.ThumbnailPath is not null)
                                           .WithMessage(locale["NotAValidPath"]);

            RuleFor(pr => pr.Content).NotEmpty()
                                     .MaximumLength(20000);
        }

        private bool BeValidPath(string path)
        {
            return path.IndexOfAny(Path.GetInvalidPathChars()) == -1
                   || Uri.IsWellFormedUriString(path, UriKind.Absolute);
        }
    }
}
