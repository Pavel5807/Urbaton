using System.Text.Json.Serialization;
using MvcMovie.Models;
using Urbaton.Data;
using Urbaton.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IParkingRepository, ParkingRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

SeedData.Initialize(app.Configuration);

app.Run();
