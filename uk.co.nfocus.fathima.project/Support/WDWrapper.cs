using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support
{
    //WDWrapper class provides a wrapper for WebDriver to
    //make the driver type safe when used
    public class WDWrapper
    {
        private IWebDriver _driver;

        public IWebDriver Driver { get => _driver; set => _driver = value; }
    }
}