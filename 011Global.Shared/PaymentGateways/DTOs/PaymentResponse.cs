using _011Global.Shared.CustomerDbContext.Entities;
using _011Global.Shared.CustomerDbContext.Enums;

namespace _011Global.Shared.PaymentGateways.DTOs;

public class PaymentResponse
{
    public string ResponseCode { get; set; } = null!;
    
    public string ResponseMessage { get; set; } = null!;
    
    public string TransactionId { get; set; } = null!;
    
    public double Amount { get; set; }
    
    public TransactionStatus TransactionStatus { get; set; }
    
    public string? ErrorMessage { get; set; }
    
    public CreditCard? CreditCard { get; set; }
}