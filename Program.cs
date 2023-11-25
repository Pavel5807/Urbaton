using Urbatron.Data;
using Urbatron.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

app.Run();
