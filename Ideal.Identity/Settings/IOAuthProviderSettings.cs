namespace Ideal.Identity.Settings
{
    public interface IOAuthProviderSettings
    {
        string Secret { get; }
        string AuthEndpoint { get; set; }
    }
}
