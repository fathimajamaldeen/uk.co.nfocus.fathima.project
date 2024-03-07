using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    public class ProductPagePOM
    {
        private IWebDriver _driver; //Field to store WebDriver instance
        public ProductPagePOM(IWebDriver driver) //Constructor to intitalise the WebDriver instance
        {
            _driver = driver; //Assign the WebDriver instance passed into the field
        }

        //Locators - Finding elements on the page
        private IWebElement Belt => _driver.FindElement(By.CssSelector("a.button.product_type_simple.add_to_cart_button.ajax_add_to_cart[aria-label='Add “Belt” to your cart']"));

        //Clicks on the belt item and adds to cart
        public void AddBeltToCart()
        {
            Belt.Click();
        }
        
    }
}
