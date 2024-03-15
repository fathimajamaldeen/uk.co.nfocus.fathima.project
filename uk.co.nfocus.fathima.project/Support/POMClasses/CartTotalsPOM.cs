using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class CartTotalsPOM
    {
        private IWebDriver _driver; //Field to store WebDriver instance
        private HelperLib _helper; //Field to store the HelperLib functionality

        public CartTotalsPOM(IWebDriver driver) //Constructor to intitalise the WebDriver instance
        {
            _driver = driver; //Assign the WebDriver instance passed into the field
            _helper = new HelperLib(_driver); //Assigning the helper after driver is assigned
        }

        //Locators - finding elements on the page
        private IWebElement _discountAmount => _helper.WaitForElement(By.CssSelector($".cart-discount .amount"), 5);
        private IWebElement _previousTotal => _driver.FindElement(By.CssSelector(".cart-subtotal bdi"));
        private IWebElement _newTotal => _driver.FindElement(By.CssSelector(".order-total bdi"));
        private IWebElement _shippingCost => _driver.FindElement(By.CssSelector(".shipping bdi"));

        

        //Method to recieve the previous total value
        public decimal GetPreviousTotalValue()
        {
            //Break the text down and convert it to decimal value and returns the value extracted
            return ConversionHelper.StringToDecimal(_previousTotal.Text);
        }

        //Method to recieve the new total value after applying the discount
        public decimal GetNewTotalValue()
        {
            //Break the text down and convert it to decimal value and returns the value extracted     
            return ConversionHelper.StringToDecimal(_newTotal.Text);
        }

        //Method to recieve the shipping cost value after applying the discount
        public decimal GetShippingCostValue()
        {
            //Break the text down and convert it to decimal value and rReturns the value extracted
            return ConversionHelper.StringToDecimal(_shippingCost.Text);
        }

        //Method to recieve the discount value
        public decimal GetDiscountValue()
        {

            //Break the text down and convert it to decimal value and returns the value extracted
            return ConversionHelper.StringToDecimal(_discountAmount.Text);
        }
    }
}