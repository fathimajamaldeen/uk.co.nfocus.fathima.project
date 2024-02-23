Feature: eCommerceFinalProject

A short summary of the feature

@tag1
Scenario: Applying discount code to the cart
    Given I am logged in on the shopping website
    When I add a belt to my cart
        And I view my cart
        And I apply a discount code 'edgewords'
    Then I should see the discount applied correctly

@tag1
 Scenario: Checking if order numbers are consistent
    Given I am logged in on the shopping website
    When I add a belt to my cart
    And I proceed to checkout
    And I fill in billing details with
      | First Name | Last Name | Address     | City     | Postcode | Phone Number |
      | hello      | example   | 64 zoo lane | Zootopia | SW1A 1AA | 555 564 2211 |
    And I place the order
    Then I should see the same order number in my account orders as the one displayed after placing the order
