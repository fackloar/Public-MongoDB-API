using Elasticsearch.Net;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.BusinessLayer.DTOs;
using MongoDB.BusinessLayer.Interfaces;
using MongoDB.BusinessLayer.Services;
using MongoDB.DataLayer;
using MongoDB.DataLayer.Interfaces;
using MongoDB.DataLayer.Models;
using MongoDB.DataLayer.Repositories;
using MongoDB.Driver;
using Nest;
using Safe_Development.BusinessLayer.Validation;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BooksDatabaseSettings>(
    builder.Configuration.GetSection(nameof(BooksDatabaseSettings)));

builder.Services.AddSingleton<IBooksDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<BooksDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("BooksDatabaseSettings:ConnectionString")));

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IValidator<BookDTO>, BookValidation>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen();

builder.Services.Configure<ElasticSearchSettings>(
    builder.Configuration.GetSection("ElasticSearch"));
// elastic service
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var Seeder = new Seed(services.GetRequiredService<IOptions<BooksDatabaseSettings>>(), new ElasticClient());
        Seeder.Initialize().Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error while seeding the db");
    }
}

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
