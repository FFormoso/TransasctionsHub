using _011Global.Shared.CustomerDbContext.Entities;

namespace _011Global.Shared.CustomerDbContext.Interfaces;

public interface ICustomerRepository
{
    public IQueryable<Customer> Get();
}