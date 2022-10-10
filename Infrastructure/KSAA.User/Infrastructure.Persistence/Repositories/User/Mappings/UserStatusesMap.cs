using KSAA.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace KSAA.User.Infrastructure.Persistence.Repositories.User.Mappings
{
    public partial class UserStatusesMap : ClassMapping<UserStatuses>
    {
        public UserStatusesMap()
        {
            Table("UserStatuses");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.StatusDescription, map => map.Length(50));
            Property(x => x.StatusValue, map => { map.NotNullable(true); map.Length(50); });
        }
    }
}
