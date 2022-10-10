using KSAA.DAL;
using KSAA.DAL.Repositories;
using KSAA.Domain.Interfaces.Repositories;
using KSAA.User.Application.Interfaces.Repositories;
using KSAA.User.Application.Interfaces.Services;
using KSAA.User.Infrastructure.Persistence.Repositories;
using KSAA.User.Infrastructure.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MSSQLConnection");
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));


            #region Repositories

            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));            
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserStatusRepositoryAsync, UserStatusRepositoryAsync>();
            services.AddScoped<IUserTokenRepositoryAsync, UserTokenRepositoryAsync>();

            #endregion Repositories
        }
    }
}
