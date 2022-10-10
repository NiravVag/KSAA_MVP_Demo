using AutoMapper;
using KSAA.User.Application.DTOs.User;
using KSAA.User.Application.Interfaces.Services;
using KSAA.User.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Response<UserViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserViewModel>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<Response<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var getUserById = await _userService.GetUserById(request.Id);
            return new Response<UserViewModel>(getUserById);
        }
    }
}
