using AutoMapper;
using KSAA.User.Application.Interfaces.Services;
using KSAA.User.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.Features.Role.Commands
{
    public class UpdateRoleCommand : IRequest<Response>
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleService.EditRole(request);

            return new Response("Role Updated Successfully");
        }
    }
}
