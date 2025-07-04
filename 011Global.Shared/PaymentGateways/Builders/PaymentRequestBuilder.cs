using _011Global.Shared.CustomerDbContext.Entities;
using _011Global.Shared.PaymentGateways.DTOs;

//using Customer = _011Global.Shared.CustomerDbContext.Entities.Customer;

namespace _011Global.Shared.PaymentGateways.Builders;

public class PaymentRequestBuilder
{
    private readonly Customer _customer;
    private readonly PaymentRequest _paymentRequest;
    
    private PaymentRequestBuilder(Customer customer)
    {
        _customer = customer;
        _paymentRequest = new PaymentRequest();
    }
    
    public static PaymentRequestBuilder CreateRecurringPaymentRequest(Customer customer)
    {
        return new PaymentRequestBuilder(customer)
        {
            _paymentRequest = {
                PartialAuth = false,
                PaymentType = PaymentType.Recurring
            }
        };
    }

    
    public PaymentRequestBuilder WithCreditCard(CreditCard card)
    {
        _paymentRequest.CreditCard = card;
        return this;
    }

    public PaymentRequestBuilder WithCardTokenization()
    {
        _paymentRequest.CardTokenization = true;
        return this;
    }
    
    public PaymentRequestBuilder WithCustomerBasicInfo()
    {
        _paymentRequest.Amount = _customer.MonthlyFee;
        _paymentRequest.CustomerEmail = _customer.CustomerEmail;
        return this;
    }
    
    public PaymentRequestBuilder WithBillingAddress()
    {
        _paymentRequest.BillingAddress = _customer.BillingAddress;
        return this;
    }

    public PaymentRequestBuilder WithShippingAddress()
    {
        _paymentRequest.ShippingAddress = _customer.ShippingAddress;
        return this;
    }

    public PaymentRequestBuilder WithSystemInfo(string assemblyName)
    {
        _paymentRequest.Clerk = Environment.MachineName;
        _paymentRequest.Software = assemblyName;
        return this;
    }

    public PaymentRequest Build()
    {
        return _paymentRequest;
    }
}