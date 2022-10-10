namespace KSAA.ApiServices.Controllers.V1
{
    using KSAA.User.Application.Features.Role.Commands;
    using KSAA.User.Application.Features.Role.Queries;
    using KSAA.User.Application.Interfaces.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    public class RoleController : BaseApiController
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> Post(CreateRoleCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpPost]
        [Route("GetAllRole")]
        public async Task<IActionResult> GetAllRole(GetAllRoleQuery command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var getRoleByIdQuery = new GetRoleByIdQuery()
            {
                Id = id,
            };
            return this.Ok(await this.Mediator.Send(getRoleByIdQuery));
        }

        [HttpPut]
        [Route("UpdateRoleById")]
        public async Task<IActionResult> UpdateRoleById(UpdateRoleCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var deleteRoleByIdQuery = new DeleteRoleCommand()
            {
                Id = id,
            };
            return this.Ok(await this.Mediator.Send(deleteRoleByIdQuery));
        }
    }
}
