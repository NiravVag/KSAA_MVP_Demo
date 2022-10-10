using KSAA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Application.DTOs.Role
{
    public class RoleListModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public IsActive IsActive { get; set; }
        public virtual int? CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual int? ModifiedBy { get; set; }
        public virtual DateTime? ModifiedOn { get; set; }
    }
}
