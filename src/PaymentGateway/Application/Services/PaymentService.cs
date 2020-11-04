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

            if (!(await this.merchantRepository.Exists(paymentPostRequest.MerchantId)))
            {
                result.Errors.Add("merchantNotFound", $"MerchantId '{paymentPostRequest.MerchantId}' not found.");
            }

            if (!Enum.TryParse<Currency>(paymentPostRequest.Currency, true, out var currency))
            {
                result.Errors.Add("currencyNotFound", $"Currency '{paymentPostRequest.Currency}' is not valid.");
            }

            if (result.Errors.Any())
            {
                return result;
            }

            try
            {
                var bankClientResult = await this.acquiringBankClient.ProcessTransactionAsync(paymentPostRequest.ToBankClientRequest());
                if (bankClientResult == null)
                {
                    result.Errors.Add("bankClient", "Error while processing bank transaction.");
                    return result;
                }

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

        public async Task<BaseResult<IEnumerable<PaymentGetResult>>> RetrievePaymentsDetailsAsync(PaymentGetRequest paymentGetRequest)
        {
            var result = new BaseResult<IEnumerable<PaymentGetResult>>();

            var payments = await this.paymentRepository.GetPaymentsDetailsAsync(paymentGetRequest.MerchantId, paymentGetRequest.PaymentId);
            if (payments.Any())
            {
                result.Result = payments.ToDetailsResult();
                return result;
            }

            this.logger.LogInformation("Payments details not found.", new
            {
                Class = nameof(PaymentService),
                Method = nameof(RetrievePaymentsDetailsAsync),
                paymentGetRequest.MerchantId,
                paymentGetRequest.PaymentId
            });

            result.Errors.Add("paymentsDetails", "Payments details not found.");
            return result;
        }
    }
}