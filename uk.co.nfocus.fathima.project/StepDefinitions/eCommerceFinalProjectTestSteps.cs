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

            _driver.Url = "https://edgewordstraining.co.uk/demo-site";
            //Going to the log in page
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.Myaccount.Click();
            //Dismissing the bottom pop up saying this is a demo
            _driver.FindElement(By.LinkText("Dismiss")).Click();
            //Logging into the page
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            //Gets the username and password from the .runsettings file
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
            //Viewing cart
            HelperLib myHelper = new HelperLib(_driver); //Instantiate HelperLib class and pass the driver to the constructor
            myHelper.WaitForElement(By.LinkText("View cart"), 10);
            _driver.FindElement(By.LinkText("View cart")).Click();
        }

        [When(@"I apply a discount code '(.*)'")]
        public void WhenIApplyADiscountCode(string edgewords0)
        {
            //Applying coupon
            _driver.FindElement(By.CssSelector("#coupon_code")).Clear();
            _driver.FindElement(By.CssSelector("#coupon_code")).SendKeys("edgewords");
            //Waiting for the Apply coupon button to appear
            HelperLib myHelper = new HelperLib(_driver); //Instantiate HelperLib class and pass the driver to the constructor
            myHelper.WaitForElement(By.Name("apply_coupon"), 15);
            _driver.FindElement(By.Name("apply_coupon")).Click();
            Console.WriteLine("Applied discount code");
        }

        [Then(@"I should see the discount applied correctly")]
        public void ThenIShouldSeeTheDiscountAppliedCorrectly()
        {
            HelperLib myHelper = new HelperLib(_driver); //Instantiate HelperLib class and pass the driver to the constructor
            //Waiting for the details with pop up to come to avoid stale elements
            myHelper.WaitForElement(By.LinkText("[Remove]"), 5);
            //Getting the total value
            string previousTotal = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi")).Text;
            decimal previousTotalValue = decimal.Parse(previousTotal.Replace("£", ""));
            //Getting the discount value
            string discount = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span")).Text;
            decimal discountValue = decimal.Parse(discount.Replace("£", ""));

            //Checking to see that discount is 15% of total value
            try
            {
                Assert.That(discountValue, Is.EqualTo(previousTotalValue * 0.15m));
                Console.WriteLine(":) The discount code works");
            }
            catch (AssertionException)
            {
                Console.WriteLine(":( The discount code does not work");
            }
        
            //Getting the new total value
            string newTotal = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span")).Text;
            decimal newTotalValue = decimal.Parse(newTotal.Replace("£", ""));
            //Getting the shipping cost and value
            string shippingCost = _driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi")).Text;
            decimal shippingCostValue = decimal.Parse(shippingCost.Replace("£", ""));

            //Checking to see if new total value is correctly calculated
            try
            {
                Assert.That(newTotalValue, Is.EqualTo(previousTotalValue - discountValue + shippingCostValue));
                Console.WriteLine(":) The total is correctly calculated");
            }
            catch (AssertionException)
            {
                Console.WriteLine(":( The total is not correctly calculated");
            }
        }
    }
}