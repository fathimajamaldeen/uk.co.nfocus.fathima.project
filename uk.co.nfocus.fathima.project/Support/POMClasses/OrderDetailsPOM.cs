using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class OrderDetailsPOM
    {
        private IWebDriver _driver; 
        public OrderDetailsPOM(IWebDriver driver) 
        {
            _driver = driver; 
        }

        //Locators - Finding elements on the page
        private IWebElement orderNumber => _driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"));
        private IWebElement orderNumberInAccount => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1) > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));

        //Service method - doing things with elemenet on the page
        public int GetOrderNumberValue()
        {
            int orderNumberValue = int.Parse(orderNumber.Text);
            return orderNumberValue;
        }

        public void GoToMyOrders()
        {
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.goMyAccountPage();
            _driver.FindElement(By.LinkText("Orders")).Click();
    }
        public int GetOrderNumberInAccountValue()
        {
            int orderNumberInAccountValue = int.Parse(orderNumberInAccount.Text.Replace("#", ""));
            return orderNumberInAccountValue;
        }
    }
}
