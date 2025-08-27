namespace BuildingBlocks.Messaging.Dtos;

public record AddressDto(
    string FirstName,
    string LastName,
    string? EmailAddress,
    string AddressLine,
    string Contry,
    string State,
    string ZipCode);