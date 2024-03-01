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
        private IWebDriver _driver; //Field to store the WebDriver functionality
        
        //Construct to intialise the WebDriver instance
        public CartPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
        }

        //Method to navigate to the cart page
        public void ViewCart()
        {
            //Create an instance of HelperLib to use the helper method 
            HelperLib myHelper = new HelperLib(_driver);

            //Wait for the 'View cart' link to appear and then click it
            myHelper.WaitForElement(By.LinkText("View cart"), 10);
            _driver.FindElement(By.LinkText("View cart")).Click();
        }

        //Method to apply a discount code in the cart
        public void ApplyDiscountCode(string discountCode)
        {
            //Clear the discount code input field and enter the provided code
            _driver.FindElement(By.CssSelector("#coupon_code")).Clear();
            _driver.FindElement(By.CssSelector("#coupon_code")).SendKeys(discountCode);

            //Create an instance of HelperLib to use the helper method 
            HelperLib myHelper = new HelperLib(_driver);

            //Wait for the 'Apply coupon' button to appear then click it
            myHelper.WaitForElement(By.Name("apply_coupon"), 15);
            _driver.FindElement(By.Name("apply_coupon")).Click();
        }

        //Method to proceed to checkout from the cart
        public void ProceedToCheckout()
        {
            //Navigate to the cart page 
            ViewCart();

            //Create an instance of HelperLib to use the helper method 
            HelperLib myHelper = new HelperLib(_driver);
            
            //Wait for the 'Proceed to checkout' to appear then click it
            myHelper.WaitForElement(By.LinkText("Proceed to checkout"), 10);
            _driver.FindElement(By.LinkText("Proceed to checkout")).Click();

        }
    }
}
