using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Locate the element containing the previous total value and turn it into text
            string previousTotal = _previousTotal.Text;
            //Break the text down and convert it to decimal value and removes the pound sign to compare the values
            decimal previousTotalValue = decimal.Parse(previousTotal.Replace("£", ""));
            //Returns the value extracted
            return previousTotalValue;
        }
              
        //Method to recieve the new total value after applying the discount
        public decimal GetNewTotalValue()
        {
            //Locate the element containing the new total value and turn it into text
            string newTotal = _newTotal.Text;
            //Break the text down and convert it to decimal value and removes the pound sign to compare the values
            decimal newTotalValue = decimal.Parse(newTotal.Replace("£", ""));
            //Returns the value extracted
            return newTotalValue;
        }

        //Method to recieve the shipping cost value after applying the discount
        public decimal GetShippingCostValue()
        {
            //Locate the element containing the shipping cost value and turn it into text
            string shippingCost = _shippingCost.Text;
            //Break the text down and convert it to decimal value and removes the pound sign to compare the values 
            decimal shippingCostValue = decimal.Parse(shippingCost.Replace("£", ""));
            //Returns the value extracted
            return shippingCostValue;
        }

        //Method to recieve the discount value
        public decimal GetDiscountValue(string discountName)
        {

            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElement(By.CssSelector($".cart-discount.coupon-{discountName} td span"), 5);
            //Locate the element containing the discount value and turn it into text
            string discount = _driver.FindElement(By.CssSelector($".cart-discount.coupon-{discountName} td span")).Text;
            //Break the text down and convert it to decimal value and removes the pound sign
            decimal discountValue = decimal.Parse(discount.Replace("£", ""));
            //Returns the value extracted
            return discountValue;
        }
    }
}
