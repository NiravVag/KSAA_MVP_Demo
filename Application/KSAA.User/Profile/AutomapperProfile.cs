using KSAA.User.Application.Features.Users.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSAA.User.Application.DTOs.User;
using KSAA.Domain.Entities;
using KSAA.User.Application.DTOs.Role;
using KSAA.User.Application.Features.Role.Commands;

namespace KSAA.User.Application
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CreateUserCommand,ApplicationUser>();
            CreateMap<UpdateUserCommand, ApplicationUser>();
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<CreateRoleCommand, ApplicationRole>();
            CreateMap<UpdateRoleCommand, ApplicationRole>();
            CreateMap<ApplicationRole, RoleViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
        }
    }
}
