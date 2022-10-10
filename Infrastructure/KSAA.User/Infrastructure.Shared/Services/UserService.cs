using AutoMapper;
using KSAA.DAL;
using KSAA.Domain.Entities;
using KSAA.User.Application.DTOs.User;
using KSAA.User.Application.Features.Users.Commands;
using KSAA.User.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KSAA.User.Infrastructure.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly RoleManager<ApplicationRole> _roleManager;

        //HttpBrowserCapabilities browserCapabilities = Request.Browser;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper, ApplicationDBContext applicationDBContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _applicationDBContext = applicationDBContext;
            _roleManager = roleManager;
        }

        public async Task<UserViewModel> AddUser(CreateUserCommand command)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(command);
            applicationUser.UserName = applicationUser.Email;
            //applicationUser.IP = HttpContext.Current.Request.UserHostAddress;
            //applicationUser.BrowserCase = browserCapabilities.Browser;
            applicationUser.IsActive = IsActive.Active;
            applicationUser.CreatedBy = 0;
            applicationUser.CreatedOn = DateTime.Now;
            applicationUser.ModifiedBy = 0;
            applicationUser.ModifiedOn = DateTime.Now;

            var identityResult = await _userManager.CreateAsync(applicationUser, command.Password);
            if (identityResult.Succeeded)
            {
                if (command.RoleId > 0)
                {
                    //_roleManager.RoleExistsAsync("");
                    //_roleManager.CreateAsync();
                    //_roleManager.FindByIdAsync();
                    //_roleManager.UpdateAsync();
                    //_roleManager.DeleteAsync();
                    //_roleManager.Roles.ToList();

                    var role = await _roleManager.FindByIdAsync(command.RoleId.ToString());
                    await _userManager.AddToRoleAsync(applicationUser, role.Name);
                }
            }
            //if (identityResult.Succeeded == false)
            //{
            //    var message = identityResult.Errors.FirstOrDefault();
            //    return message;
            //}

            return _mapper.Map<UserViewModel>(applicationUser);
        }

        public async Task DeleteUser(DeleteUserCommand command)
        {
            if (command.Id > 0)
            {
                var applicationUser = await _userManager.FindByIdAsync(command.Id.ToString());
                applicationUser.IsActive = IsActive.Delete;
                await _userManager.UpdateAsync(applicationUser);
            }
            else
            {
                throw new Application.Exceptions.BadRequestException("Invalid request");
            }
        }

        public async Task<UserViewModel> EditUser(UpdateUserCommand command)
        {
            var applicationUser = await _userManager.FindByIdAsync(command.Id.ToString());
            _mapper.Map(command, applicationUser);

            var identityResult = await _userManager.UpdateAsync(applicationUser);

            if (identityResult.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(applicationUser);
                if (roles.Any())
                {
                    await _userManager.RemoveFromRoleAsync(applicationUser, roles.FirstOrDefault());
                }
                if (command.RoleId > 0)
                {
                    var role = await _roleManager.FindByIdAsync(command.RoleId.ToString());
                    await _userManager.AddToRoleAsync(applicationUser, role.Name);
                }
            }

            return _mapper.Map<UserViewModel>(applicationUser);
        }

        public async Task<UserViewModel> GetUserById(long id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _userManager.GetRolesAsync(applicationUser);
            var result = _mapper.Map<UserViewModel>(applicationUser);
            if (roles.Any())
            {
                result.RoleId = (await _roleManager.FindByNameAsync(roles.FirstOrDefault())).Id;
            }
            return result;
        }

        public async Task<List<UserListModel>> GetUserList()
        {
            var users = await _userManager.Users
                .Select(y => new UserListModel()
                {
                    Id = y.Id,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    Email = y.Email,
                    UserTypeName = y.UserTypeNavigation.UserTypeName,
                    UserType = y.UserType,
                    Company = y.Company,
                    CompanyName = y.CompanyNavigation.CompanyName,
                    IsActive = y.IsActive,
                    UserRoleName = string.Join(", ", y.UserRoles.Select(x => x.Role.Name).ToList())
                }).Where(x => x.IsActive != IsActive.Delete).ToListAsync();

            return _mapper.Map<List<UserListModel>>(users);
        }
    }
}
