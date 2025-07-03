using _011Global.Shared.CustomerDbContext.Entities;
using _011Global.Shared.CustomerDbContext.Interfaces;

namespace _011Global.Shared.CustomerDbContext.Repos;

public class CustomerRepository(CustomerContext context) : ICustomerRepository
{
    public IQueryable<Customer> Get()
    {
        return context.Customers.AsQueryable();
    }
}