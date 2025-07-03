IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TransactionsHub3')
BEGIN
  CREATE DATABASE [TransactionsHub3]
END;