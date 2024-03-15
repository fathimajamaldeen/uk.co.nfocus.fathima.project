using NUnit.Framework;
using OpenQA.Selenium;
using uk.co.nfocus.fathima.project.Support;
using uk.co.nfocus.fathima.project.Support.POMClasses;

namespace uk.co.nfocus.fathima.project.StepDefinitions
{
    [Binding]
    public class TestStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public TestStepDefinitions(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver; 
        }

        [Given(@"I am logged in on the shopping website")]
        public void GivenIAmLoggedInOnTheShoppingWebsite()
        {
            NavbarPOM navbar = new NavbarPOM(_driver);
            //Going to login page
            navbar.NavigateToLoginPage();
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
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
            ShopPOM product = new ShopPOM(_driver, itemName);
            product.AddItemToCart();
            Console.WriteLine($"Added {itemName} to cart");
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            //Going to view the cart
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.ViewCart();
        }

        [When(@"I apply a discount code '(.*)'")]
        public void WhenIApplyADiscountCode(string discountCode)
        {
            //Applying the discount code set in the test
            CartPOM cart = new CartPOM(_driver);
            cart.ApplyDiscountCode(discountCode);
            Console.WriteLine("Applied discount code");
        }

        [Then(@"I should see the discount of (.*)% is applied correctly")]
        public void VerifyDiscountApplied(int discount)
        {

            try
            {
                CartTotalsPOM discountDetails = new CartTotalsPOM(_driver);
                //Checking to see if the discount is the correct percentage
                decimal discountDecimal = (decimal)discount / 100m;
                decimal expectedDiscountValue = discountDetails.GetPreviousTotalValue() * discountDecimal;
                Assert.That(discountDetails.GetDiscountValue(), Is.EqualTo(expectedDiscountValue), $"Expected discount of {discount}% is not applied correctly");
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

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            NavbarPOM navbar = new NavbarPOM(_driver);
            //Navigate to the cart page 
            navbar.ViewCart();
            //Proceeding to checkout
            CartPOM cart = new CartPOM(_driver);
            cart.ProceedToCheckout();
        }

        [When(@"I fill in billing details, to place the order, with")]
        public void FillBillingDetailsAndPlaceOrder(Table billingDetailsTable)
        {

            BillingDetailsPOM billing = new BillingDetailsPOM(_driver);
            //Create billing details with the information passed from the feature table
            BillingTable billingTable = billing.CreateBillingDetail(billingDetailsTable);
            //Filling in the billing details with the table details from the test
            billing.FillInBillingDetails(billingTable);
            //Placing the order 
            billing.PlaceOrder();
        }


        [Then(@"an order number is shown")]
        public void ThenAnOrderNumberIsShown()
        {
            PlacedOrderPOM placedOrder = new PlacedOrderPOM(_driver);
            //Getting order number from order recieved post ordering item
            int orderNumberValue = placedOrder.GetOrderNumberValue();
            //Storing the order number value
            _scenarioContext["OrderNumberValue"] = orderNumberValue;
        }

        [Then(@"that order number is displayed in order history")]
        public void ThenThatOrderNumberIsDisplayedInOrderHistory()
        {
            //Retriving order number value 
            int orderNumberValue = (int)_scenarioContext["OrderNumberValue"];
            //Going to my orders section of the account
            OrderPagePOM orderDetails = new OrderPagePOM(_driver);
            orderDetails.GoToMyOrders();
            AccountOrderHistoryPOM accountOrderHistory = new AccountOrderHistoryPOM(_driver);
            //Getting order number from orders in account
            int orderNumberInAccountValue = accountOrderHistory.GetOrderNumberInAccountValue();
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