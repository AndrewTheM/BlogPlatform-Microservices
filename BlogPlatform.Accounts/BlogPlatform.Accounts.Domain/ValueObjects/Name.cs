using BlogPlatform.Verifications.Domain.Abstract;
using BlogPlatform.Verifications.Domain.Exceptions;

namespace BlogPlatform.Verifications.Domain.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; private set; }

    public string MiddleName { get; private set; }

    public string LastName { get; private set; }

    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Name(string firstName, string middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    public static Name From(string name)
    {
        try
        {
            string[] parts = name.Split();
            
            (string firstName, string middleName, string lastName) = parts.Length switch
            {
                2 => (parts[0], null, parts[1]),
                3 => (parts[0], parts[1], parts[2]),
                _ => throw new InvalidNameException()
            };

            return new Name(firstName, middleName, lastName);
        }
        catch (InvalidNameException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidNameException(ex);
        }
    }

    public override string ToString()
    {
        string middlePart = (string.IsNullOrWhiteSpace(MiddleName))
            ? string.Empty
            : $"{MiddleName} ";

        return $"{FirstName} {middlePart}{LastName}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return MiddleName;
        yield return LastName;
    }
}
