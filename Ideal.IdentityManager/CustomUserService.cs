using Ideal.Infrastructure.Membership;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;

namespace Ideal.IdentityManager
{
    public static class CustomUserServiceExtensions
    {
        public static void ConfigureCustomUserService(this IdentityServerServiceFactory factory, string connString)
        {
            factory.UserService = new Registration<IUserService, CustomUserService>();
            factory.Register(new Registration<CustomUserAccountService>());
            factory.Register(new Registration<CustomConfig>(CustomConfig.Config));
            factory.Register(new Registration<CustomUserRepository>());
            factory.Register(new Registration<CustomDatabase>(resolver => new CustomDatabase(connString)));
        }
    }
    
    public class CustomUserService : MembershipRebootUserService<CustomUser>
    {
        public CustomUserService(CustomUserAccountService userSvc)
            : base(userSvc)
        {
        }
    }
}
