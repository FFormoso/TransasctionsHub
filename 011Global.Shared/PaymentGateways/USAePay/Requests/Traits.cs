namespace _011Global.Shared.PaymentGateways.USAePay.Requests;

public class Traits
{
    public bool? Is_Debt { get; set; }
    
    public bool? Is_Bill_Pay { get; set; }
    
    public bool? Is_Recurring { get; set; }
    
    public bool? Is_Healthcare { get; set; }
    
    public bool? Is_Cash_Advance { get; set; }
    
    public int? Secure_Collection { get; set; }
    
    public string? Stored_Credential { get; set; }
}