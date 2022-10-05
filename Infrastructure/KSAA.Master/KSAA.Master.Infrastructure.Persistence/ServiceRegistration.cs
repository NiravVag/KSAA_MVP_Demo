using KSAA.DAL;
using KSAA.DAL.Repositories;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.Master.Application.Interfaces.Services;
using KSAA.Master.Infrastructure.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Master.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MSSQLConnection");
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IPlantCodeService , PlantCodeService>();
            services.AddScoped<ITaxCodeService, TaxCodeServices>();

            #region Repositories

            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IPlantCodeService, PlantCodeService>();

            #endregion Repositories
        }
    }
}
