using LibraryManager.API.ExceptionHandler;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("LibraryManagerCs");
builder.Services.AddDbContext<LibraryManagerDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
