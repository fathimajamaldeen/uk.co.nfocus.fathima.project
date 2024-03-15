using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class DiscountDetailsPOM
    {
        private IWebDriver _driver; //Field to store WebDriver instance
        public DiscountDetailsPOM(IWebDriver driver) //Constructor to intitalise the WebDriver instance
        {
            _driver = driver; //Assign the WebDriver instance passed into the field
        }

        //Locators - finding elements on the page
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
        public decimal GetDiscountValue(string discountName)
        {

            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElement(By.CssSelector($".cart-discount.coupon-{discountName} td span"), 5);
            //Locate the element containing the discount value and turn it into text
            string discount = _driver.FindElement(By.CssSelector($".cart-discount.coupon-{discountName} td span")).Text;
            //Break the text down and convert it to decimal value and returns the value extracted
            return ConversionHelper.StringToDecimal(discount);
        }
    }
}