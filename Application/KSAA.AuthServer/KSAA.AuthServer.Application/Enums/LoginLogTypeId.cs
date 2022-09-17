using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.AuthServer.ApiServices.Application.Enums
{
    public enum LoginLogTypeId
    {
        Success = 1,
        IncorrectUserNameOrPassword = 2,
        UserInActive = 3,
        SSOSuccess = 4,
    }
}
