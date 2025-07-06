using _011Global.RestServiceAPI.DTOs;
using _011Global.Shared.CustomerDbContext.Entities;
using _011Global.Shared.Utils;

namespace _011Global.RestServiceAPI.Mappers;

public static class CustomerMapper
{
    public static Customer Map(CustomerDto customerDto)
    {
        Customer customer = ObjectsMapper.Map<CustomerDto, Customer>(customerDto);
        
        customer.BillingAddress = ObjectsMapper.Map<AddressDto, Address>(customerDto.BillingAddress);
        customer.BillingAddress.CreationDate = DateTime.Now;
        
        customer.ShippingAddress = ObjectsMapper.Map<AddressDto, Address>(customerDto.ShippingAddress);
        customer.ShippingAddress.CreationDate = DateTime.Now;

        customer.CreditCards = customerDto.CreditCards.Select(c =>
        {
            var creditCard = ObjectsMapper.Map<CreditCardDto, CreditCard>(c);
            creditCard.CreationDate = DateTime.Now;
            creditCard.Tokenized = false;
            return creditCard;
        }).ToList();
        
        customer.CreationDate = DateTime.Now;
        customer.IsActive = true;
        
        return customer;
    }
}