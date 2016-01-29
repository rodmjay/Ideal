namespace Ideal.Core.Interfaces.Settings
{
    public interface ISecureTokenServiceSettings
    {
        string IssuerUri { get; set; }
        string RootUri { get; set; }
        string IdenityUri { get; set; }
        string TokenUri { get; set; }
        string AuthUri { get; set; }
        string UserInfoUri { get; set; }
        string EndSessionUri { get; set; }
        string RevokeUri { get; set; }
    }
}