namespace _011Global.Shared.PaymentGateways.USAePay.Requests;

public class CreditCard
{
    public string? Cardholder { get; set; }
    
    public string Number { get; set; } = null!;
    
    public string Expiration { get; set; } = null!;
    
    public string? Cvc { get; set; }
    
    public string? Avs_Street { get; set; }
    
    public string? Avs_PostalCode { get; set; }
}