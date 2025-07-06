using _011Global.Shared.CustomerDbContext.Entities;
using _011Global.Shared.PaymentGateways.DTOs;

namespace _011Global.Shared.CustomerDbContext.Interfaces;

public interface ICustomerRepository
{
    public IQueryable<Customer> Get();

    public IQueryable<Customer> GetCustomersInDue();

    public Task<Transaction> SaveTransaction(Transaction transaction);

    public Task<Transaction> SaveTransaction(PaymentResponse paymentResponse, CreditCard creditCard, int customerId);
    
    public Task<Transaction> SaveTransaction(PaymentResponse paymentResponse, int customerId);

    public Task Insert(Customer customer);

    public Task<int> Unsubscribe(int customerId);
}