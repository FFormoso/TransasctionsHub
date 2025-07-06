using System.ComponentModel.DataAnnotations;

namespace _011Global.RestServiceAPI.DTOs;

public class CreditCardDto
{
    [MaxLength(256)]
    [Required]
    public string CreditCardNumber { get; set; } = null!;
    
    [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid LastFourNumbers")]
    [Required]
    public string LastFourNumbers { get; set; } = null!;
    
    [MaxLength(350)]
    public string CardHolder { get; set; } = null!;
    
    [Range(1, 12)]
    [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "Invalid ExpirationMonth format")]
    [Required]
    public string ExpirationMonth { get; set; } = null!;
    
    [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid ExpirationYear format")]
    [Required]
    public string ExpirationYear { get; set; } = null!;
}