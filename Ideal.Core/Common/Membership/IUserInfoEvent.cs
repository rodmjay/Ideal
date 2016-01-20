namespace Ideal.Core.Common.Membership
{
    public interface IUserInfoEvent
    {
        string Tenant { get; }
        string Username { get; }
    }
}
