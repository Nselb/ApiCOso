using Microsoft.EntityFrameworkCore;
using ApiCOso;
using ApiCOso.Data;
namespace ApiCOso.Controllers;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Order", async (ApiCOsoContext db) =>
        {
            return await db.Orders.Include(a => a.OrderDetails).ToListAsync();
        })
        .WithName("GetAllOrders")
        .Produces<List<Order>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Order/{id}", async (int Id, ApiCOsoContext db) =>
        {
            return await db.Orders.FindAsync(Id)
                is Order model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetOrderById")
        .Produces<Order>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Order/{id}", async (int Id, Order order, ApiCOsoContext db) =>
        {
            var foundModel = await db.Orders.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(order);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateOrder")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Order/", async (Order order, ApiCOsoContext db) =>
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return Results.Created($"/Orders/{order.Id}", order);
        })
        .WithName("CreateOrder")
        .Produces<Order>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Order/{id}", async (int Id, ApiCOsoContext db) =>
        {
            if (await db.Orders.FindAsync(Id) is Order order)
            {
                db.Orders.Remove(order);
                await db.SaveChangesAsync();
                return Results.Ok(order);
            }

            return Results.NotFound();
        })
        .WithName("DeleteOrder")
        .Produces<Order>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
