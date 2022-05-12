using BlogPlatform.Verifications.Domain.Abstract;

namespace BlogPlatform.Verifications.Domain.ValueObjects;

public class Location : ValueObject
{   
    public string City { get; private set; }

    public string State { get; private set; }
    
    public string Country { get; private set; }

    public Location(string country, string state, string city)
    {
        Country = country;
        State = state;
        City = city;
    }

    public override string ToString()
    {
        return $"{City}, {State}, {Country}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return State;
        yield return Country;
    }
}
