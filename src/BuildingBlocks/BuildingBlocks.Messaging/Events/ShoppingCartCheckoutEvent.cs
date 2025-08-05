namespace BuildingBlocks.Messaging.Events;

public record ShoppingCartCheckoutEvent : IntegrationEvent
{
    public string Username { get; set; } = default!;

    public Guid CustomerId { get; set; }

    public decimal Price { get; set; }

    //Shipping, Billing
    public string FirstName { get; } = default!;

    public string LastName { get; } = default!;

    public string? EmailAddress { get; }

    public string AddressLine { get; } = default!;

    public string Contry { get; } = default!;

    public string State { get; } = default!;

    public string ZipCode { get; } = default!;

    //Payment
    public string? CardName { get; } = default!;

    public string CardNumber { get; } = default!;

    public string Expiration { get; } = default!;

    public string Cvv { get; } = default!;

    public int PaymentMethod { get; } = default!;



}

