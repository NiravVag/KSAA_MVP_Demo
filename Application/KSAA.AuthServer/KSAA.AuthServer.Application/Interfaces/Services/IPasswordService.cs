using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.AuthServer.ApiServices.Application.Interfaces.Services
{
    public interface IPasswordService
    {
        void CreatePassword(string password);

        bool VerifyPassword(string password);

        string Base64Decode(string password);

        bool IsBase64String(string s);

        string GeneratePassword(int passwordlength);
    }
}
