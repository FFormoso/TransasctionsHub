using _011Global.Shared.CustomerDbContext.Entities;

namespace _011Global.Shared.PaymentGateways.DTOs;

public class PaymentRequest
{
    public decimal Amount { get; set; }
    
    public bool PartialAuth { get; set; }
    
    public PaymentType PaymentType { get; set; }
    
    public CreditCard CreditCard { get; set; } = null!;
    
    public bool CardTokenization { get; set; }
    
    public string CustomerEmail { get; set; } = null!;
    
    public Address BillingAddress { get; set; } = null!;
    
    public Address ShippingAddress { get; set; } = null!;
    
    public string CustomerFirstName { get; set; } = null!;
    
    public string CustomerLastName { get; set; } = null!;
    
    public string Clerk { get; set; } = null!;
    
    public string Software { get; set; } = null!;
}