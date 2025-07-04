namespace _011Global.Shared.PaymentGateways.USAePay.Requests;

public class AmountDetail
{
    public double? Subtotal { get; set; }
    
    public double? Tax { get; set; }
    
    public bool? Nontaxable { get; set; }
    
    public double? Tip { get; set; }
    
    public double? Discount { get; set; }
    
    public double? Shipping { get; set; }
    
    public double? Duty { get; set; }
    
    public bool? Enable_Partialauth { get; set; }
}