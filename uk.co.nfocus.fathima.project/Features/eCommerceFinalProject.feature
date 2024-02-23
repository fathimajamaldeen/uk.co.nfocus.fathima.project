Feature: eCommerceFinalProject

A short summary of the feature

@tag1
Scenario: Applying discount code to the cart
    Given I am logged in on the shopping website
    When I add a belt to my cart
        And I view my cart
        And I apply a discount code 'edgewords'
    Then I should see the discount applied correctly
