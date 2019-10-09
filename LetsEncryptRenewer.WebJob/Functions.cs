using LetsEncryptRenewer.WebJob.Configuration;
using LetsEncryptRenewer.WebJob.Contracts;
using LetsEncryptRenewer.WebJob.Utils;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LetsEncryptRenewer.WebJob
{
    public partial class Functions
    {
        private readonly LetsEncryptConfig _letsEncryptConfig;
        private readonly ITelemetryLogger _appInsightLogger;

        public Functions(LetsEncryptConfig letsEncryptConfig, ITelemetryLogger appInsightLogger)
        {
            _letsEncryptConfig = letsEncryptConfig;
            _appInsightLogger = appInsightLogger;
        }

        [Singleton]
        public async Task LetsEncryptRenewerTimerTriggerAsync([TimerTrigger("%LetsEncryptRenewerWebJobSettings:TimerSettings%", RunOnStartup = true, UseMonitor = true)] TimerInfo timerInfo, ILogger logger)
        {
            if (_letsEncryptConfig.IsJobEnabled)
            {
                PublishingCredentials publishingCredentials = null;
                try
                {
                    logger.LogInformation($"LetsEncryptRenewerWebJob started!");
                    logger.LogInformation($"Trying to obtain token...");
                    var token = await CredentialsManager.GetAccessToken(_letsEncryptConfig.AzureEnvironment);
                    logger.LogInformation($"Token obtain completed!");

                    logger.LogInformation($"Trying to obtain publishing credentials...");
                    publishingCredentials = await CredentialsManager.GetPublishingCredentials(_letsEncryptConfig.AzureEnvironment, token);
                    logger.LogInformation($"Publishing credentials were retrieved successfully!");
                }
                catch (Exception ex)
                {
                    _appInsightLogger.TrackException(ex);
                    logger.LogInformation($"Failed to obtain credentials!");
                }
                if (publishingCredentials != null)
                {
                    try
                    {
                        logger.LogInformation($"Proceed lets encrypt renewer api...");
                        var client = new HttpClient();
                        var renewUrl = string.Format(Constants.LetsEncryptApiUrlTemplate, _letsEncryptConfig.AzureEnvironment.WebAppName);
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{publishingCredentials.Properties.PublishingUserName}:{publishingCredentials.Properties.PublishingPassword}")));
                        var res = await client.PostAsync(renewUrl, new StringContent(JsonConvert.SerializeObject(_letsEncryptConfig, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json"));

                        switch (res.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                logger.LogInformation($"Lets encrypt certificate succsessfully renewed...");
                                break;
                            default:
                                logger.LogInformation($"Failed to renew certificate, something wrong with lets encrypt renewer api or configuration!");
                                var ex = new Exception(string.Format("Failed to renew certificate! : Response: {0}", await res.Content.ReadAsStringAsync()));
                                _appInsightLogger.TrackException(ex);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _appInsightLogger.TrackException(ex);
                        logger.LogInformation($"Exception during lets encrypt renewer api call!");
                    }
                }
            }
            else
            {
                logger.LogInformation($"Job disabled by settings!");
            }
        }
    }
}
