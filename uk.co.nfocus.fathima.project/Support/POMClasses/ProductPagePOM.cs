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
        private IWebElement Belt => _driver.FindElement(By.CssSelector("#main > ul > li.product.type-product.post-28.status-publish.instock.product_cat-accessories.has-post-thumbnail.sale.shipping-taxable.purchasable.product-type-simple > a.button.product_type_simple.add_to_cart_button.ajax_add_to_cart"));
        
        //Clicks on the belt item and adds to cart
        public void AddBeltToCart()
        {
            Belt.Click();
        }
    }
}
