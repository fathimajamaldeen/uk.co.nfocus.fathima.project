﻿using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.fathima.project.Support;
using uk.co.nfocus.fathima.project.Support.POMClasses;

namespace uk.co.nfocus.fathima.project.StepDefinitions
{
    [Binding]
    public class TestStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public TestStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }

        [Given(@"I am logged in on the shopping website")]
        public void GivenIAmLoggedInOnTheShoppingWebsite()
        {
            //Going to login page
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            loginpage.NavigateToLoginPage();
            //Logging into the wbesite
            loginpage.SetUsername(TestContext.Parameters["WebAppUsername"]).SetPassword(TestContext.Parameters["WebAppPassword"]).SubmitForm();
        }

        [When(@"I add a belt to my cart")]
        public void WhenIAddABeltToMyCart()
        {
            //Going to shop page
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.GoShopPage();
            //Adding belt to cart
            ProductPagePOM product = new ProductPagePOM(_driver);
            product.AddBeltToCart();
            Console.WriteLine("Added belt to cart");
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            //Going to view the cart
            CartPOM cart = new CartPOM(_driver);
            cart.ViewCart();
        }

        [When(@"I apply a discount code '(.*)'")]
        public void WhenIApplyADiscountCode(string discountCode)
        {
            //Storing the discount code
            _scenarioContext["DiscountCode"] = discountCode;
            //Applying the discount code set in the test
            CartPOM cart = new CartPOM(_driver);
            cart.ApplyDiscountCode(discountCode);
            Console.WriteLine("Applied discount code");
        }

        [Then(@"I should see the discount of (.*)% is applied correctly")]
        public void ThenIShouldSeeTheDiscountOfIsAppliedCorrectly(int discount)
        {
            //Waiting for the discount to be applied succesfully
            HelperLib myHelper = new HelperLib(_driver); 
            myHelper.WaitForElement(By.LinkText("[Remove]"), 5);
            DiscountDetailsPOM discountDetails = new DiscountDetailsPOM(_driver);

            //Getting the discount code from ScenarioContext
            string discountName = (string)_scenarioContext["DiscountCode"];
            //Checking to see that discount is 15% of total value
            try
            {
                decimal discountDecimal = (decimal) discount / 100m;//Casting 100 to decimal for accurate division
                Assert.That(discountDetails.GetDiscountValue(discountName), Is.EqualTo((discountDetails.GetPreviousTotalValue() * discountDecimal)));
                Console.WriteLine(":) The discount code works");
            }
            catch (AssertionException)
            {
                Console.WriteLine(":( The discount code does not work");
                throw; //Rethrow the exception to ensure its caught by SpecFlow
            }
            //Checking to see if new total value is correctly calculated
            try
            {
                Assert.That(discountDetails.GetNewTotalValue(), Is.EqualTo(discountDetails.GetPreviousTotalValue() - discountDetails.GetDiscountValue(discountName) + discountDetails.GetShippingCostValue()));
                Console.WriteLine(":) The total is correctly calculated");
            }
            catch (AssertionException)
            {
                Console.WriteLine(":( The total is not correctly calculated");
                throw; //Rethrow the exception to ensure its caught by SpecFlow
            }

            //Removes the coupon and item from the cart 
            CartPOM cart = new CartPOM(_driver);
            cart.RemoveCouponCode();
            cart.RemoveItemFromCart();

        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            //Proceeding to checkout
            CartPOM cart = new CartPOM(_driver);
            cart.ProceedToCheckout();
        }

        [When(@"I fill in billing details, to place the order, with")]
        public void WhenIFillInBillingDetailsToPlaceTheOrderWith(Table table)
        {
            //Filling in the billing details with the table details from the test
            BillingDetailsPOM billing = new BillingDetailsPOM(_driver);
            billing.SetFirstName(table.Rows[0]["First Name"]).SetLastName(table.Rows[0]["Last Name"]).SetAddress(table.Rows[0]["Address"]).SetCity(table.Rows[0]["City"]).SetPostcode(table.Rows[0]["Postcode"]).SetPhoneNumber(table.Rows[0]["Phone Number"]);
            billing.PlaceOrder();
        }

        [Then(@"I should see the same order number in my account orders as the one displayed after placing the order")]
        public void ThenIShouldSeeTheSameOrderNumberInMyAccountOrdersAsTheOneDisplayedAfterPlacingTheOrder()
        {
            OrderDetailsPOM orderDetails = new OrderDetailsPOM(_driver);
            //Getting order number from order recieved post ordering item
            int orderNumberValue = orderDetails.GetOrderNumberValue();
            orderDetails.GoToMyOrders();
            //Getting order number from orders in account
            int orderNumberInAccountValue = orderDetails.GetOrderNumberInAccountValue();
            //Check if both values are equal or not and output correct line in console
            try
            {
                Assert.That(orderNumberInAccountValue, Is.EqualTo(orderNumberValue));
                Console.WriteLine(":) The order numbers are the same");
            }
            catch (AssertionException)
            {
                Console.WriteLine(":( The order numbers are not the same");
                throw; //Rethrow the exception to ensure its caught by SpecFlow
            }
        }


    }
}
