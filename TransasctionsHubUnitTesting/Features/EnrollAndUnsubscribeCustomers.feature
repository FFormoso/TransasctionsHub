Feature: EnrollAndUnsubscribeCustomers

  Scenario Outline: Enroll new customer
    Given The database does not contains a customer with email "<Email>"
    When A POST request is done in order to add a new customer with email "<Email>"
    Then The database contains a customer with email "<Email>"
    And A transaction is eventually created for the customer with email "<Email>" within 30 seconds
    Examples:
      | Email               |
      | jhondoe@example.com |
      
  Scenario Outline: Unsubscribe customer
      Given The database contains an active customer with customerId <CustomerId>
      When A PATCH request is done in order to unsubscribe a customer with customerId <CustomerId>
      Then The database contains an inactive customer with customerId <CustomerId>
      Examples:
        | CustomerId |
        | 16474      |