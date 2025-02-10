

namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;

    public string LastName { get; } = default!;

    public string? EmailAddress { get; } = default!;

    public string AddressLine {get;}=default!;

    public string Country { get; } = default!;

    public string State { get; } = default!;

    public string ZipCode { get; } = default!;

    protected Address()
    {

    }

    private Address(string firstname,string lastname,string addressLine, string emailAddress, string country,string state,string zipcode)
    {
        FirstName = firstname;
        LastName = lastname;
        EmailAddress = emailAddress;
        Country = country;
        State = state;
        ZipCode = zipcode;
        AddressLine = addressLine;
    }

    public static Address Of(string firstname, string lastname, string addressLine, string emailAddress, string country, string state, string zipcode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        return new Address(firstname,lastname,addressLine,emailAddress,country,state,zipcode);
    }
}
