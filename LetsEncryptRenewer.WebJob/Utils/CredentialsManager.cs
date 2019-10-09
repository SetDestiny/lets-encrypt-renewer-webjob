using LetsEncryptRenewer.WebJob.Configuration;
using LetsEncryptRenewer.WebJob.Contracts;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LetsEncryptRenewer.WebJob.Utils
{
    public static class CredentialsManager
    {
        public static async Task<string> GetAccessToken(AzureEnvironment config)
        {
            string authContextURL = config.AuthenticationEndpoint + config.Tenant;
            var authenticationContext = new AuthenticationContext(authContextURL);
            var credential = new ClientCredential(config.ClientId, config.ClientSecret);
            var result = await authenticationContext.AcquireTokenAsync(config.ManagementEndpoint, credential);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token!");
            }
            return result.AccessToken;
        }

        public static async Task<PublishingCredentials> GetPublishingCredentials(AzureEnvironment config, string token)
        {
            var requestUrl = string.Format(Constants.PublishingCredentialsUrlTemplate, config.SubscriptionId, config.ResourceGroupName, config.WebAppName);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

            var result = await client.PostAsync(requestUrl, new StringContent("", Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                PublishingCredentials publishingCredentials = JsonConvert.DeserializeObject<PublishingCredentials>(content);
                return publishingCredentials;
            }
            else
            {
                throw new InvalidOperationException("Failed to obtain publishing credentials!");
            }
        }

    }
}
