using System.ComponentModel.DataAnnotations;

namespace _011Global.RestServiceAPI.DTOs;

public class AddressDto
{
    [Required]
    public short CountryISO2 { get; set; }
    
    [MaxLength(150)]
    [Required]
    public string StateISO2 { get; set; } = null!;
    
    [MaxLength(150)]
    [Required]
    public string City { get; set; } = null!;
    
    [MaxLength(25)]
    [Required]
    public string ZipCode { get; set; } = null!;
    
    [MaxLength(500)]
    [Required]
    public string AddressText { get; set; } = null!;
}