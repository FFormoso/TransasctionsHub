using _011Global.Shared.CustomerDbContext.Enums;
using _011Global.Shared.PaymentGateways.DTOs;
using _011Global.Shared.PaymentGateways.Interfaces;
using _011Global.Shared.PaymentGateways.USAePay.Requests;

namespace _011Global.Shared.PaymentGateways.USAePay;

public class USAePayPaymentGatewayAdapter(USAePayService service) : IPaymentGateway
{
    public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
    {
        //TODO: Encapsulate this mapping
        SaleRequest req = new()
        {
            Command = "sale",
            Amount_Detail = new AmountDetail { Enable_Partialauth = request.PartialAuth },
            Traits = new Traits
            {
                Is_Recurring = request.PaymentType == PaymentType.Recurring,
                Stored_Credential = request.PaymentType.ToString().ToLower()
            },
            Creditcard = new CreditCard
            {
                Number = request.CreditCard.CreditCardNumber,
                Expiration = request.CreditCard.ExpirationMonth + request.CreditCard.ExpirationYear.Substring(2, 2)
            },
            Save_Card = request.CardTokenization,
            Amount = request.Amount,
            Email = request.CustomerEmail,
            BillingAddress = new Address
            {
                First_Name = request.CustomerFirstName,
                Last_Name = request.CustomerLastName,
                Street = request.BillingAddress.AddressText,
                City = request.BillingAddress.City,
                State = request.BillingAddress.StateISO2,
                Postalcode = request.BillingAddress.ZipCode,
                Country = request.BillingAddress.CountryISO2
            },
            ShippingAddress = new Address
            {
                First_Name = request.CustomerFirstName,
                Last_Name = request.CustomerLastName,
                Street = request.ShippingAddress.AddressText,
                City = request.ShippingAddress.City,
                State = request.ShippingAddress.StateISO2,
                Postalcode = request.ShippingAddress.ZipCode,
                Country = request.ShippingAddress.CountryISO2
            },
            Clerk = request.Clerk,
            Software = request.Software
        };

        var response = await service.SaleAsync(req);
        
        var paymentResponse = new PaymentResponse()
        {
            Amount = response.Auth_Amount,
            ResponseCode = response.Result_Code,
            ResponseMessage = response.Result,
            TransactionId = response.Refnum,
            TransactionStatus = Enum.Parse<TransactionStatus>(response.Result),
            ErrorMessage = response.Error
        };

        if (response.Savedcard != null)
        {
            paymentResponse.CreditCard = new CustomerDbContext.Entities.CreditCard()
            {
                CreditCardNumber = response.Savedcard.Key
            };
        }

        return paymentResponse;
    }
}