using _011Global.RestServiceAPI.DTOs;
using _011Global.RestServiceAPI.Mappers;
using _011Global.Shared.CustomerDbContext.Entities;
using _011Global.Shared.CustomerDbContext.Interfaces;
using _011Global.Shared.Filters;
using _011Global.Shared.Patterns;
using _011Global.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _011Global.RestServiceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : Controller
{
    private readonly ILogger _logger;
    private readonly ICustomerRepository _customerRepository;
    
    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    [HttpPost("EnrollCustomer")]
    [AuthorizationFilter]
    public async Task<IActionResult> EnrollCustomer(CustomerDto customerDto)
    {
        try
        {
            var emailUsed = await _customerRepository.Get()
                .AnyAsync(c => c.CustomerEmail == customerDto.CustomerEmail);

            if (emailUsed)
                return BadRequest(Result.Failure("Email already in use"));

            var customer = CustomerMapper.Map(customerDto);
            
            await _customerRepository.Insert(customer);
            return Ok(Result.Success());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enrolling customer");
            return StatusCode(500, Result.Failure($"Error enrolling customer: {ex.Message}"));
        }
    }
    
    [HttpPost("UnsubscribeCustomer")]
    [AuthorizationFilter]
    public async Task<IActionResult> UnsubscribeCustomer(int customerId)
    {
        try
        {
            var rows = await _customerRepository.Unsubscribe(customerId);
            
            return (rows == 0) ? 
                BadRequest(Result.Failure($"There is no active customer with Id {customerId}")) :
                Ok(Result.Success("Customer unsubscribed successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unsubscribing customer");
            return StatusCode(500, Result.Failure($"Error unsubscribing customer: {ex.Message}"));
        }
    }
}