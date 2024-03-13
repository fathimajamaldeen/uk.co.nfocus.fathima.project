using OpenQA.Selenium;

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
        private IWebElement _logoutButton => _driver.FindElement(By.LinkText("Logout"));

        
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
            //Navigate to my account page
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.GoMyAccountPage();
            navbar.DismissPopup();
        }

        //Method to log out from the website
        public void LogOut()
        {
            HelperLib myHelper = new HelperLib(_driver);
            //Navigate to my account page
            myHelper.WaitForElementDisabled(By.LinkText("My account"), 3);
            NavbarPOM navbar = new NavbarPOM(_driver);
            navbar.GoMyAccountPage();
            //Wait for the logout link to appear
            
            myHelper.WaitForPageToLoad(10);
            myHelper.WaitForElement(By.LinkText("Logout"), 10);
            _logoutButton.Click();
            Console.WriteLine("Completed Log out process");
        }

    }
}
