using Ideal.Infrastructure.Membership;
using IdentityManager.MembershipReboot;

namespace Ideal.IdentityManager
{
	public class CustomIdentityManagerService : MembershipRebootIdentityManagerService<CustomUser, CustomGroup>
	{
		public CustomIdentityManagerService(CustomUserAccountService userSvc, CustomGroupService groupSvc)
			: base(userSvc, groupSvc)
		{
		}
	}
}