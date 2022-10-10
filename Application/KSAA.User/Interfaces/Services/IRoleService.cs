using KSAA.User.Application.DTOs.Role;
using KSAA.User.Application.Features.Role.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<RoleViewModel> AddRole(CreateRoleCommand command);

        Task<RoleViewModel> EditRole(UpdateRoleCommand command);

        Task<List<RoleListModel>> GetRoleList();

        Task<RoleViewModel> GetRoleById(long id);

        Task DeleteRole(DeleteRoleCommand _object);
    }
}
