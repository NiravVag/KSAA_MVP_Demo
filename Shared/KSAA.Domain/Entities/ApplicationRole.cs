using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Domain.Entities
{
    public class ApplicationRole : IdentityRole<long>
    {
        public ApplicationRole()
        {
            UserRoles = new HashSet<ApplicationUserRole>();
        }
        //public virtual string? UserRoleName { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } 
    }
}
