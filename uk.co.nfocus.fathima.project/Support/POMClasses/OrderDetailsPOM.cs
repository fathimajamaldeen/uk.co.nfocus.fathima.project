using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class OrderDetailsPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality
        private NavbarPOM _navbar; //Field to store the navbar as it is within this page

        //Construct to intialise the WebDriver instance
        public OrderDetailsPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
            _navbar = new NavbarPOM(_driver); //Initialise here as it is within the order details page
        }

        //Locators - Finding elements on the page
        private IWebElement _orderNumber => _driver.FindElement(By.CssSelector(".woocommerce-order-overview__order.order strong"));
        private IWebElement _orderNumberInAccount => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number a:first-of-type"));
        private IWebElement _myOrders => _driver.FindElement(By.LinkText("Orders"));

        //Method to recieve order number value from the order details page
        public int GetOrderNumberValue()
        {
            //Waiting for the order number to be available 
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElement(By.CssSelector(".woocommerce-order-overview__order.order strong"), 10);
            //Return the converted value
            return ConversionHelper.StringToInt(_orderNumber.Text);
        }

        //Method to recieve order number value in the account page
        public int GetOrderNumberInAccountValue()
        {
            //Return the converted value 
            return ConversionHelper.StringToInt(_orderNumberInAccount.Text);
        }

        //Method to go to my orders page
        public void GoToMyOrders()
        {
            _navbar.GoMyAccountPage();
            _myOrders.Click();
        }


    }
}