using Ragne.Features.en;

using Ragne.Features.to;

using Ragne.Features.tre;

using Ragne.Features.fire;

using Ragne.Features.fem;

using Ragne.Features.seks;

using Microsoft.OpenApi.Models;
using Ragne.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL; // If not automatically added with Npgsql package

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IenService, enService>();
builder.Services.AddScoped<IenRepository, enRepository>();

builder.Services.AddScoped<ItoService, toService>();
builder.Services.AddScoped<ItoRepository, toRepository>();

builder.Services.AddScoped<ItreService, treService>();
builder.Services.AddScoped<ItreRepository, treRepository>();

builder.Services.AddScoped<IfireService, fireService>();
builder.Services.AddScoped<IfireRepository, fireRepository>();

builder.Services.AddScoped<IfemService, femService>();
builder.Services.AddScoped<IfemRepository, femRepository>();

builder.Services.AddScoped<IseksService, seksService>();
builder.Services.AddScoped<IseksRepository, seksRepository>();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDatabase"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ragne API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ragne API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
