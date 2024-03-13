using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class NavbarPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality

        //Construct to intialise the WebDriver instance
        public NavbarPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
        }

        //Locators - Finding elements on the page
        public IWebElement Home => _driver.FindElement(By.LinkText("Home"));
        public IWebElement Shop => _driver.FindElement(By.LinkText("Shop"));
        public IWebElement MyAccount => _driver.FindElement(By.PartialLinkText("My account"));
        public IWebElement Cart => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _dismissButton => _driver.FindElement(By.LinkText("Dismiss"));

        //Method to get to the home page
        public void GoHomePage()
        {
            Home.Click();
        }

        //Method to get to the shop page
        public void GoShopPage()
        {
            Shop.Click();
        }

        //Method to get to my account page
        public void GoMyAccountPage()
        {
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForPageToLoad(10);
            myHelper.ScrollOnPageVertically(-0);
            MyAccount.Click();
        }

        //Method to get to cart page
        public void GoCartPage()
        {
            Cart.Click();
        }

        public void DismissPopup()
        {
            _dismissButton.Click();
        }
    }
}
