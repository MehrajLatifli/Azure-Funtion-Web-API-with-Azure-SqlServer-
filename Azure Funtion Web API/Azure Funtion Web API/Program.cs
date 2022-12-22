using Azure_Funtion_Web_API.DataAccess;
using Azure_Funtion_Web_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o=>o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title ="Azure Function Web API", Version="V1", Description="Upload Image"}));
builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllers().AddJsonOptions(o =>
{

    o.JsonSerializerOptions.PropertyNamingPolicy = null;
    o.JsonSerializerOptions.DictionaryKeyPolicy = null;
});


builder.Services.AddScoped<IInfoFileDal, EfInfoFileDal>();

builder.Services.AddDbContext<InfoFileContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o=>o.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure Function Web API"));
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

app.MapControllerRoute(name: "default", pattern: "{Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
