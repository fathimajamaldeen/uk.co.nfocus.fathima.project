using OpenQA.Selenium;
using AventStack.ExtentReports;

namespace uk.co.nfocus.fathima.project.Support
{
    internal class Screenshots
    {
        private IWebDriver _driver;
        private ExtentTest _test;

        public Screenshots(IWebDriver driver, ExtentTest test)
        {
            _driver = driver;
            _test = test; // Assign the test instance passed from the calling test
        }

        public void TakeScreenshot(string screenshotName)
        {
            try
            {
                // Convert driver to ITakesScreenshot interface
                ITakesScreenshot screenshotDriver = (ITakesScreenshot)_driver;

                // Take screenshot and save to a file
                Screenshot screenshot = screenshotDriver.GetScreenshot();

                // Get the directory path where the executable is located
                string directoryPath = System.IO.Directory.GetParent(@"../../../").FullName + Path.DirectorySeparatorChar + "Result" + Path.DirectorySeparatorChar;

                // Construct the full file path for the screenshot
                string screenshotFilePath = Path.Combine(directoryPath, screenshotName);

                // Save the screenshot
                screenshot.SaveAsFile(screenshotFilePath);

                // Add screenshot to extent report
                _test.Fail("Screenshot:", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotFilePath).Build());

                Console.WriteLine("HEllo" + File.Exists(screenshotFilePath));
            }
            catch (Exception ex)
            {
                // Log any exception that occurs
                Console.WriteLine("Exception while taking screenshot: " + ex.Message);
            }
        }

    }
}
