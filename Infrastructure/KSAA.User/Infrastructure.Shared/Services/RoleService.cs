using AutoMapper;
using KSAA.DAL;
using KSAA.Domain.Entities;
using KSAA.User.Application.DTOs.Role;
using KSAA.User.Application.Features.Role.Commands;
using KSAA.User.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Infrastructure.Shared.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, IMapper mapper, ApplicationDBContext applicationDBContext)
        {
            _mapper = mapper;
            _applicationDBContext = applicationDBContext;
            _roleManager = roleManager;
        }

        public async Task<RoleViewModel> AddRole(CreateRoleCommand command)
        {
            var applicationRole = _mapper.Map<ApplicationRole>(command);
            applicationRole.Name = applicationRole.Name;
            applicationRole.IsActive = IsActive.Active;
            applicationRole.CreatedBy = 0;
            applicationRole.CreatedOn = DateTime.Now;
            applicationRole.ModifiedBy = 0;
            applicationRole.ModifiedOn = DateTime.Now;

            var identityResult = await _roleManager.CreateAsync(applicationRole);
            if (identityResult.Succeeded)
            {
                
            }

            return _mapper.Map<RoleViewModel>(applicationRole);
        }

        public async Task DeleteRole(DeleteRoleCommand _object)
        {
            if (_object.Id > 0)
            {
                var applicationRole = await _roleManager.FindByIdAsync(_object.Id.ToString());
                applicationRole.IsActive = IsActive.Delete;
                //await _roleManager.DeleteAsync(applicationRole);
                await _roleManager.UpdateAsync(applicationRole);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<RoleViewModel> EditRole(UpdateRoleCommand command)
        {
            var applicationRole = await _roleManager.FindByIdAsync(command.Id.ToString());
            _mapper.Map(command, applicationRole);

            var identityResult = await _roleManager.UpdateAsync(applicationRole);

            if (identityResult.Succeeded)
            {
                
            }

            return _mapper.Map<RoleViewModel>(applicationRole);
        }

        public async Task<RoleViewModel> GetRoleById(long id)
        {
            var applicationRole = await _roleManager.FindByIdAsync(id.ToString());            
            var result = _mapper.Map<RoleViewModel>(applicationRole);
            
            return result;
        }

        public async Task<List<RoleListModel>> GetRoleList()
        {
            var roles = await _roleManager.Roles
                .Select(y => new RoleListModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                    IsActive = y.IsActive,
                    CreatedBy = y.CreatedBy,
                    CreatedOn = y.CreatedOn,
                    ModifiedBy = y.ModifiedBy,
                    ModifiedOn = y.ModifiedOn,
                }).Where(x => x.IsActive != IsActive.Delete).OrderByDescending(x => x.Id).ToListAsync();

            return _mapper.Map<List<RoleListModel>>(roles);
        }
    }
}
