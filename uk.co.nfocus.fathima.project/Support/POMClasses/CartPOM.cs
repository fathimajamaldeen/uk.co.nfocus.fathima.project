using OpenQA.Selenium;

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
        private IWebElement _applyCouponButton => _helper.WaitForElement(By.Name("apply_coupon"), 15);
        private IWebElement _proceedToCheckout => _helper.WaitForElement(By.LinkText("Proceed to checkout"),10);
        private IWebElement _removeCode => _helper.WaitForElement(By.LinkText("[Remove]"), 5);
        private IWebElement _removeItem => _helper.WaitForElement(By.LinkText("×"), 5);

        

        //Method to apply a discount code in the cart
        public void ApplyDiscountCode(string discountCode)
        {
            //Clear the discount code input field and enter the provided code
            _couponCodeField.Clear();
            _couponCodeField.SendKeys(discountCode);
            //Click the apply coupon button
            _applyCouponButton.Click();
        }

        //Method to proceed to checkout from the cart
        public void ProceedToCheckout()
        {
            _proceedToCheckout.Click();

        }

        //Method to Remove the coupon code from cart
        public void RemoveCouponCode()
        {
            //Wait for the remove button to appear
            _helper.WaitForPageToLoad(15);
            _removeCode.Click();
        }

        //Method to remove the item from cart
        public void RemoveItemFromCart()
        {
            //Wait for the remove button to appear
            _helper.WaitForPageToLoad(10);
            _removeItem.Click();
        }

        //Method which implements cart clean up
        public void CartCleanUp()
        {
            RemoveCouponCode();
            RemoveItemFromCart();
            _helper.WaitForPageToLoad(3);
        }


    }
}