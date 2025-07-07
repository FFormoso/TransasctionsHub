using _011Global.RestServiceAPI.DTOs;

namespace TransasctionsHubUnitTesting.Fixtures;

public static class CustomerFixture
{
    public static CustomerDto CreateValidCustomer()
    {
        return new CustomerDto
        {
            CustomerEmail = "placeholder@example.com",
            CustomerFirstName = "Jhon",
            CustomerLastName = "Doe",
            MonthlyFee = 99.99m,
            ShippingAddress = new AddressDto
            {
                CountryISO2 = 1,
                StateISO2 = "CA",
                City = "City1",
                ZipCode = "1001",
                AddressText = "1234 Street1"
            },
            BillingAddress = new AddressDto
            {
                CountryISO2 = 1,
                StateISO2 = "CA",
                City = "City1",
                ZipCode = "1001",
                AddressText = "1234 Street1"
            },
            CreditCards = new List<CreditCardDto>
            {
                new() {
                    CreditCardNumber = "4000101411112228",
                    LastFourNumbers = "2225",
                    CardHolder = "Jhon Doe",
                    ExpirationMonth = "06",
                    ExpirationYear = "2030"
                }
            }
        };
    }
}