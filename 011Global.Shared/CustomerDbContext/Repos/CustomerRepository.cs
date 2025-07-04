using _011Global.Shared.CustomerDbContext.Entities;
using _011Global.Shared.CustomerDbContext.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _011Global.Shared.CustomerDbContext.Repos;

public class CustomerRepository(CustomerContext context) : ICustomerRepository
{
    public IQueryable<Customer> Get()
    {
        return context.Customers.AsQueryable();
    }
    
    /// <summary>
    /// Get all customers whose last transaction was more than 30 days ago (regardless of the transaction status)
    /// </summary>
    /// <returns>IQueryable of customers already including CreditCards and Addresses</returns>
    public IQueryable<Customer> GetCustomersInDue()
    {
        return context.Customers
            .Where(c => c.Transactions
                .Any(t => t.CreationDate < DateTime.Now.AddDays(-30)))
            .Include(c => c.CreditCards)
            .Include(c => c.BillingAddress)
            .Include(c => c.ShippingAddress);
    }
    
    public async Task<Transaction> SaveTransaction(Transaction transaction)
    {
        await context.AddAsync(transaction);
        await context.SaveChangesAsync();
        return transaction;
    }
}