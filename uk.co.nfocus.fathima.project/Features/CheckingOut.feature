Feature: Checking Out

This feature focuses on the checkout process of 
the eCommerce website, ensuring that users can 
successfully complete their purchases and that 
order information remains consistent throughout 
the process. It verifies that after adding items 
to the cart, proceeding to checkout, providing 
billing details, and placing the order, the 
order number displayed to the user matches 
the one stored in their account orders.

 Scenario: Checking if order numbers are consistent
   Given I am logged in on the shopping website
   When I add a belt to my cart
        And I proceed to checkout
        And I fill in billing details with
        | First Name | Last Name | Address     | City     | Postcode | Phone Number |
        | hello      | example   | 64 zoo lane | Zootopia | SW1A 1AA | 555 564 2211 |
        And I place the order
    Then I should see the same order number in my account orders as the one displayed after placing the order
