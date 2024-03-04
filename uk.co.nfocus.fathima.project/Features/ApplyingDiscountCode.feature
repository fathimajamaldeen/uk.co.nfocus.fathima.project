Feature: Applying Discount Code

This feature enables users to apply discount codes during 
the checkout process on the eCommerce website. It ensures 
that users logged into the platform can successfully apply 
a discount code to their cart, resulting in the correct 
deduction applied.

Scenario: Applying discount code to the cart
    Given I am logged in on the shopping website 
    When I add a belt to my cart
        And I view my cart
        And I apply a discount code '<Code>'
    Then I should see the discount of <Percentage>% is applied correctly 
Examples: 
    | Code      | Percentage |
    | edgewords | 15         |
    | nfocus    | 25         |
