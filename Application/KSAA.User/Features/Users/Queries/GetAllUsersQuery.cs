using AutoMapper;
using KSAA.User.Application.DTOs.User;
using KSAA.User.Application.Interfaces.Repositories;
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
    public class GetAllUsersQuery : IRequest<Response<IEnumerable<UserListModel>>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserListModel>>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<UserListModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _userService.GetUserList();
            return new Response<IEnumerable<UserListModel>>(usersList);
        }
    }
}
