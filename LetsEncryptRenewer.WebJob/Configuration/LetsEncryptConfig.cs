namespace LetsEncryptRenewer.WebJob.Configuration
{
    public class LetsEncryptConfig
    {
        public AzureEnvironment AzureEnvironment { get; set; }
        public AcmeConfig AcmeConfig { get; set; }
        public CertificateSettings CertificateSettings { get; set; }
        public AuthorizationChallengeProviderConfig AuthorizationChallengeProviderConfig { get; set; }
        public bool IsJobEnabled { get; set; }
    }

    public class AzureEnvironment
    {
        public string AzureWebSitesDefaultDomainName { get; set; } //Defaults to azurewebsites.net
        public string ServicePlanResourceGroupName { get; set; } //Defaults to ResourceGroupName
        public string SiteSlotName { get; set; } //Not required if site slots isn't used
        public string WebAppName { get; set; }
        public string AuthenticationEndpoint { get; set; } //Defaults to https://login.windows.net/
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ManagementEndpoint { get; set; } //Defaults to https://management.azure.com
        public string ResourceGroupName { get; set; }
        public string SubscriptionId { get; set; }
        public string Tenant { get; set; }
        public string TokenAudience { get; set; }
    }

    public class AcmeConfig
    {
        public string RegistrationEmail { get; set; }
        public string Host { get; set; }
        public string[] AlternateNames { get; set; } = new string[] { };
        public int RSAKeyLength { get; set; } = 2048;
        public string PFXPassword { get; set; }
        public bool UseProduction { get; set; } = false;
    }

    public class CertificateSettings
    {
        public bool UseIPBasedSSL { get; set; } = false;
    }

    public class AuthorizationChallengeProviderConfig
    {
        public bool DisableWebConfigUpdate { get; set; } = false;
    }
}
