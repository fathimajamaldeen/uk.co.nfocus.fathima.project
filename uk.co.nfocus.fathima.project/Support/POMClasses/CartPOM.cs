using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class CartPOM
    {
        private IWebDriver _driver; //Field that will hold a driver for service methods in this test to work with

        public CartPOM(IWebDriver driver) //Constructor to get the driver from the test
        {
            _driver = driver; //Assigns passed driver in to private field in this class
        }
        public void ViewCart()
        {
            //Viewing cart
            HelperLib myHelper = new HelperLib(_driver); //Instantiate HelperLib class and pass the driver to the constructor
            myHelper.WaitForElement(By.LinkText("View cart"), 10);
            _driver.FindElement(By.LinkText("View cart")).Click();
        }

        public void ApplyDiscountCode(string discountCode)
        {
            _driver.FindElement(By.CssSelector("#coupon_code")).Clear();
            _driver.FindElement(By.CssSelector("#coupon_code")).SendKeys(discountCode);
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElement(By.Name("apply_coupon"), 15);
            _driver.FindElement(By.Name("apply_coupon")).Click();
        }

        public void ProceedToCheckout()
        {
            ViewCart();
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElement(By.LinkText("Proceed to checkout"), 10);
            _driver.FindElement(By.LinkText("Proceed to checkout")).Click();

        }
    }
}
