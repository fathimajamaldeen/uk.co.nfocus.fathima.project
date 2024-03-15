﻿using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class NavbarPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality

        //Construct to intialise the WebDriver instance
        public NavbarPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
        }

        //Locators - Finding elements on the page
        public IWebElement Home => _driver.FindElement(By.LinkText("Home"));
        public IWebElement Shop => _driver.FindElement(By.LinkText("Shop"));
        public IWebElement MyAccount => _driver.FindElement(By.PartialLinkText("My account"));
        public IWebElement Cart => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _viewCart => _driver.FindElement(By.LinkText("View cart"));
        private IWebElement _dismissButton => _driver.FindElement(By.LinkText("Dismiss"));

        //Method to get to the home page
        public void GoHomePage()
        {
            Home.Click();
        }

        //Method to get to the shop page
        public void GoShopPage()
        {
            Shop.Click();
        }

        //Method to get to my account page
        public void GoMyAccountPage()
        {
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForPageToLoad(10);
            //Scrolls the page back to the top 
            myHelper.ScrollOnPageVertically(0);
            MyAccount.Click();
        }

        //Method to get to cart page
        public void GoCartPage()
        {
            Cart.Click();
        }

        //Method to navigate to the "View cart" page
        public void ViewCart()
        {
            //Create an instance of HelperLib to use the helper method 
            HelperLib myHelper = new HelperLib(_driver);
            //Waits for page to load
            myHelper.WaitForPageToLoad(10);
            //Wait for the 'View cart' link to appear and then click it
            myHelper.WaitForElement(By.LinkText("View cart"), 15);
            _viewCart.Click();
        }

        //Method to navigate to login page
        public void NavigateToLoginPage()
        {
            GoMyAccountPage();
            DismissPopup();
        }


        public void DismissPopup()
        {
            //Dismiss the store demo banner if present
            try
            {
                _dismissButton.Click();
            }
            catch (NoSuchElementException)
            {
                //Log that the banner was not found
                Console.WriteLine("Store demo banner not found.");
                //Proceed without failing the test
            }
        }
    }
}