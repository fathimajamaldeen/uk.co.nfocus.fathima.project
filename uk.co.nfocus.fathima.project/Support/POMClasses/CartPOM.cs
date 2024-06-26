﻿using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class CartPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality
        private HelperLib _helper; // Field to store the HelperLib functionality

        //Construct to intialise the WebDriver instance
        public CartPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
            _helper = new HelperLib(_driver); //Assigning the helper after driver is assigned
        }

        //Locators - finding elements on the page and waiting for certain elements to appear first
        
        private IWebElement _couponCodeField => _driver.FindElement(By.CssSelector("#coupon_code"));
        private IWebElement _applyCouponButton => _helper.WaitForElementToBeVisible(By.Name("apply_coupon"), 15);
        private IWebElement _proceedToCheckout => _helper.WaitForElementToBeVisible(By.LinkText("Proceed to checkout"),10);
        private IWebElement _removeCoupon => _helper.WaitForElementToBeVisible(By.LinkText("[Remove]"), 10);
        private IWebElement _removeItem => _helper.WaitForElementToBeVisible(By.LinkText("×"), 10);
        
        //Method to apply a discount code in the cart
        public void ApplyDiscountCode(string discountCode)
        {
            //Clear the discount code input field and enter the provided code
            _couponCodeField.Clear();
            _couponCodeField.SendKeys(discountCode);
            //Click the apply coupon button
            _applyCouponButton.Click();
            Console.WriteLine($"Applied '{discountCode}' as the coupon code");
        }

        //Method to proceed to checkout from the cart
        public void ProceedToCheckout()
        {
            _proceedToCheckout.Click();

        }

        //Method to Remove the coupon code from cart
        public void RemoveCouponCode()
        {
            _helper.WaitForPageToLoad(10);
            _removeCoupon.Click();
            Console.WriteLine("Removed the coupon code from the cart");
        }

        //Method to remove the item from cart
        public void RemoveItemFromCart()
        {
            _helper.WaitForPageToLoad(10);
            _removeItem.Click();
            Console.WriteLine("Removed item from the cart");
        }

        //Method which implements cart clean up
        public void CartCleanUp()
        {
            //Perform cleanup only if there are items in the cart
            if (AreThereItemsInCart())
            {
                try
                {
                    _helper.WaitForPageToLoad(10);
                    RemoveCouponCode();
                    RemoveItemFromCart();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during cart cleanup: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("There are no items in the cart. Skipping cart cleanup.");
            }
        }

        //Method to check if there are any items in the cart
        private bool AreThereItemsInCart()
        {
            //Find elements representing items in the cart
            var cartItems = _driver.FindElements(By.LinkText("×"));

            //Check if any elements were found
            return cartItems.Count > 0;
        }
    }
}