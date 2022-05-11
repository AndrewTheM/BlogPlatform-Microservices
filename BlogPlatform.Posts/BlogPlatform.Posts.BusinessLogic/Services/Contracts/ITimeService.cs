namespace BlogPlatform.Posts.BusinessLogic.Services.Contracts;

public interface ITimeService
{
    string ConvertToLocalRelativeString(DateTime dateTime);
}
