using BrockAllen.MembershipReboot.Ef;

namespace Ideal.Infrastructure.Membership
{
    public class CustomDatabase : MembershipRebootDbContext<CustomUser, CustomGroup>
    {
        public CustomDatabase(string name)
            :base(name)
        {
        }
    }
}