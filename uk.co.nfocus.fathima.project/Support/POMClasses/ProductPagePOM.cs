using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    public class ProductPagePOM
    {
        private IWebDriver _driver; //Field to store WebDriver instance
        private string _item;
        public ProductPagePOM(IWebDriver driver, string item) //Constructor to intitalise the WebDriver instance
        {
            _driver = driver; //Assign the WebDriver instance passed into the field
            _item = item;
        }

        //Locators - Finding elements on the page
        private IWebElement Item => _driver.FindElement(By.CssSelector($"[aria-label='Add “{_item}” to your cart']"));

        //Clicks on the belt item and adds to cart
        public void AddItemToCart()
        {
            Item.Click();
        }
        
    }
}
