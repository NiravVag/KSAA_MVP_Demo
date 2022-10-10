using KSAA.DAL;
using KSAA.DAL.Repositories;
using KSAA.Domain.Entities;
using KSAA.User.Application.Interfaces.Repositories;

namespace KSAA.User.Infrastructure.Persistence.Repositories
{
    public class UserStatusRepositoryAsync : GenericRepositoryAsync<UserStatuses>, IUserStatusRepositoryAsync 
    {
        public UserStatusRepositoryAsync(ApplicationDBContext context) : base(context)
        { }
    }
}
