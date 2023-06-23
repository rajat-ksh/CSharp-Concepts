using CatalogService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CatalogService",
        Version = "v1"
    });
});

//builder.Services.AddDbContext<CatalogServiceAPIDbContext>(options => options.UseInMemoryDatabase("CatalogService"));
builder.Services.AddDbContext<CatalogServiceAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogServiceApiConnectionString")));

var app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("../swagger/v1/swagger.json", "My API v1");
    options.RoutePrefix = string.Empty;
});
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
