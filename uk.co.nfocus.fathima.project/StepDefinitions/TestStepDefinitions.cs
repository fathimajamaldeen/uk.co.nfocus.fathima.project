using AventStack.ExtentReports;
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

        //Common to Test1 and Test2
        [Given(@"I am logged in on the shopping website")]
        public void GivenIAmLoggedInOnTheShoppingWebsite()
        {
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            //Going to login page
            loginpage.NavigateToLoginPage();
            //Logging into the website with error checking
            string username = TestContext.Parameters["WebAppUsername"];
            string password = TestContext.Parameters["WebAppPassword"];
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                loginpage.SetUsername(username).SetPassword(password).SubmitForm();

            }
            else
            {
                Assert.Fail("There is no valid username or password therefore Test cannot continue");
            }
        }

        [When(@"I add a '(.*)' to my cart")]
        public void WhenIAddAToMyCart(string itemName)
        {
            //Going to shop page
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.GoShopPage();
            //Adding belt to cart
            ProductPagePOM product = new ProductPagePOM(_driver, itemName);
            product.AddItemToCart();
            Console.WriteLine($"Added {itemName} to cart");
        }

        //Test1
        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            //Going to view the cart
            CartPOM cart = new CartPOM(_driver);
            cart.ViewCart();
        }

        //Test1
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

        //Test1
        [Then(@"I should see the discount of (.*)% is applied correctly")]
        public void ThenIShouldSeeTheDiscountOfIsAppliedCorrectly(int discount)
        {
            
            try
            {
                DiscountDetailsPOM discountDetails = new DiscountDetailsPOM(_driver);
                //Getting discount code from ScenarioContext
                string discountName = (string)_scenarioContext["DiscountCode"];
                //Checking to see if the discount is the correct percentage
                decimal discountDecimal = (decimal)discount / 100m;
                decimal expectedDiscountValue = discountDetails.GetPreviousTotalValue() * discountDecimal;
                Assert.That(discountDetails.GetDiscountValue(discountName), Is.EqualTo(expectedDiscountValue), $"Expected discount of {discount}% is not applied correctly");
                //Check to see if new total value is correctly calculated
                decimal expectedNewTotalValue = discountDetails.GetPreviousTotalValue() - expectedDiscountValue + discountDetails.GetShippingCostValue();
                Assert.That(discountDetails.GetNewTotalValue(), Is.EqualTo(expectedNewTotalValue), $"The total is not correctly calculated");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($":( {ex.Message}");
                throw; // Rethrow the exception to ensure it's caught by SpecFlow
            }
        }

        //Test2
        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            //Proceeding to checkout
            CartPOM cart = new CartPOM(_driver);
            cart.ProceedToCheckout();
        }

        //Test2
        [When(@"I fill in billing details, to place the order, with")]
        public void WhenIFillInBillingDetailsToPlaceTheOrderWith(Table table)
        {
            //Filling in the billing details with the table details from the test
            BillingDetailsPOM billing = new BillingDetailsPOM(_driver);
            billing.SetFirstName(table.Rows[0]["First Name"])
                .SetLastName(table.Rows[0]["Last Name"])
                .SetAddress(table.Rows[0]["Address"])
                .SetCity(table.Rows[0]["City"])
                .SetPostcode(table.Rows[0]["Postcode"])
                .SetPhoneNumber(table.Rows[0]["Phone Number"]);
            billing.PlaceOrder();
        }

        //Test2
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
                Assert.That(orderNumberInAccountValue, Is.EqualTo(orderNumberValue), $"The order values are not the same");
            }
            catch (AssertionException)
            {
                throw; //Rethrow the exception to ensure its caught by SpecFlow
            }
        }


    }
}
