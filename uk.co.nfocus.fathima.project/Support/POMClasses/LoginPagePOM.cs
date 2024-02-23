using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class LoginPagePOM
    {
        private IWebDriver _driver; //Field that will hold a driver for service methods in this test to work with

        public LoginPagePOM(IWebDriver driver) //Constructor to get the driver from the test
        {
            _driver = driver; //Assigns passed driver in to private field in this class
        }

        //Locators - finding elements on the page
        private IWebElement _usernameField => _driver.FindElement(By.CssSelector("#username"));
        private IWebElement _passwordField => _driver.FindElement(By.CssSelector("#password"));
        private IWebElement _loginButton => _driver.FindElement(By.CssSelector("#customer_login > div.u-column1.col-1 > form > p:nth-child(3) > button"));

        //Service method - doing things with elemenet on the page
        public LoginPagePOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
            return this;
        }
        public LoginPagePOM SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            return this;
        }
        public LoginPagePOM SubmitForm()
        {
            _loginButton.Click();
            return this;
        }
        public void NavigateToLoginPage()
        {
            string startURL = "https://edgewordstraining.co.uk/demo-site";
            _driver.Url = startURL;
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.MyAccount.Click();
            _driver.FindElement(By.LinkText("Dismiss")).Click();
        }

    }
}
