namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Extensions;
    using Application.Models;
    using Microsoft.Extensions.Logging;
    using Repository.Enums;
    using Repository.Interfaces;

    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMerchantRepository merchantRepository;
        private readonly ILogger<PaymentService> logger;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IMerchantRepository merchantRepository,
            ILogger<PaymentService> logger)
        {
            this.paymentRepository = paymentRepository;
            this.merchantRepository = merchantRepository;
            this.logger = logger;
        }

        public async Task<BaseResult<PaymentPostResult>> ProcessPaymentAsync(PaymentPostRequest paymentPostRequest)
        {
            // TODO: Validator
            var errors = new Dictionary<string, string>();

            if (!(await this.merchantRepository.Exists(paymentPostRequest.MerchantId)))
            {
                errors.Add("merchantNotFound", $"MerchantId '{paymentPostRequest.MerchantId}' not found.");
            }

            if (!Enum.TryParse<Currency>(paymentPostRequest.Currency, true, out var enumResult))
            {
                errors.Add("currencyNotFound", $"Currency '{paymentPostRequest.Currency}' is not valid.");
            }

            if (errors.Any())
            {
                return new BaseResult<PaymentPostResult>(errors: errors);
            }

            try
            {
                // TODO: Mock Acquiring Bank
                var transactionStatus = "Success";
                var payment = await this.paymentRepository.InsertAsync(paymentPostRequest.ToPaymentModel(enumResult, transactionStatus));

                return new BaseResult<PaymentPostResult>(payment.ToPostResult());
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error while processing payment.", new
                {
                    Class = nameof(PaymentService),
                    Method = nameof(ProcessPaymentAsync),
                    ex.Message
                });

                errors.Add("paymentProcess", ex.Message);

                return new BaseResult<PaymentPostResult>(errors: errors);
            }
        }

        public async Task<BaseResult<PaymentGetResult>> RetrievePaymentDetailsAsync(PaymentGetRequest paymentGetRequest)
        {
            var errors = new Dictionary<string, string>();
            var payment = await this.paymentRepository.GetByIdAsync(paymentGetRequest.PaymentId);
            if (payment == null)
            {
                this.logger.LogInformation("PaymentId not found.", new
                {
                    Class = nameof(PaymentService),
                    Method = nameof(RetrievePaymentDetailsAsync),
                    PaymentId = paymentGetRequest.PaymentId
                });

                errors.Add("paymentNotFound", $"PaymentId '{paymentGetRequest.PaymentId}' not found.");
            }

            return new BaseResult<PaymentGetResult>(payment.ToGetResult(), errors);
        }
    }
}