create table dbo.Global_Transactions
(
    TransactionID       int identity (206474, 1)
        primary key,
    CustomerID          int                                              not null
        references dbo.Global_Customers,
    Amount              float                                            not null,
    TransactionStatusID tinyint                                          not null
        references dbo.Global_TransactionsStatuses,
    PaymentGWTransID    varchar(50),
    ResponseCode        varchar(10),
    SubErrorDesc1       varchar(300),
    SubErrorDesc2       varchar(300),
    SubErrorDesc3       varchar(300),
    CreationDate        datetime
        constraint DF_Global_Transactions_CreationDate default getdate() not null,
    CreditCardID        int
        references dbo.Global_CreditCards
)
go