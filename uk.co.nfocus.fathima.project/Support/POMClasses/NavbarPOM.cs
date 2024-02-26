﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class NavbarPOM
    {
        private IWebDriver _driver; //Field that will hold a driver for service methods in this test to work with

        public NavbarPOM(IWebDriver driver) //Constructor to get the driver from the test
        {
            _driver = driver; //Assigns passed driver in to private field in this class

        }

        //Finding elements on the page
        public IWebElement Home => _driver.FindElement(By.LinkText("Home"));
        public IWebElement Shop => _driver.FindElement(By.LinkText("Shop"));
        public IWebElement MyAccount => _driver.FindElement(By.PartialLinkText("My account"));
        public IWebElement Cart => _driver.FindElement(By.LinkText("Cart"));

        public void goHomePage()
        {
            Home.Click();
        }
        public void goShopPage()
        {
            Shop.Click();
        }
        public void goMyAccountPage()
        {
            MyAccount.Click();
        }
        public void goCartPage()
        {
            Cart.Click();
        }



    }
}
