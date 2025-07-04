using System.Text.Json.Serialization;

namespace _011Global.Shared.PaymentGateways.USAePay.Requests;

public class SaleRequest
{
    public string Command { get; set; } = null!;
    
    public string? Invoice { get; set; }
    
    public string? Ponum { get; set; }
    
    public string? OrderId { get; set; }
    
    public string? Description { get; set; }
    
    public string? Comments { get; set; }
    
    public string? Email { get; set; }
    
    public bool? Send_Receipt { get; set; }
    
    public string? Merchemailaddr { get; set; }
    
    public decimal Amount { get; set; }
    
    public AmountDetail? Amount_Detail { get; set; }
    
    public CreditCard Creditcard { get; set; }
    
    public bool? Save_Card { get; set; }
    
    public Traits? Traits { get; set; }
    
    public string? Custkey { get; set; }
    
    public bool? Save_Customer { get; set; }
    
    public bool? Save_Customer_Paymethod { get; set; }
    
    public Address? BillingAddress { get; set; }
    
    public Address? ShippingAddress { get; set; }
    
    public List<LineItem>? Lineitems { get; set; }
    
    public Dictionary<string, string>? CustomFields { get; set; }
    
    public string? Currency { get; set; }
    
    public string? Terminal { get; set; }
    
    public string? Clerk { get; set; }
    
    public string? ClientTip { get; set; }
    
    public string? Software { get; set; }
    
    [JsonPropertyName("receipt-custemail")]
    public string? Receipt_Custemail { get; set; }
    
    [JsonPropertyName("receipt-merchemail")]
    public string? Receipt_Merchemail { get; set; }
    
    public bool? Ignore_Duplicate { get; set; }
}