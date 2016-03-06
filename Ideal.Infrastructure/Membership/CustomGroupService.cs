using BrockAllen.MembershipReboot;

namespace Ideal.Infrastructure.Membership
{
	public class CustomGroupService : GroupService<CustomGroup>
	{
		public CustomGroupService(CustomGroupRepository repo, CustomConfig config)
			: base(config.DefaultTenant, repo)
		{

		}
	}
}