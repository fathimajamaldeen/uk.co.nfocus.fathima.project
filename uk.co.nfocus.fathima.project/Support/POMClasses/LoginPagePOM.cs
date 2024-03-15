using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class LoginPagePOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality
        private NavbarPOM _navbar; //Field to store the navbar as it is within this page
        private HelperLib _helper; //Field to store the HelperLib functionality

        //Construct to intialise the WebDriver instance
        public LoginPagePOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
            _navbar = new NavbarPOM(_driver); //Initialise here as it is within the login page
            _helper = new HelperLib(_driver); //Assigning the helper after driver is assigned
        }

        //Locators - finding elements on the page
        private IWebElement _usernameField => _driver.FindElement(By.CssSelector("#username"));
        private IWebElement _passwordField => _driver.FindElement(By.CssSelector("#password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));
        private IWebElement _logoutButton => _helper.WaitForElement(By.LinkText("Logout"), 10);


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

        //Method to log out from the website
        public void LogOut()
        {
            try
            {
                _logoutButton.Click();
                Console.WriteLine("Completed Log out process");
            }
            catch
            {
                _logoutButton.Click();
                Console.WriteLine("Completed Log out process");
            }
        }

    }
}