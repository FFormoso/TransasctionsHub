create table dbo.Global_Addresses
(
    AddressID    int identity (26474, 1)
        primary key,
    CountryISO2  smallint                                             not null,
    StateISO2    nvarchar(150),
    City         nvarchar(150),
    ZipCode      varchar(25)                                          not null,
    Address      nvarchar(500)                                        not null,
    CreationDate datetime
        constraint DF_Global_Addresses_CreationDate default getdate() not null
)
go
