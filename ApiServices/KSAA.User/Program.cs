using KSAA.DAL;
using KSAA.Domain.Entities;
using KSAA.User.ApiServices.Extensions;
using KSAA.User.ApiServices.Middlewares;
using KSAA.User.Application;
using KSAA.User.Application.Interfaces.Services;
using KSAA.User.Infrastructure.Persistence;
using KSAA.User.Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddJwtTokenAuthentication(builder.Configuration);
builder.Services.AddApiVersioningExtension();
//builder.Services.AddSwaggerConfiguration();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer().AddApplicationLayer();
builder.Services.AddSwaggerGen();

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

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
