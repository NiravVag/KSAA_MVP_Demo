namespace KSAA.ApiServices.Controllers.V1
{
    using KSAA.User.Application.Features.Users.Commands;
    using KSAA.User.Application.Features.Users.Queries;
    using KSAA.User.Application.Interfaces.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    //[Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpPost]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(GetAllUsersQuery command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var getUserByIdQuery = new GetUserByIdQuery()
            {
                Id = id,
            };
            return this.Ok(await this.Mediator.Send(getUserByIdQuery));
        }

        [HttpPut]
        [Route("UpdateUserById")]
        public async Task<IActionResult> UpdateUserById(UpdateUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleteUserByIdQuery = new DeleteUserCommand()
            {
                Id = id,
            };
            return this.Ok(await this.Mediator.Send(deleteUserByIdQuery));
        }
    }
}
