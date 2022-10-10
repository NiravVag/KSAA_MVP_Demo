using KSAA.User.Application.DTOs.User;
using KSAA.User.Application.Features.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserViewModel> AddUser(CreateUserCommand command);

        Task<UserViewModel> EditUser(UpdateUserCommand command);

        Task <List<UserListModel>> GetUserList();

        Task<UserViewModel> GetUserById(long id);

        Task DeleteUser(DeleteUserCommand _object);
    }
}
