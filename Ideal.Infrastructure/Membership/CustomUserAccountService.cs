using BrockAllen.MembershipReboot;

namespace Ideal.Infrastructure.Membership
{
	public class CustomUserAccountService : UserAccountService<CustomUser>
	{
		public CustomUserAccountService(CustomConfig config, CustomUserRepository repo)
			: base(config, repo)
		{
		}
	}
}