using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _011Global.Shared.CustomerDbContext.Entities;

[Table("Global_Addresses")]
public class Address
{
    [Key]
    public int AddressID { get; set; }
    
    public short CountryISO2 { get; set; }
    
    [MaxLength(150)]
    public string StateISO2 { get; set; } = null!;
    
    [MaxLength(150)]
    public string City { get; set; } = null!;
    
    [MaxLength(25)]
    public string ZipCode { get; set; } = null!;
    
    [Column("Address")]
    [MaxLength(500)]
    public string AddressText { get; set; } = null!;
    
    public DateTime CreationDate { get; set; }
    
    //Navigation properties
    [InverseProperty(nameof(Customer.ShippingAddress))]
    public ICollection<Customer>? ShippingCustomers { get; set; }

    [InverseProperty(nameof(Customer.BillingAddress))]
    public ICollection<Customer>? BillingCustomers { get; set; }
}