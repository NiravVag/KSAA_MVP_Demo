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
    public partial class CreateRoleCommand : IRequest<Response>
    {
        [Required]
        public virtual string? Name { get; set; }
        
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response>
    {
        private readonly IRoleService _roleService;
        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<Response> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleObject = await _roleService.AddRole(request);
            return new Response("Role Created Successfully");
        }
    }
}
