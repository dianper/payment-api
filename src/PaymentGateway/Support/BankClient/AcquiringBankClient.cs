namespace Support.BankClient
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Support.Configuration;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Logging;

    public class AcquiringBankClient : IAcquiringBankClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<AcquiringBankClient> logger;
        private readonly BankConfiguration bankConfiguration;

        public AcquiringBankClient(
            IHttpClientFactory httpClientFactory,
            ILogger<AcquiringBankClient> logger,
            BankConfiguration bankConfiguration)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.bankConfiguration = bankConfiguration;
        }

        public async Task<BankClientResult> ProcessTransactionAsync(BankClientRequest bankClientRequest)
        {
            try
            {
                var json = JsonConvert.SerializeObject(bankClientRequest);
                var bodyContent = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = this.httpClientFactory.CreateClient(this.bankConfiguration.ServiceName))
                {
                    var httpResponseMessage = await client.PostAsync(this.bankConfiguration.Endpoint, bodyContent);
                    var stringResult = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<BankClientResult>(stringResult);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error while processing bank transaction.", new
                {
                    Class = nameof(AcquiringBankClient),
                    Method = nameof(ProcessTransactionAsync),
                    ex.Message
                });

                return default;
            }
        }
    }
}