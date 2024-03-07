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
        private IWebDriver _driver; //Field to store the WebDriver functionality

        //Construct to intialise the WebDriver instance
        public LoginPagePOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
        }

        //Locators - finding elements on the page
        private IWebElement _usernameField => _driver.FindElement(By.CssSelector("#username"));
        private IWebElement _passwordField => _driver.FindElement(By.CssSelector("#password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));
        private IWebElement _logoutButton => _driver.FindElement(By.LinkText("Log out"));

        
        //Method to set the username by clearing the field and setting the input and returning the instance
        public LoginPagePOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
            return this;
        }

        //Method to set the password by clearing the field and setting the input and returning the instance
        public LoginPagePOM SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            return this;
        }

        //Method to submit the login form and return the instance
        public LoginPagePOM SubmitForm()
        {
            _loginButton.Click();
            return this;
        }

        //Method to navigate to login page
        public void NavigateToLoginPage()
        {
            //Get the starting URL from the runsettings file and set the driver to it
            string startURL = TestContext.Parameters["WebAppURL"]; 
            _driver.Url = startURL;
            //Navigate to my account page
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.GoMyAccountPage();
            navbar.DismissPopup();
        }

        //Method to log out from the website
        public void LogOut()
        {
            //Create an instance of the HelperLib
            HelperLib myHelper = new HelperLib(_driver);
            // Scroll to the top of the page again
            myHelper.ScrollOnPage(0);
            //Navigate to my account page
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.GoMyAccountPage();
            //Wait for the logout link to appear
            myHelper.WaitForElement(By.LinkText("Log out"), 20);
            _logoutButton.Click();
            Console.WriteLine("Completed Log out process");
        }

    }
}
