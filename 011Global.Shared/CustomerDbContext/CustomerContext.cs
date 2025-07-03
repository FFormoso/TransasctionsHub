using _011Global.Shared.CustomerDbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace _011Global.Shared.CustomerDbContext;

public class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<CreditCard> CreditCards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    //public DbSet<TransactionStatus> TransactionStatuses { get; set; }
}