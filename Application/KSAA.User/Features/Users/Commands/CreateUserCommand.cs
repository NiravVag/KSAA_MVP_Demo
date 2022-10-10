using KSAA.User.Application.Interfaces.Services;
using KSAA.User.Application.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KSAA.User.Application.Features.Users.Commands
{
    public partial class CreateUserCommand : IRequest<Response>
    {
        [Required]
        public virtual string? FirstName { get; set; }
        [Required]
        public virtual string? LastName { get; set; }
        [Required]
        public virtual string? Email { get; set; }
        [Required]
        public virtual string? Password { get; set; }
        [Required]
        public long UserType { get; set; }
        [Required]
        public long Company { get; set; }        
        [Required]
        public int RoleId { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {                       
            var userObject = await _userService.AddUser(request);
            return new Response("User Created Successfully");
        }
    }
}
