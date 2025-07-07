# Transactions Hub 3
Technical test for 011Global

## Installation
### Cloning the repository
Clone the repository to your local machine or download the source code as a ZIP file.
```bash
git clone https://github.com/FFormoso/TransasctionsHub.git
```

### Setting the configuration
Rename the `.env.example` file located under `/Deployment/Development/` to `.env`.
```bash
mv ./Deployment/Development/.env.example ./Deployment/Development/.env
```
Set appropriate values for each environment variable in the `.env` file as required.

If you plan to deploy directly from the IDE, you must configure `User Secrets` for both projects.  
In JetBrains Rider: **Right-click on a project → Tools → .NET User Secrets.**

#### Example user secrets

**JobService**
```json
{
  "ConnectionStrings": {
    "TransactionsHubDB": "Server=localhost,1433; Database=TransactionsHub3; User Id=sa; Password=Securitymssqlpas5!; TrustServerCertificate=True;"
  },
  "USAePay": {
    "Authentication": {
      "ApiSeed": "youapiseed",
      "ApiKey": "youapikey",
      "ApiPin": "youapipin"
    }
  }
}
```

**RestServiceAPI**
```json
{
  "ConnectionStrings": {
    "TransactionsHubDB": "Server=localhost,1433; Database=TransactionsHub3; User Id=sa; Password=Securitymssqlpas5!; TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "AccessToken": {
      "SecretKey": "qweqweqweqweqweqweqweqweqweqweqweqwe"
    }
  }
}
```

### Deployment
#### Deploy with Docker Compose
Navigate to the `/Deployment/Development/` directory and deploy using `deploy.sh` or the `docker-compose` command. Alternatively, you can use any IDE that supports Docker, such as Visual Studio or JetBrains Rider.
```bash
cd ./Deployment/Development/
```

To deploy all components:
```bash
./deploy.sh TransactionsHub
```
To deploy only the database and run the applications manually (ensure user secrets are configured beforehand):
```bash
./deploy.sh TransactionsHubDatabase
```

## Usage

### JobsService
A background worker service designed to process payments for all active customers in due.

### RestServiceAPI
You will find a Postman Collection JSON file (`TransactionsHub3.postman_collection.json`) included with API endpoint examples for your convenience.

> [!NOTE]
> The API endpoints require an `Authorization` header with a valid JWT token. With the default secret key, you can use the following:
> 
> `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImV4cCI6MTg1MTgxNDk0OH0.Pwc3WvTbVTq5R7Up2DuSr0k0L2dSXzi4tT0Jl_1UA7E`
> 
> The Postman Collection already includes this token in its headers.

#### Example Endpoints

**EnrollCustomer**: Add a new customer.
```bash
curl --location 'http://localhost:5142/Customers/EnrollCustomer' \
--header 'Authorization: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImV4cCI6MTg1MTgxNDk0OH0.Pwc3WvTbVTq5R7Up2DuSr0k0L2dSXzi4tT0Jl_1UA7E' \
--header 'Content-Type: application/json' \
--data-raw '{
        "customerEmail": "JohnDoe@example.com",
        "customerFirstName": "John",
        "customerLastName": "Doe",
        "monthlyFee": 99.9900,
        "shippingAddress": {
            "countryISO2": 1,
            "stateISO2": "CA",
            "city": "City1",
            "zipCode": "1001",
            "addressText": "1234 Street1"
        },
        "billingAddress": {
            "countryISO2": 1,
            "stateISO2": "CA",
            "city": "City1",
            "zipCode": "1001",
            "addressText": "1234 Street1"
        },
        "creditCards": [
            {
                "creditCardNumber": "4000101411112228",
                "lastFourNumbers": "2225",
                "cardHolder": "John Doe",
                "expirationMonth": "06",
                "expirationYear": "2030"
            }
        ]
    }'
```

**UnsubscribeCustomer**: Unsubscribe an existing customer.
```bash
curl --location --request PATCH 'http://localhost:5142/Customers/UnsubscribeCustomer?customerId=16505' \
--header 'Authorization: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImV4cCI6MTg1MTgxNDk0OH0.Pwc3WvTbVTq5R7Up2DuSr0k0L2dSXzi4tT0Jl_1UA7E'
```

**DebugConfig** (Development Only): Retrieve all environment configuration parameters.
```bash
curl --location 'http://localhost:5142/DebugConfig'
```

## Running Tests

To run the tests, you need to configure the `User Secrets` for the test project.

Example:
```json
{
  "ConnectionStrings": {
    "TransactionsHubDB": "Server=localhost,1433; Database=TransactionsHub3; User Id=sa; Password=Securitymssqlpas5!; TrustServerCertificate=True;"
  },
  "RestServiceAPI": {
    "TestingAuthorizationToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImV4cCI6MTg1MTgxNDk0OH0.Pwc3WvTbVTq5R7Up2DuSr0k0L2dSXzi4tT0Jl_1UA7E"
  }
}
```

Be sure that the configuration file `appsettings.test.json` contains the correct parameters:
```json
{
    "ConnectionStrings": {
        "TransactionsHubDB": "<from-user-secrets>"
    },
    "RestServiceAPI": {
        "BaseUrl": "http://localhost:5142/",
        "TestingAuthorizationToken": "<from-user-secrets>",
        "Endpoints": {
            "EnrollCustomer": "Customers/EnrollCustomer",
            "UnsubscribeCustomer": "Customers/UnsubscribeCustomer"
        }
    }
}
```

> [!IMPORTANT]
> Make sure to run the tests with a fresh dataset, and keep in mind that the test project does **NOT** reset the dataset on every run

## Changelog

- Database project/directory with
  - 01_Schema: Database structure inherent to the application
  - 02_Data: Required data inherent to the application
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
  - DebugConfig: Endpoint for getting all the configuration parameters. Only available in development environment
- Authentication with jwt token through `AuthorizationFilter`
- Simple **docker deployment** with **docker compose orchestration** for the `testing database`, `JobsService` and `RestServiceAPI`
- Testing project with `Reqnroll`

### Not implemented yet
- **Concurrency control** for when multiple threads access to the same database row at the same time
- Extracted `MonthlyFee` from `Customers` table to a new `SubscriptionPlans` table for better management of different plans
- Isolated test environment. Automated deployment of a fresh containerized environment per test run.

## Project Status
There are still some things I want to continue implementing, but I think it's enough as it is.