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
    using Support.BankClient;

    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMerchantRepository merchantRepository;
        private readonly IAcquiringBankClient acquiringBankClient;
        private readonly ILogger<PaymentService> logger;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IMerchantRepository merchantRepository,
            IAcquiringBankClient acquiringBankClient,
            ILogger<PaymentService> logger)
        {
            this.paymentRepository = paymentRepository;
            this.merchantRepository = merchantRepository;
            this.acquiringBankClient = acquiringBankClient;
            this.logger = logger;
        }

        public async Task<BaseResult<PaymentPostResult>> ProcessPaymentAsync(PaymentPostRequest paymentPostRequest)
        {
            var result = new BaseResult<PaymentPostResult>();

            var (errors, currency) = await this.ValidatePaymentPostRequest(paymentPostRequest);
            if (errors.Any())
            {
                result.Errors = errors;
                return result;
            }

            var bankClientResult = await this.acquiringBankClient.ProcessTransactionAsync(paymentPostRequest.ToBankClientRequest());
            if (bankClientResult == null)
            {
                result.Errors.Add("bankClient", "Error while processing bank transaction.");
                return result;
            }

            try
            {
                var paymentModel = paymentPostRequest.ToPaymentModel(currency, bankClientResult);

                var paymentModelResult = await this.paymentRepository.InsertAsync(paymentModel);

                result.Result = paymentModelResult.ToPostResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error while processing payment.", new
                {
                    Class = nameof(PaymentService),
                    Method = nameof(ProcessPaymentAsync),
                    ex.Message
                });

                result.Errors.Add("paymentProcess", ex.Message);
            }

            return result;
        }

        public async Task<BaseResult<PaymentGetResult>> RetrievePaymentDetailsAsync(PaymentGetRequest paymentGetRequest)
        {
            var result = new BaseResult<PaymentGetResult>();

            var payment = await this.paymentRepository.GetByIdAsync(paymentGetRequest.PaymentId);
            if (payment != null)
            {
                result.Result = payment.ToGetResult();
                return result;
            }

            this.logger.LogInformation("Payment details not found.", new
            {
                Class = nameof(PaymentService),
                Method = nameof(RetrievePaymentDetailsAsync),
                paymentGetRequest.PaymentId
            });

            result.Errors.Add("paymentDetails", "Payment details not found.");
            return result;
        }

        private async Task<(IDictionary<string, string>, Currency)> ValidatePaymentPostRequest(PaymentPostRequest paymentPostRequest)
        {
            var errors = new Dictionary<string, string>();

            var merchant = await this.merchantRepository.GetByIdAsync(paymentPostRequest.MerchantId);
            if (merchant == null)
            {
                errors.Add("merchantNotFound", $"MerchantId '{paymentPostRequest.MerchantId}' not found.");
            }

            if (!Enum.TryParse<Currency>(paymentPostRequest.Currency, true, out var currency))
            {
                errors.Add("currencyNotValid", $"Currency '{paymentPostRequest.Currency}' is not valid.");
            }

            return (errors, currency);
        }
    }
}