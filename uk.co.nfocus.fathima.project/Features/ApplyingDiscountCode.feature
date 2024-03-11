Feature: Applying Discount Code

This feature will login to an e-commerce site as a registered user, 
purchase an item of clothing, apply a discount code and verify the 
correct application of the discount codes.

@Test1
Scenario: Applying different discount codes to the cart
    Given I am logged in on the shopping website 
    When I add a 'Beanie' to my cart
        And I view my cart
        And I apply a discount code '<Code>'
    Then I should see the discount of <Percentage>% is applied correctly 
Examples: 
    | Code      | Percentage |
    | edgewords | 15         |
    | nfocus    | 25         |