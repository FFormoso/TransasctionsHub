namespace _011Global.Shared.PaymentGateways.USAePay.Responses;

public class CreditCard
{
    public string Number { get; set; } = null!;
    
    public string Category_Code { get; set; } = null!;
    
    public string Entry_Mode { get; set; } = null!;
}