using Microsoft.EntityFrameworkCore;
using ProvaPub.Application.Interfaces;
using ProvaPub.Application.Services;
using ProvaPub.Domain.Interfaces;
using ProvaPub.Infrastructure.Persistence.Repository;
using ProvaPub.Infrastructure.Repositories;
using ProvaPub.Infrastructure.Services.PaymentStrategies;
using ProvaPub.Repository;
using ProvaPub.Services.PaymentStrategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRandomRepository, RandomRepository>();

// Domain/Application Services
builder.Services.AddScoped<RandomService>();        
builder.Services.AddScoped<CustomerService>();     
builder.Services.AddScoped<ProductService>();       
builder.Services.AddScoped<OrderService>();         

builder.Services.AddScoped<IPaymentStrategy, PixPaymentStrategy>();
builder.Services.AddScoped<IPaymentStrategy, CreditCardPaymentStrategy>();
builder.Services.AddScoped<IPaymentStrategy, PaypalPaymentStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
