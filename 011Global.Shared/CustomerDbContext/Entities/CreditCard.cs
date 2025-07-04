using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _011Global.Shared.CustomerDbContext.Entities;

[Table("Global_CreditCards")]
public class CreditCard
{
    [Key]
    public int CreditCardID { get; set; }
    
    public int CustomerID { get; set; }
    
    [MaxLength(256)]
    public string CreditCardNumber { get; set; } = null!;
    
    [MaxLength(8)]
    public string LastFourNumbers { get; set; } = null!;
    
    [MaxLength(350)]
    public string? CardHolder { get; set; }
    
    [MaxLength(5)]
    public string ExpirationMonth { get; set; } = null!;
    
    [MaxLength(8)]
    public string ExpirationYear { get; set; } = null!;
    
    public DateTime? CreationDate { get; set; }
    
    public bool Tokenized { get; set; }
    
    //Navigation properties
    [ForeignKey("CustomerID")]
    public Customer Customer { get; set; } = null!;
    
    public ICollection<Transaction> Transactions { get; set; } = null!;
}