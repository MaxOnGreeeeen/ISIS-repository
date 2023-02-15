using Microsoft.EntityFrameworkCore;
using NotesWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connection));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
   {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
   });
}

app.UseAuthorization();

app.MapControllers();

app.Run();