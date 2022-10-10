using KSAA.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace KSAA.User.Infrastructure.Persistence.Repositories
{
    public partial class UserMap : ClassMapping<ApplicationUser>
    {
        public UserMap()
        {
            Table("Users");
            Lazy(true);
            Id(x => x.Id);
            Property(x => x.FirstName, map => { map.NotNullable(true); map.Length(50); });
            Property(x => x.LastName, map => map.Length(50));
            Property(x => x.Email, map => { map.NotNullable(true); map.Length(50); });
            Property(x => x.PasswordHash, map => map.NotNullable(true));           

        }
    }
}
