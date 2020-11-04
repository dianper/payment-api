namespace Support.BankClient
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class AcquiringBankClient : IAcquiringBankClient
    {
        private readonly HttpClient httpClient;

        public AcquiringBankClient()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44385"),
                Timeout = TimeSpan.FromMilliseconds(1000)
            };
        }

        public async Task<BankClientResult> ProcessTransaction(BankClientRequest bankClientRequest)
        {
            try
            {
                var json = JsonConvert.SerializeObject(bankClientRequest);
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponseMessage = await this.httpClient.PostAsync("/api/v1/transactions", body);
                string result = await httpResponseMessage.Content.ReadAsStringAsync();                
                return JsonConvert.DeserializeObject<BankClientResult>(result);
            }
            catch (Exception ex)
            {
                return new BankClientResult(Guid.NewGuid(), "");
            }            
        }
    }
}