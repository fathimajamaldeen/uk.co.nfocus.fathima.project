Feature: Checking Out

@Test2
 Scenario: Checking if order numbers are consistent
    This feature will login to an e-commerce site 
    as a registered user, purchase an item of clothing and go 
    through checkout. It will capture the order number and 
    check the order is also present in the ‘My 
    Orders’ section of the site.
   Given I am logged in on the shopping website
   When I add a 'Belt' to my cart
        And I proceed to checkout
        And I fill in billing details, to place the order, with
            | Field        | Value             |
            | First Name   | Hello             |
            | Last Name    | Example           |
            | Address      | 64 Zoo Lane       |
            | City         | Zootopia          |
            | Postcode     | SW1 1AA           |
            | Phone Number | 555 564 2211      |
            | Email        | hello@example.com |
    Then I see an order number
        And that order number is same in order history
