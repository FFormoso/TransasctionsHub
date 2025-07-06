using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _011Global.Shared.CustomerDbContext.Enums;

namespace _011Global.Shared.CustomerDbContext.Entities;

[Table("Global_Transactions")]
public class Transaction
{
    [Key]
    public int TransactionID { get; set; }
    
    public int CustomerID { get; set; }
    
    public double Amount { get; set; }
    
    public TransactionStatus TransactionStatusID { get; set; }
    
    [MaxLength(50)]
    public string PaymentGWTransID { get; set; } = null!;
    
    [MaxLength(10)]
    public string? ResponseCode { get; set; }
    
    [MaxLength(300)]
    public string? SubErrorDesc1 { get; set; }
    
    [MaxLength(300)]
    public string? SubErrorDesc2 { get; set; }
    
    [MaxLength(300)]
    public string? SubErrorDesc3 { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreationDate { get; set; }
    
    public int CreditCardID { get; set; }
    
    //Navigation properties
    [ForeignKey("CustomerID")]
    public Customer Customer { get; set; } = null!;
    
    [ForeignKey("CreditCardID")]
    public CreditCard? CreditCard { get; set; }
}