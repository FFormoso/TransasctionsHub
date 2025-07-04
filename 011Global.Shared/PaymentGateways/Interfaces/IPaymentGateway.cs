using _011Global.Shared.PaymentGateways.DTOs;

namespace _011Global.Shared.PaymentGateways.Interfaces;

public interface IPaymentGateway
{
    public Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request);
}