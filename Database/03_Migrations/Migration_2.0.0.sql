ALTER TABLE dbo.Global_CreditCards ADD Tokenized bit NOT NULL DEFAULT false;

ALTER TABLE dbo.Global_CreditCards DROP COLUMN SecurityCode;