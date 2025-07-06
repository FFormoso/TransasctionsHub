ALTER TABLE dbo.Global_CreditCards ADD Tokenized bit NOT NULL DEFAULT 0;

ALTER TABLE dbo.Global_CreditCards DROP COLUMN SecurityCode;

ALTER TABLE dbo.Global_Customers ADD IsActive bit NOT NULL DEFAULT 1;