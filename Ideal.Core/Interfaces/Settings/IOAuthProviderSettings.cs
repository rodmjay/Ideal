namespace Ideal.Core.Interfaces.Settings
{
    public interface IOAuthProviderSettings
    {
        string Secret { get; }
        string AuthEndpoint { get; set; }
    }
}
