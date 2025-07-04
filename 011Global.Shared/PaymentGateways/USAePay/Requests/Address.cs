namespace _011Global.Shared.PaymentGateways.USAePay.Requests;

public class Address
{
    public string First_Name { get; set; } = null!;
    
    public string Last_Name { get; set; } = null!;
    
    public string Street { get; set; } = null!;
    
    public string Street2 { get; set; } = null!;
    
    public string City { get; set; } = null!;
    
    public string State { get; set; } = null!;
    
    public string Postalcode { get; set; } = null!;
    
    public short Country { get; set; }
    
    public string Phone { get; set; } = null!;
    
    public string Fax { get; set; } = null!;
}