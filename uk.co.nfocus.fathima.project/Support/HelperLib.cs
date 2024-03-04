using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gherkin.CucumberMessages.Types;


namespace uk.co.nfocus.fathima.project.Support
{
    internal class HelperLib
    {
        private IWebDriver _driver; //Field to work with passed driver in class methdos

        public HelperLib(IWebDriver driver) //Get the driver from the calling test
        {
            _driver = driver; //Put it in to this classes field
        }


        public void WaitForElement(By locator, int timeoutInSeconds) //A helper method using the IWebDriver field
        {
            WebDriverWait myWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            myWait.Until(drv => drv.FindElement(locator).Enabled);
        }

        public bool WaitForElementDisabled(By locator, int timeoutInSeconds)
        {
            WebDriverWait myWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                return myWait.Until(drv =>
                {
                    try
                    {
                        var element = drv.FindElement(locator);
                        return !element.Enabled;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void TakeScreenshot(IWebDriver driver, string screenshotName)
        {
            // Convert driver to ITakesScreenshot interface
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

            // Take screenshot and save to a file
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            // Get the directory path where the executable is located
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;

            // Construct the full file path for the screenshot
            string screenshotFilePath = Path.Combine(directoryPath, screenshotName);

            // Save the screenshot
            screenshot.SaveAsFile(screenshotFilePath);
        }
    }
}
