namespace LetsEncryptRenewer.WebJob.Configuration
{
    public static class Constants
    {
        /// <summary>
        /// {0} - subscriptionId
        /// {1} - resourceGroup
        /// {2} - webApp
        /// </summary>
        public const string PublishingCredentialsUrlTemplate = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Web/sites/{2}/config/publishingcredentials/list?api-version=2016-08-01";
        /// <summary>
        /// {0} - webAppName
        /// </summary>
        public const string LetsEncryptApiUrlTemplate = "https://{0}.scm.azurewebsites.net/letsencrypt/api/certificates/challengeprovider/http/kudu/certificateinstall/azurewebapp?api-version=2017-09-01";
    }
}

