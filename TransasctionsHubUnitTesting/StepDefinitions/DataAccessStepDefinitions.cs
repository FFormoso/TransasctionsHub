using _011Global.Shared.CustomerDbContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using Reqnroll;

namespace TransasctionsHubUnitTesting.StepDefinitions;

[Binding]
[Scope(Feature = "EnrollAndUnsubscribeCustomers")]
public class DataAccessStepDefinitions
{
    private readonly ICustomerRepository _customerRepository;
    
    public DataAccessStepDefinitions(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    [Given("The database does not contains a customer with email {string}")]
    public async Task GivenTheDatabaseDoesNotContainsACustomerWithEmail(string email)
    {
        var customer = await _customerRepository.Get().SingleOrDefaultAsync(c => c.CustomerEmail == email);

        Assert.Null(customer);
    }

    [Then("The database contains a customer with email {string}")]
    public async Task ThenTheDatabaseContainsACustomerWithEmail(string email)
    {
        var customer = await _customerRepository.Get().SingleOrDefaultAsync(c => c.CustomerEmail == email);

        Assert.Equal(email, customer?.CustomerEmail);
    }

    [Then("A transaction is eventually created for the customer with email {string} within {int} seconds")]
    public async Task ThenATransactionIsEventuallyCreatedForTheCustomerWithEmailWithinSeconds(string email, int timeOut)
    {
        const int pollIntervalInMilliseconds = 1000;
        int elapsed = 0;
        bool found = false;

        while (elapsed < timeOut * 1000)
        {
            var customer = await _customerRepository.Get()
                .Include(c => c.Transactions)
                .SingleOrDefaultAsync(c => c.CustomerEmail == email);
            
            if (customer?.Transactions.Count > 0)
            {
                found = true;
                break;
            }

            await Task.Delay(pollIntervalInMilliseconds);
            elapsed += pollIntervalInMilliseconds;
        }

        Assert.True(found, $"Expected a transaction for customer '{email}' within {timeOut} seconds, but none was found.");
    }

    [Given("The database contains an active customer with customerId {int}")]
    public async Task GivenTheDatabaseContainsAnActiveCustomerWithCustomerId(int customerId)
    {
        var customer = await _customerRepository.Get().SingleOrDefaultAsync(c => c.CustomerID == customerId && c.IsActive);

        Assert.Equal(customerId, customer?.CustomerID);
    }

    [Then("The database contains an inactive customer with customerId {int}")]
    public async Task ThenTheDatabaseContainsAnInactiveCustomerWithCustomerId(int customerId)
    {
        var customer = await _customerRepository.Get().SingleOrDefaultAsync(c => c.CustomerID == customerId && !c.IsActive);

        Assert.Equal(customerId, customer?.CustomerID);
    }
}