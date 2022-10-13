using KSAA.Master.ApiServices.Extensions;
using KSAA.Master.ApiServices.Middlewares;
using KSAA.Master.Application;
using KSAA.Master.Application.Interfaces.Services;
using KSAA.Master.Infrastructure.Persistence;
using KSAA.Master.Infrastructure.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddScoped<IDocumentTypeService , DocumentTypeService>(); 
builder.Services.AddScoped<IPlantCodeService , PlantCodeService>();
builder.Services.AddScoped<ITaxCodeService, TaxCodeService>();
builder.Services.AddScoped<ICustomerCodeService, CustomerCodeService>();
builder.Services.AddScoped<IVendorCodeService, VendorCodeService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ITBTaggingService, TBTaggingService>();
builder.Services.AddScoped<IGLIncome_MappingService, GLIncome_MappingService>();

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
