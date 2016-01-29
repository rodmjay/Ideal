using System.ComponentModel;

namespace Ideal.Membership.Model
{
    public enum MembershipEventCode : int
    {
        [Description("New User Created")]
        UserCreated = 1000001,

        [Description("User Login")]
        UserLogin = 1000002,

        [Description("User Logout")]
        UserLogout = 1000003,

        [Description("User Locked Out")]
        UserLockedOut = 1000004
    }
}