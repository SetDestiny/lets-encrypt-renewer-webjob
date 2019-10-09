<b>LetsEncryptRenewer.WebJob</b>
<hr>
WebJob that based on .net core web-job-sdk for renewing Azure Web App TLS/SSL certificates using Azure Let's Encrypt (No Web Jobs) Extension API.
<br>
<b>Configuration</b>
<hr>
<pre>
{
  "LetsEncryptRenewerWebJobSettings": {
    "ApplicationInsights": {
      "LogLevel": {
        "Default": "Warning"
      },
      "Instrumentationkey": "",
      "AdditionalLogProperties": {
        "Category": "",
        "Application": "LetsEncryptRenewer WebJob",
        "Env": "dev"
      }
    },
    "TimerSettings": "",
    "AcmeConfig": {
      "RegistrationEmail": "",
      "Host": "",
      "RSAKeyLength": 2048,
      "PFXPassword": "",
      "UseProduction": true
    },
    "AuthorizationChallengeProviderConfig": {
      "DisableWebConfigUpdate": false
    },
    "AzureEnvironment": {
      "WebAppName": "",
      "ClientId": "",
      "ClientSecret": "",
      "ResourceGroupName": "",
      "SubscriptionId": "",
      "Tenant": "",
      "AuthenticationEndpoint": "https://login.windows.net/",
      "ManagementEndpoint": "https://management.azure.com",
      "TokenAudience": "https://management.core.windows.net/"
    },
    "CertificateSettings": {
      "UseIPBasedSSL": false
    },
    "IsJobEnabled": false
  }
}
</pre>
