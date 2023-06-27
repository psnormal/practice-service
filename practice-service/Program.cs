using practice_service.Services;
using practice_service;
using Microsoft.EntityFrameworkCore;
using practice_service.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPracticePeriodService, PracticePeriodService>();
builder.Services.AddScoped<IPracticeProfileService, PracticeProfileService>();
builder.Services.AddScoped<IWorkPlaceInfoService, WorkPlaceInfoService>();

//DB connection:
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));


var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
context?.Database.Migrate();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000").AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
