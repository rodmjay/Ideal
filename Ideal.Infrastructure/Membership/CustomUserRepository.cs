using BrockAllen.MembershipReboot.Ef;

namespace Ideal.Infrastructure.Membership
{
	public class CustomUserRepository : DbContextUserAccountRepository<CustomDatabase, CustomUser>
	{
		public CustomUserRepository(CustomDatabase ctx)
			: base(ctx)
		{
		}
	}
}