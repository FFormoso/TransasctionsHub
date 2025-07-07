using System.Net.Http.Headers;
using System.Net.Http.Json;
using _011Global.Shared.Patterns;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using TransasctionsHubUnitTesting.Fixtures;

namespace TransasctionsHubUnitTesting.StepDefinitions;

[Binding]
[Scope(Feature = "EnrollAndUnsubscribeCustomers")]
public class ServiceQueryAPIStepDefinitions
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;
    
    public ServiceQueryAPIStepDefinitions(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_config["RestServiceAPI:BaseUrl"]);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_config["RestServiceAPI:TestingAuthorizationToken"]);
    }
    
    [When("A POST request is done in order to add a new customer with email {string}")]
    public async Task WhenApostRequestIsDoneInOrderToAddANewCustomerWithEmail(string email)
    {
        var customer = CustomerFixture.CreateValidCustomer();
        customer.CustomerEmail = email;
        
        var response = await _httpClient.PostAsJsonAsync(_config["RestServiceAPI:Endpoints:EnrollCustomer"] , customer);

        response.EnsureSuccessStatusCode();
    }

    [When("A PATCH request is done in order to unsubscribe a customer with customerId {int}")]
    public async Task WhenApatchRequestIsDoneInOrderToUnsubscribeACustomerWithCustomerId(int customerId)
    {
        var response = await _httpClient.PatchAsync(_config["RestServiceAPI:Endpoints:UnsubscribeCustomer"] + $"?customerId={customerId}", null);
        
        response.EnsureSuccessStatusCode();
    }
}