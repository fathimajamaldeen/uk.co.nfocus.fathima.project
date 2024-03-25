using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class AccountOrderHistoryPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality

        //Constructor to intialise the WebDriver instance
        public AccountOrderHistoryPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
        }

        //Locators - finding elements on the page and waiting for certain elements to appear first
        private IWebElement _orderNumberInAccount => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number a:first-of-type"));

        //Method to recieve order number value in the account page
        public int GetOrderNumberInAccountValue()
        {
            //Return the converted value and write out the value
            int orderNumberInAccountValue = ConversionHelper.ConvertStringToInt(_orderNumberInAccount.Text);
            Console.WriteLine($"The order number in order history is {orderNumberInAccountValue}");
            return orderNumberInAccountValue;
        }
    }
}
