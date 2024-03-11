﻿Feature: Checking Out

This feature will login to an e-commerce site 
as a registered user, purchase an item of clothing and go 
through checkout. It will capture the order number and 
check the order is also present in the ‘My 
Orders’ section of the site.

@Test2
 Scenario: Checking if order numbers are consistent
   Given I am logged in on the shopping website
   When I add a 'Belt' to my cart
        And I proceed to checkout
        And I fill in billing details, to place the order, with
        | Field        | Value        |
        | First Name   | hello        |
        | Last Name    | example      |
        | Address      | 64 Zoo Lane  |
        | City         | Zootopia     |
        | Postcode     | SW1A 1AA     |
        | Phone Number | 555 564 2211 |
    Then I should see the same order number in my account orders as the one displayed after placing the order
