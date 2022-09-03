using AspnetCoreIdentity.Api.Extensions;
using AspnetCoreIdentity.Data.Context;
using AspnetCoreIdentity.Data.Repositories;
using AspnetCoreIdentity.Domain.Interfaces.Repositories;
using AspnetCoreIdentity.Domain.Interfaces.Services;
using AspnetCoreIdentity.Domain.Services;
using AspnetCoreIdentity.Identity.Data;
using AspnetCoreIdentity.Identity.Interfaces;
using AspnetCoreIdentity.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();

var configuration = builder.Configuration;
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(configuration.GetConnectionString("Connection")));
builder.Services.AddDbContext<IdentityDataContext>(options => options.UseNpgsql(configuration.GetConnectionString("Connection")));

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
