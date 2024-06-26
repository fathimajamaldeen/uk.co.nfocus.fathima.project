﻿using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class NavbarPOM
    {
        private IWebDriver _driver; //Field to store the WebDriver functionality
        private HelperLib _helper; //Field to store the HelperLib functionality

        //Construct to intialise the WebDriver instance
        public NavbarPOM(IWebDriver driver)
        {
            _driver = driver; //Assigning the WebDriver instance passed in to the field
            _helper = new HelperLib(_driver); //Assigning the helper after driver is assigned
        }

        //Locators - Finding elements on the page and waiting for certain elements to appear first
        public IWebElement Home => _driver.FindElement(By.LinkText("Home"));
        public IWebElement Shop => _driver.FindElement(By.LinkText("Shop"));
        public IWebElement MyAccount => _helper.WaitForElementToBeVisible(By.PartialLinkText("My account"), 9);
        public IWebElement Cart => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _viewCart => _helper.WaitForElementToBeVisible(By.LinkText("View cart"), 15);
        private IWebElement _dismissButton => _driver.FindElement(By.LinkText("Dismiss"));

        //Method to get to the home page
        public void GoHomePage()
        {
            Home.Click();
            Console.WriteLine("Navigated to Home");
        }

        //Method to get to the shop page
        public void GoShopPage()
        {
            Shop.Click();
            Console.WriteLine("Navigated to Shop");
        }

        //Method to get to my account page
        public void GoMyAccountPage()
        {
            _helper.WaitForPageToLoad(5);
            //Scrolls the page back to the top 
            _helper.ScrollOnPageVertically(-100);
            _helper.WaitForPageToScroll(5);
            try
            {
                MyAccount.Click();
                Console.WriteLine("Navigated to My Account");
            }
            catch
            {
                MyAccount.Click();
                Console.WriteLine("Navigated to My Account");
            }
        }

        //Method to get to cart page
        public void GoCartPage()
        {
            Cart.Click();
            Console.WriteLine("Navigated to Cart");
        }

        //Method to navigate to the "View cart" page
        public void ViewCart()
        {
            //Waits for page to load
            _helper.WaitForPageToLoad(10);
            //Click on the 'View cart' link 
            _viewCart.Click();
            Console.WriteLine("Navigated to View Cart");
        }

        //Method to navigate to login page
        public void NavigateToLoginPage()
        {
            GoMyAccountPage();
            DismissPopup();
            Console.WriteLine("Navigated to Login");
        }

        //Method to dismiss the pop up
        public void DismissPopup()
        {
            //Dismiss the store demo banner if present
            try
            {
                _dismissButton.Click();
                Console.WriteLine("Dismissed pop up");
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