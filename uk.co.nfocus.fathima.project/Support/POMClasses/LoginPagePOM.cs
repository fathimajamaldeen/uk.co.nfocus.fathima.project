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
        private IWebDriver _driver; 
        public LoginPagePOM(IWebDriver driver) 
        { 
            _driver = driver; 
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
            string startURL = TestContext.Parameters["WebAppURL"]; 
            _driver.Url = startURL;
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.goMyAccountPage();
            _driver.FindElement(By.LinkText("Dismiss")).Click(); // Dismisses the label that comes at the start 
        }

        public void LogOut()
        {
            //Going to log out
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.goMyAccountPage();
            //Logging out of the page
            HelperLib myHelper = new HelperLib(_driver); 
            myHelper.WaitForElement(By.LinkText("Log out"), 7);//Wait for the logout link
            _driver.FindElement(By.LinkText("Log out")).Click();
            Console.WriteLine("Completed Log out process");
        }

    }
}
