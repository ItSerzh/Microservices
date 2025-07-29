namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;

    public string LastName { get; } = default!;

    public string? EmailAddress { get; }

    public string AddressLine { get; } = default!;

    public string Contry { get; } = default!;

    public string State { get; } = default!;

    public string ZipCode { get; } = default!;

    protected Address()
    {

    }

    private Address(string firstName, string lastName, string? emailAddress, string addressLine, string contry, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Contry = contry;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Off(string firstName, string lastName, string? emailAddress, string addressLine, string contry, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        return new Address( firstName, lastName, emailAddress, addressLine, contry, state, zipCode);
    }
}

