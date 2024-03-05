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

        //Method to recieve the previous total value
        public decimal GetPreviousTotalValue()
        {
            //Locate the element containing the previous total value and turn it into text
            string previousTotal = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi")).Text;
            //Break the text down and convert it to decimal value and removes the pound sign
            decimal previousTotalValue = decimal.Parse(previousTotal.Replace("£", ""));
            //Returns the value extracted
            return previousTotalValue;
        }

        //Method to recieve the discount value
        public decimal GetDiscountValue(string discountName)
        {
            //Locate the element containing the discount value and turn it into text
                                         
            string discount = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-" + discountName + " > td > span")).Text;
            Console.WriteLine(discount);
            //Break the text down and convert it to decimal value and removes the pound sign
            decimal discountValue = decimal.Parse(discount.Replace("£", ""));
            //Returns the value extracted
            return discountValue;
        }

        //Method to recieve the new total value after applying the discount
        public decimal GetNewTotalValue()
        {
            //Locate the element containing the new total value and turn it into text
            string newTotal = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span")).Text;
            //Break the text down and convert it to decimal value and removes the pound sign
            decimal newTotalValue = decimal.Parse(newTotal.Replace("£", ""));
            //Returns the value extracted
            return newTotalValue;
        }

        //Method to recieve the shipping cost value after applying the discount
        public decimal GetShippingCostValue()
        {
            //Locate the element containing the shipping cost value and turn it into text
            string shippingCost = _driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi")).Text;
            //Break the text down and convert it to decimal value and removes the pound sign
            decimal shippingCostValue = decimal.Parse(shippingCost.Replace("£", ""));
            //Returns the value extracted
            return shippingCostValue;
        }
    }
}
