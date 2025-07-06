using _011Global.Shared.CustomerDbContext.Entities;

namespace _011Global.Shared.CustomerDbContext.Interfaces;

public interface ICustomerRepository
{
    public IQueryable<Customer> Get();

    public IQueryable<Customer> GetCustomersInDue();

    public Task<Transaction> SaveTransaction(Transaction transaction);

    public Task Insert(Customer customer);

    public Task<int> Unsubscribe(int customerId);
}