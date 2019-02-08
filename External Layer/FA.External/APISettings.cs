using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System.Configuration;

namespace FA.External
{
    public class APISettings
    {
        public const string UriBase = "https://southeastasia.api.cognitive.microsoft.com/face/v1.0/";
        public string SubscriptionKey { get; private set; }

        public APISettings()
        {
            try
            {
                var client = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

                var result = client
                    .GetSecretAsync("https://faceapi.vault.azure.net/secrets/Subscription-Key/f885329076364b719f6e61e69ae263e4")
                    .Result;

                SubscriptionKey = result.Value;
            }

            catch
            {
                SubscriptionKey = ConfigurationManager.AppSettings["SubscriptionKey"];
            }
        }
    }
}
