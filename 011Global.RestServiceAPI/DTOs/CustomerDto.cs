using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _011Global.RestServiceAPI.DTOs;

public class CustomerDto
{
    [MaxLength(256)]
    [Required]
    public string CustomerEmail { get; set; } = null!;
    
    [MaxLength(256)]
    [Required]
    public string CustomerFirstName { get; set; } = null!;
    
    [MaxLength(256)]
    [Required]
    public string CustomerLastName { get; set; } = null!;
    
    [Column(TypeName = "money")]
    [Required]
    public decimal MonthlyFee { get; set; }
    
    [Required]
    public AddressDto ShippingAddress { get; set; } = null!;
    
    [Required]
    public AddressDto BillingAddress { get; set; } = null!;
    
    [Required]
    public ICollection<CreditCardDto> CreditCards { get; set; } = null!;
}