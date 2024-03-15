﻿using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support
{
    internal class HelperLib
    {
        private IWebDriver _driver; //Field to work with passed driver in class methods
        
        //Construct to initialize the HelperLib with a WebDriver instance
        public HelperLib(IWebDriver driver) 
        {
            _driver = driver; //Put it in to this classes field
        }

        //Method to scroll the page vertically by the specified amount
        public void ScrollOnPageVertically(int amount)
        {
            //Using JavaScriptExecutor to scroll the page
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript($"window.scrollTo(0, {amount});");
        }

        //Method to wait for an element identified by the locator to become enabled
        public IWebElement WaitForElement(By locator, int timeoutInSeconds) //A helper method using the IWebDriver field
        {
            // Initializing WebDriverWait to wait for the element to be enabled
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(driver => driver.FindElement(locator));
        }

        //Method to wait for the page to load completely to prevent synchronisation issues
        public void WaitForPageToLoad(int loadInSeconds)
        {
            //Wait for page to load completely using Selenium built in page load
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(loadInSeconds);
            //WebDriver will throw a timeout exception if not loaded within the time
        }

        //Method to make the page wait to scroll adding a delay
        public void WaitForPageToScroll(int waitSeconds)
        {
            // Wait until scrolling action is completed
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitSeconds));
            wait.Until(driver => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));
        }

        //Method to wait for an element identified by the locator to become disabled
        public bool WaitForElementDisabled(By locator, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                //Waiting until the element is disabled or timeout occurs
                return wait.Until(driver =>
                {
                    try
                    {
                        var element = driver.FindElement(locator);
                        return !element.Enabled;//Return true if the element is disabled
                    }
                    catch (NoSuchElementException)
                    {
                        return true;//Return true if the element is not found
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true;//Return true if the element is no longer attached to the document object model (DOM)
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;//Return false if the element is still enabled after the timeout
            }
        }

        //Method to find the row in the table where the "Field" column matches the given fieldName,
        //and return the value from the corresponding "Value" column
        public string GetFieldValue(Table table, string fieldName)
        {
            return table.Rows.Single(row => row["Field"] == fieldName)["Value"];
        }
    }
}
