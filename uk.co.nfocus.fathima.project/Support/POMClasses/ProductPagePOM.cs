using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    public class ProductPagePOM
    {
        private readonly IWebDriver _driver;

        public ProductPagePOM(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators - Finding elements on the page
        private IWebElement Belt => _driver.FindElement(By.CssSelector("#main > ul > li.product.type-product.post-28.status-publish.instock.product_cat-accessories.has-post-thumbnail.sale.shipping-taxable.purchasable.product-type-simple > a.button.product_type_simple.add_to_cart_button.ajax_add_to_cart"));

        //Service method - doing things with elemenet on the page
        public void AddBeltToCart()
        {
            Belt.Click();
        }
    }
}
