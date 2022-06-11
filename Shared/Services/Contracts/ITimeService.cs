namespace BlogPlatform.Shared.Services.Contracts;

public interface ITimeService
{
    string ConvertToLocalRelativeString(DateTime dateTime);
}
