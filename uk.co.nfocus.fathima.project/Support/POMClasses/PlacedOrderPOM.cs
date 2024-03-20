using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class PlacedOrderPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality
        private HelperLib _helper; //Field to store the HelperLib functionality

        //Construct to intialise the WebDriver instance
        public PlacedOrderPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
            _helper = new HelperLib(_driver); //Assigning the helper after driver is assigned
        }

        //Locators - finding elements on the page and waiting for certain elements to appear first
        private IWebElement _orderNumber => _helper.WaitForElementToBeVisible(By.CssSelector(".woocommerce-order-overview__order.order strong"), 10);

        //Method to recieve order number value from the placing order page
        public int GetOrderNumberValue()
        {
            Console.WriteLine($"The order number value is {_orderNumber.Text}");
            //Return the converted value
            return ConversionHelper.StringToInt(_orderNumber.Text);
        }


    }
}
