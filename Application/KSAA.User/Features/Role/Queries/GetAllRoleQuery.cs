using AutoMapper;
using KSAA.User.Application.DTOs.Role;
using KSAA.User.Application.Interfaces.Services;
using KSAA.User.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.Features.Role.Queries
{
    public class GetAllRoleQuery : IRequest<Response<IEnumerable<RoleListModel>>>
    {
    }

    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, Response<IEnumerable<RoleListModel>>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetAllRoleQueryHandler(IRoleService userService, IMapper mapper)
        {
            _roleService = userService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<RoleListModel>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _roleService.GetRoleList();
            return new Response<IEnumerable<RoleListModel>>(usersList);
        }
    }
}
