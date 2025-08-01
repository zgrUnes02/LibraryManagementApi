using LibraryManagementApi.Database;
using LibraryManagementApi.Exceptions;
using LibraryManagementApi.Repositories;
using LibraryManagementApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionLink"));
});

builder.Services.AddScoped<IAuthRepository, AuthenticationService>();

// Add global exception handler


// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndExemple", policy =>
    {
        policy.WithOrigins("https://localhost:7056/");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

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

app.UseExceptionHandler();

app.UseCors("FrontEndExemple");

app.Run();
