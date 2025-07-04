namespace _011Global.Shared.PaymentGateways.USAePay.Requests;

public class LineItem
{
    public string ProductKey { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Cost { get; set; } = default!;
    public string Qty { get; set; } = default!;
    public string TaxAmount { get; set; } = default!;
    public string LocationKey { get; set; } = default!;
    public string ListPrice { get; set; } = default!;
}