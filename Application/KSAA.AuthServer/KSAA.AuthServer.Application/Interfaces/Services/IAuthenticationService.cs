using KSAA.AuthServer.ApiServices.Application.DTOs.Authentication;
using KSAA.User.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.AuthServer.ApiServices.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
        Task<Response<string>> RevokeTokenAsync(RevokeTokenRequest request, string ipAddress);
    }
}
