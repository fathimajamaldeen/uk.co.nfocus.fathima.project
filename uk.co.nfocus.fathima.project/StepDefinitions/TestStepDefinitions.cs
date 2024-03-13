using NUnit.Framework;
using uk.co.nfocus.fathima.project.Support;
using uk.co.nfocus.fathima.project.Support.POMClasses;

namespace uk.co.nfocus.fathima.project.StepDefinitions
{
    [Binding]
    public class TestStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private WebDriverWrapper _driver;

        public TestStepDefinitions(ScenarioContext scenarioContext, WebDriverWrapper driverWrapper)
        {
            _scenarioContext = scenarioContext;
            this._driver = driverWrapper;
        }

        [Given(@"I am logged in on the shopping website")]
        public void GivenIAmLoggedInOnTheShoppingWebsite()
        {
            LoginPagePOM loginpage = new LoginPagePOM(_driver.Driver);
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
            NavbarPOM navbar = new NavbarPOM(_driver.Driver);
            navbar.GoShopPage();
            //Adding belt to cart
            ProductPagePOM product = new ProductPagePOM(_driver.Driver, itemName);
            product.AddItemToCart();
            Console.WriteLine($"Added {itemName} to cart");
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            //Going to view the cart
            CartPOM cart = new CartPOM(_driver.Driver);
            cart.ViewCart();
        }

        [When(@"I apply a discount code '(.*)'")]
        public void WhenIApplyADiscountCode(string discountCode)
        {
            //Storing the discount code
            _scenarioContext["DiscountCode"] = discountCode;
            //Applying the discount code set in the test
            CartPOM cart = new CartPOM(_driver.Driver);
            cart.ApplyDiscountCode(discountCode);
            Console.WriteLine("Applied discount code");
        }

        [Then(@"I should see the discount of (.*)% is applied correctly")]
        public void VerifyDiscountApplication(int discount)
        {
            
            try
            {
                DiscountDetailsPOM discountDetails = new DiscountDetailsPOM(_driver.Driver);
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

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            //Proceeding to checkout
            CartPOM cart = new CartPOM(_driver.Driver);
            cart.ProceedToCheckout();
        }

        [When(@"I fill in billing details, to place the order, with")]
        public void FillingInBillingDetailsToPlaceOrder(Table billingDetailsTable)
        {
            BillingDetailsPOM billing = new BillingDetailsPOM(_driver.Driver);
            //Create billing details with the information passed from the feature table
            BillingTable billingTable = billing.CreateBillingDetail(billingDetailsTable);
            //Filling in the billing details with the table details from the test
            billing.FillInBillingDetails(billingTable);
            billing.PlaceOrder();

        }

        [Then(@"I should see the same order number in my account orders as the one displayed after placing the order")]
        public void VerifyingOrderNumberConsistency()
        {
            OrderDetailsPOM orderDetails = new OrderDetailsPOM(_driver.Driver);
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
