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
    public class GetRoleByIdQuery : IRequest<Response<RoleViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<RoleViewModel>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        public async Task<Response<RoleViewModel>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var getRoleById = await _roleService.GetRoleById(request.Id);
            return new Response<RoleViewModel>(getRoleById);
        }
    }
}
