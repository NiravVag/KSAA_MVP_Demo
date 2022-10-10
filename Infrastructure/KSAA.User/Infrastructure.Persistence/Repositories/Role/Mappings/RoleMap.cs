using KSAA.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.User.Infrastructure.Persistence.Repositories.Role.Mappings
{
    public partial class RoleMap : ClassMapping<ApplicationRole>
    {
        public RoleMap()
        {
            Table("Roles");
            Lazy(true);
            Id(x => x.Id);
            Property(x => x.Name, map => { map.NotNullable(true); map.Length(50); });

        }
    }
}
