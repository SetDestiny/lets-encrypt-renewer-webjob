using LetsEncryptRenewer.WebJob.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace LetsEncryptRenewer.WebJob.Utils
{
    public static class ConfigBuilder
    {
        public static LetsEncryptConfig BuildLetsEncryptConfig(HostBuilderContext context)
        {
            var letsEncryptConfig = new LetsEncryptConfig();
            letsEncryptConfig.AcmeConfig = new AcmeConfig
            {
                RegistrationEmail = context.Configuration["LetsEncryptRenewerWebJobSettings:AcmeConfig:RegistrationEmail"],
                Host = context.Configuration["LetsEncryptRenewerWebJobSettings:AcmeConfig:Host"],
                RSAKeyLength = Convert.ToInt32(context.Configuration["LetsEncryptRenewerWebJobSettings:AcmeConfig:RSAKeyLength"]),
                PFXPassword = context.Configuration["LetsEncryptRenewerWebJobSettings:AcmeConfig:PFXPassword"],
                UseProduction = Convert.ToBoolean(context.Configuration["LetsEncryptRenewerWebJobSettings:AcmeConfig:UseProduction"]),
            };
            letsEncryptConfig.AuthorizationChallengeProviderConfig = new AuthorizationChallengeProviderConfig
            {
                DisableWebConfigUpdate = Convert.ToBoolean(context.Configuration["LetsEncryptRenewerWebJobSettings:AuthorizationChallengeProviderConfig:DisableWebConfigUpdate"])
            };
            letsEncryptConfig.AzureEnvironment = new AzureEnvironment
            {
                WebAppName = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:WebAppName"],
                ClientId = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:ClientId"],
                ClientSecret = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:ClientSecret"],
                ResourceGroupName = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:ResourceGroupName"],
                SubscriptionId = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:SubscriptionId"],
                Tenant = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:Tenant"],
                AuthenticationEndpoint = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:AuthenticationEndpoint"],
                ManagementEndpoint = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:ManagementEndpoint"],
                TokenAudience = context.Configuration["LetsEncryptRenewerWebJobSettings:AzureEnvironment:TokenAudience"],
            };
            letsEncryptConfig.CertificateSettings = new CertificateSettings
            {
                UseIPBasedSSL = Convert.ToBoolean(context.Configuration["LetsEncryptRenewerWebJobSettings:CertificateSettings:UseIPBasedSSL"])
            };
            letsEncryptConfig.IsJobEnabled = Convert.ToBoolean(context.Configuration["LetsEncryptRenewerWebJobSettings:IsJobEnabled"]);

            return letsEncryptConfig;
        }
    }
}
