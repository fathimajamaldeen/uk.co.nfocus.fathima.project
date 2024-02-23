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
        private IWebDriver _driver; //Field that will hold a driver for service methods in this test to work with

        public DiscountDetailsPOM(IWebDriver driver) //Constructor to get the driver from the test
        {
            _driver = driver; //Assigns passed driver in to private field in this class
        }

        public decimal GetPreviousTotalValue()
        {
            string previousTotal = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi")).Text;
            decimal previousTotalValue = decimal.Parse(previousTotal.Replace("£", ""));
            return previousTotalValue;
        }

        public decimal GetDiscountValue()
        {
            string discount = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span")).Text;
            decimal discountValue = decimal.Parse(discount.Replace("£", ""));
            return discountValue;
        }

        public decimal GetNewTotalValue()
        {
            string newTotal = _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span")).Text;
            decimal newTotalValue = decimal.Parse(newTotal.Replace("£", ""));
            return newTotalValue;
        }

        public decimal GetShippingCostValue()
        {
            string shippingCost = _driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi")).Text;
            decimal shippingCostValue = decimal.Parse(shippingCost.Replace("£", ""));
            return shippingCostValue;
        }
    }
}
