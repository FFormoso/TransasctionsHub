using _011Global.JobsService.JobInterfaces;
using _011Global.JobsService.Settings;
using _011Global.Shared;
using _011Global.Shared.CustomerDbContext.Enums;
using _011Global.Shared.CustomerDbContext.Interfaces;
using _011Global.Shared.PaymentGateways.Builders;
using _011Global.Shared.PaymentGateways.Interfaces;
using _011Global.Shared.PaymentGateways.DTOs;
using Microsoft.EntityFrameworkCore;

namespace _011Global.JobsService.JobImplementations;

public class RecurringChargeJob : Job, IJob
{
    private readonly IServiceProvider _serviceProvider;

    private readonly AppSettingsManager _appSettings;
    protected override int IterationWaitTime => _appSettings.RecurringChargeJobIterationTime;

    public RecurringChargeJob(
        CancellationTokenBase cancellationTokenBase, ILogger<RecurringChargeJob> logger,
        IServiceProvider serviceProvider, AppSettingsManager appSettings)
        : base(cancellationTokenBase, logger, serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _appSettings = appSettings;
    }

    protected override async Task WorkLoad()
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var customerRepo = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
        var paymentGateway = scope.ServiceProvider.GetRequiredKeyedService<IPaymentGateway>(_appSettings.RecurringChargeJobPaymentGateway);

        var customers = await customerRepo.GetCustomersInDue().ToListAsync(cancellationToken);
        
        foreach (var customer in customers)
        {
            try
            {
                foreach (var creditCard in customer.CreditCards)
                {
                    logger.LogInformation("Processing payment for customer {customerId} with Credit Card {creditCardId}"
                        , customer.CustomerID, creditCard.CreditCardID);
                    
                    var requestBuilder = PaymentRequestBuilder
                        .CreateRecurringPaymentRequest(customer)
                        .WithCustomerBasicInfo()
                        .WithCreditCard(creditCard)
                        .WithBillingAddress()
                        .WithShippingAddress()
                        .WithSystemInfo(assemblyName: Name);

                    if (!creditCard.Tokenized)
                        requestBuilder.WithCardTokenization();

                    var request = requestBuilder.Build();

                    PaymentResponse paymentRes = await paymentGateway.ProcessPaymentAsync(request);

                    if (request.CardTokenization && paymentRes.CreditCard != null)
                    {
                        creditCard.CreditCardNumber = paymentRes.CreditCard.CreditCardNumber;
                        creditCard.Tokenized = true;
                    }

                    await customerRepo.SaveTransaction(paymentRes, creditCard, customer.CustomerID);

                    if (paymentRes.TransactionStatus != TransactionStatus.Approved)
                    {
                        // Notify of the failure in some way
                        logger.LogInformation(
                            "Payment for customer {customerId} with Credit Card {creditCardId} was not approved. Response code: {responseCode}", 
                            customer.CustomerID, creditCard.CreditCardID, paymentRes.ResponseCode);
                    }
                    else
                    {
                        logger.LogInformation("Payment for customer {customerId} was approved.", customer.CustomerID);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Error processing payment for customer {customer.CustomerID}", ex);
            }
        }


        logger.LogInformation($"{Name} my last run time was: {DateTime.Now}");
    }
}