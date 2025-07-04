namespace _011Global.Shared.PaymentGateways.USAePay.Responses;

public class SavedCard
{
    public string Key { get; set; } = null!;
    
    public string Cardnumber { get; set; } = null!;
    
    public string Type { get; set; } = null!;
    
    public string Expiration { get; set; } = null!;
}