namespace WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using WebApi.Enums;
    using WebApi.Models;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult<TransactionResult> PostAsync([FromBody]TransactionRequest transactionRequest)
        {
            var result = new TransactionResult();

            if (transactionRequest.CardNumber.Equals("4485008383107041"))
            {
                result.TransactionStatus = TransactionStatus.Failed.ToString();
            }
            else if (transactionRequest.CardNumber.Equals("5468797069745763"))
            {
                result.TransactionStatus = TransactionStatus.Rejected.ToString();
            }
            else if (transactionRequest.Amount < 150)
            {
                result.TransactionStatus = TransactionStatus.Rejected.ToString();
            }
            else
            {
                result.TransactionStatus = TransactionStatus.Accepted.ToString();
            }

            return result;
        }
    }
}