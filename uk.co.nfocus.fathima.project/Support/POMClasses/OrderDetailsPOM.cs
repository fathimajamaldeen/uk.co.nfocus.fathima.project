﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class OrderDetailsPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality

        //Construct to intialise the WebDriver instance
        public OrderDetailsPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
        }

        //Locators - Finding elements on the page
        private IWebElement _orderNumber => _driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"));
        private IWebElement _orderNumberInAccount => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1) > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));


        //Method to recieve order number value from the order details page
        public int GetOrderNumberValue()
        {
            //Waiting for the order number to be available 
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"), 10);
            //Break the text down and convert it to integer value
            int orderNumberValue = int.Parse(_orderNumber.Text);
            return orderNumberValue;
        }

        //Method to go to my orders page
        public void GoToMyOrders()
        {
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.GoMyAccountPage();
            _driver.FindElement(By.LinkText("Orders")).Click();
    }

        //Method to recieve order number value in the account page
        public int GetOrderNumberInAccountValue()
        {
            //Break the text down and convert it to integer value and get rid of the '#' at the start
            int orderNumberInAccountValue = int.Parse(_orderNumberInAccount.Text.Replace("#", ""));
            return orderNumberInAccountValue;
        }
    }
}
