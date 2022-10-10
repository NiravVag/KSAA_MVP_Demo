using KSAA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.DTOs.User
{
    public class UserListModel
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long UserType { get; set; }
        public string? UserTypeName { get; set; }
        public long Company { get; set; }
        public string? CompanyName { get; set; }
        public string? UserRoleName { get; set; }
        public IsActive IsActive { get; set; }
    }

    //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        //public object password { get; set; }
        public long userType { get; set; }
        public string userTypeName { get; set; }
        public long company { get; set; }
        public string companyName { get; set; }
        public string userRoleName { get; set; }
        public IsActive isActive { get; set; }

    }

    public class Root
    {
        public List<Data> data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}
