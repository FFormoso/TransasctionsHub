# Changelog

- Database project/directory with
  - 01_Schema: Database structure inherit to the application
  - 02_Data: Required data inherit to the application
  - 03_Migrations: Changes from the previous version to the current one
  - 05_Dataset: Testing dataset
- Configuration management with **multiple configuration providers** accessed through `AppSettingsManager` class (alternatively, this can be done with *Options* pattern)
- Managing all the confidential configuration parameters with **User Secrets** in the development environment, and **Azure Key Vault** in production
- Extracted PaymentGateways configuration to `PGWsSettings.json` in the Shared project, and added as a configuration source in each project that needs it
- Added `Serilog` logging provider, using sinks to log to file (Logs > `JobsService.log` and `RestServiceAPI.log`) and console
- Removed `SecurityCode` column from `Customers` table. It is not required to perform payments, and its storage is illegal, according to [PCI-Data Security Standard](https://www.globalpaymentsintegrated.com/en-us/blog/2020/01/14/card-verification-codes-pci-rules-for-data-storage#:~:text=CVV%20data%20is%20not%20necessary%20for%20card%2Don%2Dfile%20transactions%20or%20recurring%20payments%2C%20and%20storage%20of%20this%20data%20is%20prohibited%20by%20the%20PCI%2DData%20Security%20Standard.).
- Added `Tokenized` column to `CreditCards` table
- Perform card numbers tokenization within the payments if they aren't already.
- Added `IsActive` column to `Customers` table
- Added `CustomerDbContext` following the actual design implemented, but using `DataAnnotations` instead of `OnModelCreating`
- Implemented **builder design pattern** for building the payment requests
- Abstracted payment gateways implementation with **adapter design pattern** for the capability of injecting and using multiple payment gateways, and be able to change it without changing any implemented code in the payment flows
- Configurable payment gateway and iteration time for jobs
- Implemented **result design pattern** for standardized API responses
- Implemented `ObjectsMapper` class as a utility in the Shared project
- Implemented RestServiceAPI with a `CustomerController` with two endpoints
  - EnrollCustomer: for adding new customers, using `CustomerDTO` validated through `DataAnnotations` 
  - UnsubscribeCustomer: for an existing active customer, changes customers `IsActive` to `false`
- Simple **docker deployment** with **docker compose orchestration** for the `testing database`, `JobsService` and `RestServiceAPI`
- Authentication with jwt token through `AuthorizationFilter`

## Not implemented yet
- **Concurrency control** for when multiple threads access to the same database row at the same time
- Extracted `MonthlyFee` from `Customers` table to a new `SubscriptionPlans` table for better management of different plans
