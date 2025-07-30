using Microsoft.AspNetCore.Builder;
using Ordering.Infrastructure.Data.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabseAsync(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<OrderingContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(OrderingContext context)
    {
        await SeedCustomerAsync(context);
        await SeedProductAsync(context);
        await SeedOrderAndItemsAsync(context);
    }

    private static async Task SeedCustomerAsync(OrderingContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(OrderingContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedOrderAndItemsAsync(OrderingContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }

}

