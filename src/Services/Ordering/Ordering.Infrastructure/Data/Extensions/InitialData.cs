namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => [
        Customer.Create(CustomerId.Of(new Guid("4afe1194-67a5-4a3f-8019-10e14a1f5d27")), "Customer1", "customer1@mail.com" ),
        Customer.Create(CustomerId.Of(new Guid("4afe1194-67a5-4a3f-8019-10e14a1f5d28")), "Customer2", "customer2@mail.com" ),
        ];

    public static IEnumerable<Product> Products => [
        Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", 950.00m),
        Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", 840.00m),
        Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e916")), "IPhone 16", 1050.00m),
        Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e915")), "Samsung 15", 1100.00m),
        ];

    public static Order[] OrdersWithItems
    {
        get
        {
            var address1 = Address.Off("Thomas", "Anderson", "thomas@mail.com", "Elmhurst, 1368 Glen Falls Road, 25", "USA", "New York", "11373");
            var address2 = Address.Off("John", "McClane", "john@mail.com", "Woodstock, 920 Red Bud Lane, 8/5", "USA", "Connecticut", "06281");

            var payment1 = Payment.Of("Thomas", "1234 5689 1236 6547", "10/29", "987", 1);
            var payment2 = Payment.Of("John", "3334 4444 1236 8888", "10/30", "765", 2);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("4afe1194-67a5-4a3f-8019-10e14a1f5d27")),
                OrderName.Of("Order 1"),
                shipping: address1,
                billing: address1,
                payment1);

            order1.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 1, 950m);
            order1.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 2, 840m);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("4afe1194-67a5-4a3f-8019-10e14a1f5d28")),
                OrderName.Of("Order 2"),
                shipping: address2,
                billing: address2,
                payment2);

            order2.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e916")), 2, 1050m);
            order2.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e915")), 1, 1100m);

            return [order1, order2];
        }
    }
}

