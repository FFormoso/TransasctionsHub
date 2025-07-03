create table dbo.Global_Customers
(
    CustomerID        int identity (16474, 1)
        primary key,
    CustomerEmail     varchar(256),
    CustomerFirstName nvarchar(256),
    CustomerLastName  nvarchar(256),
    ShippingAddressID int
        constraint FK_Global_Customers_ShippingAddress
            references dbo.Global_Addresses,
    BillingAddressID  int
        constraint FK_Global_Customers_BillingAddress
            references dbo.Global_Addresses,
    MonthlyFee        money                                           not null,
    CreationDate      datetime
        constraint DF_Global_Customers_CreationDate default getdate() not null
)
go