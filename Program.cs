using HHPW_Bangazon_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using HHPW_Bangazon_BE;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HHPWDbContext>(builder.Configuration["HHPWDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ORDERS
// Get All Orders
app.MapGet("/api/orders", (HHPWDbContext db) =>
{
    return db.Orders.ToList();
});

// Get A Single Order
app.MapGet("/api/orders/{orderId}", (HHPWDbContext db, int id) =>
{
    Orders order = db.Orders.FirstOrDefault(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(order);
});

// Create A New Order
app.MapPost("/api/orders", (HHPWDbContext db, Orders order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"/api/orders/{order.Id}", order);
});

// Update An Existing Order
app.MapPut("/api/orders/{orderId}", (HHPWDbContext db, int id, Orders order) =>
{
    Orders orderToUpdate = db.Orders.SingleOrDefault(o => o.Id == id);
    if (orderToUpdate == null)
    {
        return Results.NotFound();
    }
    orderToUpdate.OrderName = order.OrderName;
    orderToUpdate.CustomerPhone = order.CustomerPhone;
    orderToUpdate.CustomerEmail = order.CustomerEmail;
    orderToUpdate.Type = order.Type;
    orderToUpdate.OrderStatus = order.OrderStatus;
    orderToUpdate.PaymentType = order.PaymentType;
    orderToUpdate.OrderTotal = order.OrderTotal;
    orderToUpdate.Rating = order.Rating;
    orderToUpdate.UserId = order.UserId;
    db.SaveChanges();
    return Results.NoContent();
});

// Delete An Existing Order 
app.MapDelete("/api/orders/{orderId}", (HHPWDbContext db, int id) =>
{
    Orders order = db.Orders.SingleOrDefault(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }

    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();
});

// ITEMS
// Get All Items
app.MapGet("/api/items", (HHPWDbContext db) =>
{
    return db.Items.ToList();
});

// Get A Single Item
app.MapGet("/api/items/{itemId}", (HHPWDbContext db, int id) =>
{
    Items item = db.Items.FirstOrDefault(i => i.Id == id);
    if (item == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(item);
});

// Create A New Item
app.MapPost("/api/items", (HHPWDbContext db, Items item) =>
{
    db.Items.Add(item);
    db.SaveChanges();
    return Results.Created($"/api/items/{item.Id}", item);
});

// Update An Existing Item
app.MapPut("/api/items/{itemId}", (HHPWDbContext db, int id, Items item) =>
{
    Items itemToUpdate = db.Items.SingleOrDefault(i => i.Id == id);
    if (itemToUpdate == null)
    {
        return Results.NotFound();
    }
    itemToUpdate.Name = item.Name;
    itemToUpdate.Price = item.Price;
    db.SaveChanges();
    return Results.NoContent();
});

// Delete An Existing Item 
app.MapDelete("/api/items/{itemId}", (HHPWDbContext db, int id) =>
{
    Items item = db.Items.SingleOrDefault(i => i.Id == id);
    if (item == null)
    {
        return Results.NotFound();
    }

    db.Items.Remove(item);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();

