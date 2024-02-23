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
        private IWebDriver _driver; //Field that will hold a driver for service methods in this test to work with

        public OrderDetailsPOM(IWebDriver driver) //Constructor to get the driver from the test
        {
            _driver = driver; //Assigns passed driver in to private field in this class
        }

        public int GetOrderNumberValue()
        {
            string orderNumber = _driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong")).Text;
            int orderNumberValue = int.Parse(orderNumber);
            return orderNumberValue;
        }

        public void GoToMyOrders()
        {
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.MyAccount.Click();
            _driver.FindElement(By.LinkText("Orders")).Click();
    }
        public int GetOrderNumberInAccountValue()
        {
            string orderNumberInAccount = _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1) > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text;
            int orderNumberInAccountValue = int.Parse(orderNumberInAccount.Replace("#", ""));
            return orderNumberInAccountValue;
        }
    }
}
