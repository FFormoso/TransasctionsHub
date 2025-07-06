using System.ComponentModel.DataAnnotations;

namespace _011Global.RestServiceAPI.DTOs;

public class CreditCardDto
{
    [MaxLength(256)]
    [Required]
    public string CreditCardNumber { get; set; } = null!;
    
    [MaxLength(8)]
    [Required]
    public string LastFourNumbers { get; set; } = null!;
    
    [MaxLength(350)]
    public string CardHolder { get; set; } = null!;
    
    [MaxLength(5)]
    [Required]
    public string ExpirationMonth { get; set; } = null!;
    
    [MaxLength(8)]
    [Required]
    public string ExpirationYear { get; set; } = null!;
}