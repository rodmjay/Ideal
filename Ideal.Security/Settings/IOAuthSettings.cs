namespace Ideal.Security.Settings
{
    public interface IOAuthSettings
    {
        string Secret { get; }
        string IssuerUri { get; }
        string STSOrigin { get; }
        string STSTokenEndpoint { get; }
        string AuthorizationEndpoint { get; }
        string UserInfoEndpoint { get; }
        string EndSessionEndpoint { get; }
        string RevokeTokenEndpoint { get; }
    }
}
