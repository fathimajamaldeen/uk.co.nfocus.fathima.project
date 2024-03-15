using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class OrderPagePOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality
        private NavbarPOM _navbar; //Field to store the navbar as it is within this page
        
        //Construct to intialise the WebDriver instance
        public OrderPagePOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
            _navbar = new NavbarPOM(_driver); //Initialise here as it is within the order details page
        }

        //Locators - finding elements on the page and waiting for certain elements to appear first
        private IWebElement _myOrders => _driver.FindElement(By.LinkText("Orders"));

        //Method to go to my orders page
        public void GoToMyOrders()
        {
            _navbar.GoMyAccountPage();
            _myOrders.Click();
        }
    }
}