using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.fathima.project.Support;
using uk.co.nfocus.fathima.project.Support.POMClasses;

namespace uk.co.nfocus.fathima.project.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
        }
        [Given(@"I am logged in on the shopping website")]
        public void GivenIAmLoggedInOnTheShoppingWebsite()
        {
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            loginpage.NavigateToLoginPage();
            loginpage.SetUsername("hello@example.com").SetPassword("Helloworld123!").SubmitForm();
        }

        [When(@"I add a belt to my cart")]
        public void WhenIAddABeltToMyCart()
        {
            //Going to shop
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.Shop.Click();
            //Adding belt to cart
            _driver.FindElement(By.CssSelector("#main > ul > li.product.type-product.post-28.status-publish.instock.product_cat-accessories.has-post-thumbnail.sale.shipping-taxable.purchasable.product-type-simple > a.button.product_type_simple.add_to_cart_button.ajax_add_to_cart")).Click();
            Console.WriteLine("Added belt to cart");
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            CartPOM cart = new CartPOM(_driver);
            cart.ViewCart();
        }

        [When(@"I apply a discount code '(.*)'")]
        public void WhenIApplyADiscountCode(string discountCode)
        { 
            CartPOM cart = new CartPOM(_driver);
            cart.ApplyDiscountCode(discountCode);
            Console.WriteLine("Applied discount code");
        }

        [Then(@"I should see the discount applied correctly")]
        public void ThenIShouldSeeTheDiscountAppliedCorrectly()
        {
            HelperLib myHelper = new HelperLib(_driver); //Instantiate HelperLib class and pass the driver to the constructor
            //Waiting for the details with pop up to come to avoid stale elements
            myHelper.WaitForElement(By.LinkText("[Remove]"), 5);
            DiscountDetailsPOM discountDetails = new DiscountDetailsPOM(_driver);
            //Checking to see that discount is 15% of total value
            try
            {
                Assert.That(discountDetails.GetDiscountValue(), Is.EqualTo(discountDetails.GetPreviousTotalValue() * 0.15m));
                Console.WriteLine(":) The discount code works");
            }
            catch (AssertionException)
            {
                Console.WriteLine(":( The discount code does not work");
            }
            //Checking to see if new total value is correctly calculated
            try
            {
                Assert.That(discountDetails.GetNewTotalValue(), Is.EqualTo(discountDetails.GetPreviousTotalValue() - discountDetails.GetDiscountValue() + discountDetails.GetShippingCostValue()));
                Console.WriteLine(":) The total is correctly calculated");
            }
            catch (AssertionException)
            {
                Console.WriteLine(":( The total is not correctly calculated");
            }
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            CartPOM cart = new CartPOM(_driver);
            cart.ProceedToCheckout();
        }

        [When(@"I fill in billing details with")]
        public void WhenIFillInBillingDetailsWith(Table table)
        {
            BillingDetailsPOM billing = new BillingDetailsPOM(_driver);
            billing.SetFirstName(table.Rows[0]["First Name"]).SetLastName(table.Rows[0]["Last Name"]).SetAddress(table.Rows[0]["Address"]).SetCity(table.Rows[0]["City"]).SetPostcode(table.Rows[0]["Postcode"]).SetPhoneNumber(table.Rows[0]["Phone Number"]);
        }

        [When(@"I place the order")]
        public void WhenIPlaceTheOrder()
        {
            BillingDetailsPOM billing = new BillingDetailsPOM(_driver);
            HelperLib myHelper = new HelperLib(_driver);
            //Placing order - first wait for the button to appear as it loads a couple of times when filling the form out
            myHelper.WaitForElementDisabled(By.CssSelector("#place_order"), 5);
            billing.PlaceOrder();
        }

        [Then(@"I should see the same order number in my account orders as the one displayed after placing the order")]
        public void ThenIShouldSeeTheSameOrderNumberInMyAccountOrdersAsTheOneDisplayedAfterPlacingTheOrder()
        {
            //Sanitising initial order number
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"), 10);
            OrderDetailsPOM orderDetails = new OrderDetailsPOM(_driver);
            int orderNumberValue = orderDetails.GetOrderNumberValue();

            //Navigate to my orders
            orderDetails.GoToMyOrders();

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
            }
        }
    }
}
