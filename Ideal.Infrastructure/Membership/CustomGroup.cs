using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;

namespace Ideal.Infrastructure.Membership
{
    public class CustomGroup : RelationalGroup
    {
        public virtual string Description { get; set; }
    }

	public class CustomGroupRepository : DbContextGroupRepository<CustomDatabase, CustomGroup>
    {
        public CustomGroupRepository(CustomDatabase ctx)
            : base(ctx)
        {
        }
    }

}