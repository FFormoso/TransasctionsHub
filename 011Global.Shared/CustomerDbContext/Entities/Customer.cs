using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _011Global.Shared.CustomerDbContext.Entities;

[Table("Global_Customers")]
public class Customer
{
    [Key]
    public int CustomerID { get; set; }
    
    public bool IsActive { get; set; }
    
    [MaxLength(256)]
    public string CustomerEmail { get; set; } = null!;
    
    [MaxLength(256)]
    public string CustomerFirstName { get; set; } = null!;
    
    [MaxLength(256)]
    public string CustomerLastName { get; set; } = null!;
    
    public int ShippingAddressID { get; set; }
    
    public int BillingAddressID { get; set; }
    
    [Column(TypeName = "money")]
    public decimal MonthlyFee { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    //Navigation properties
    [ForeignKey("ShippingAddressID")]
    public Address ShippingAddress { get; set; } = null!;
    
    [ForeignKey("BillingAddressID")]
    public Address BillingAddress { get; set; } = null!;
    
    public ICollection<CreditCard> CreditCards { get; set; } = null!;
    
    public ICollection<Transaction>? Transactions { get; set; }
}