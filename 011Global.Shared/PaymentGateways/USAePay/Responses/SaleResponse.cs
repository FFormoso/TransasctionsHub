namespace _011Global.Shared.PaymentGateways.USAePay.Responses;

public class SaleResponse
{
    public string Type { get; set; } = null!;
    
    public string Key { get; set; } = null!;
    
    public string Refnum { get; set; } = null!;
    
    public string Is_Duplicate { get; set; }
    
    public string Result_Code { get; set; } = null!;
    
    public string Result { get; set; } = null!;
    
    public string Authcode { get; set; } = null!;
    
    public CreditCard CreditCard { get; set; }
    
    public SavedCard Savedcard { get; set; }
    
    public string Invoice { get; set; } = null!;
    
    public Avs Avs { get; set; }
    
    public Cvc Cvc { get; set; }
    
    public Batch Batch { get; set; }
    
    public double Auth_Amount { get; set; }
    
    public string Trantype { get; set; } = null!;
    
    public Receipts Receipts { get; set; } = null!;
    
    public string Error { get; set; } = null!;
    
    public string Error_Code { get; set; } = null!;
}