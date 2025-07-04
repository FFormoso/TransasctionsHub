namespace _011Global.Shared.PaymentGateways.USAePay.Responses;

public class Batch
{
    public string Type { get; set; } = null!;
    
    public string Key { get; set; } = null!;
    
    public int BatchRefNum { get; set; }
    
    public string Sequence { get; set; } = null!;
}