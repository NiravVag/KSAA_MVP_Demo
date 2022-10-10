using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.DTOs.User
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public long UserType { get; set; }
        public long Company { get; set; }
        public int IsActive { get; set; }
        public long RoleId { get; set; }

    }

    //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data1
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public object password { get; set; }
        public long userType { get; set; }
        public long company { get; set; }
        //public long userRoles { get; set; }
        //public string userRoleName { get; set; }
        public int isActive { get; set; }
        public int roleId { get; set; }

    }

    //public class Root
    //{
    //    public List<Data> data { get; set; }
    //    public bool succeeded { get; set; }
    //    public object message { get; set; }
    //}

    public class Root1
    {
        public Data1 data { get; set; }
        public bool succeeded { get; set; }
        public object message { get; set; }
    }
}
