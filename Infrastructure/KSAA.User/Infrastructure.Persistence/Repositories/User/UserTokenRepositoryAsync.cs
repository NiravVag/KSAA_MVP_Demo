using KSAA.DAL;
using KSAA.DAL.Repositories;
using KSAA.Domain.Entities;
using KSAA.User.Application.Interfaces.Repositories;

namespace KSAA.User.Infrastructure.Persistence.Repositories
{
    public class UserTokenRepositoryAsync : GenericRepositoryAsync<UserToken>, IUserTokenRepositoryAsync
    {
        public UserTokenRepositoryAsync(ApplicationDBContext context) : base(context)
        {
        }
    }
}
