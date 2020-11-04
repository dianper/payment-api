namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using Application.Models;
    using Application.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PaymentPostRequest paymentPostRequest)
        {
            var result = await this.paymentService.ProcessPaymentAsync(paymentPostRequest);
            if (result.Success)
            {
                return this.Ok(result);
            }

            return this.BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaymentGetRequest paymentGetRequest)
        {
            var result = await this.paymentService.RetrievePaymentsDetailsAsync(paymentGetRequest);
            if (result.Success)
            {
                return this.Ok(result);
            }

            return this.NotFound(result);
        }
    }
}