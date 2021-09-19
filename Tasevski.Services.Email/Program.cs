using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tasevski.Services.Email.DbContexts;
using Tasevski.Services.Email.Extensions;
using Tasevski.Services.Email.Messaging;
using Tasevski.Services.Email.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<EmailRepository, EmailRepository>();

var optionBilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionBilder.UseSqlServer(connectionString);
builder.Services.AddSingleton(new EmailRepository(optionBilder.Options));
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Tasevski.Services.Email", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasevski.Services.Email v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAzureServiceBusConsumer();

app.Run();
