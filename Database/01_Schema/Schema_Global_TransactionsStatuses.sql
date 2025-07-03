create table dbo.Global_TransactionsStatuses
(
    TransactionStatusID tinyint      not null
        primary key,
    TransactionStatus   varchar(256) not null
)
go