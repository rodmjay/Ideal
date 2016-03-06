using BrockAllen.MembershipReboot;

namespace Ideal.Infrastructure.Membership
{
    public class CustomConfig : MembershipRebootConfiguration<CustomUser>
    {
        public static readonly CustomConfig Config;
        
        static CustomConfig()
        {
            Config = new CustomConfig();
            Config.PasswordHashingIterationCount = 10000;
            Config.RequireAccountVerification = false;
            //config.EmailIsUsername = true;
        }
    }
}