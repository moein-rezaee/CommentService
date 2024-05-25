using System.Reflection;
using CommentService.Context;
using CommentService.Interfaces;
using CommentService.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var defName = builder.Configuration["Db:Name"];
var defHost = builder.Configuration["Db:Host"];
var defPass = builder.Configuration["Db:Pass"];
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;
var connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID=sa;Password={dbPass};Initial Catalog={dbName};";
builder.Services.AddDbContext<CommentContextDb>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

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
