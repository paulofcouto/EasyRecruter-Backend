using Google.Cloud.SecretManager.V1;

namespace Easy.Infrastructure.Persistence
{
    public static class SecretManagerHelper
    {
        public static string GetSecret(string secretName)
        {
            SecretManagerServiceClient client;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                client = new SecretManagerServiceClientBuilder
                {
                    CredentialsPath = "C:\\Projetos\\Credenciais\\winged-guild-443820-h2-ebbf95e1e03d.json"
                }.Build();
            }
            else
            {
                client = SecretManagerServiceClient.Create();
            }

            var secret = client.AccessSecretVersion($"projects/496739670499/secrets/mongo-connection-string/versions/4");
            return secret.Payload.Data.ToStringUtf8();
        }
    }
}
