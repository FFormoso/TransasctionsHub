create table dbo.Global_CreditCards
(
    CreditCardID     int identity (36474, 1)
        primary key,
    CustomerID       int                                                not null
        constraint FK_Global_CreditCards_Customers
            references dbo.Global_Customers,
    CreditCardNumber nvarchar(256)                                      not null,
    LastFourNumbers  varchar(8)                                         not null,
    CardHolder       nvarchar(350),
    ExpirationMonth  varchar(5)                                         not null,
    ExpirationYear   varchar(8)                                         not null,
    CreationDate     datetime
        constraint DF_Global_CreditCards_CreationDate default getdate() not null,
    Tokenized        bit                                                not null
)
go