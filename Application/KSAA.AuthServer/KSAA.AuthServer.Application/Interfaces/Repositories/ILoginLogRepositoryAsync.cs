using KSAA.User.Application.Interfaces.Repositories;
using KSAA.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.AuthServer.ApiServices.Application.Interfaces.Repositories
{
    public interface ILoginLogRepositoryAsync : IGenericRepositoryAsync<LoginLog>
    {
    }
}
