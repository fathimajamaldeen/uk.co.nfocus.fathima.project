using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using uk.co.nfocus.fathima.project.Support.POMClasses;

namespace uk.co.nfocus.fathima.project.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extent;
        private static ExtentTest _test;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //[BeforeTestRun]
        //public static void BeforeTestRun()
        //{
        //    // Initialise ExtentReports and create a report
        //    _extent = new ExtentReports();
        //    ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter("extent-report.html");
        //    _extent.AttachReporter(htmlReporter);
        //}

        //[BeforeScenario]
        //public void BeforeScenario()
        //{
        //    // Create a test node in the report for each scenario
        //    _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
        //}

        [BeforeScenario]
        public void SetUp()
        {
            _driver = new EdgeDriver();
            _driver.Manage().Window.Maximize();
            _scenarioContext["myDriver"] = _driver;
        }

        [AfterScenario]
        public void TearDown()
        {
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            loginpage.LogOut();
            _driver.Quit();
        }

        //[AfterTestRun]
        //public static void AfterTestRun()
        //{
        //    // Flush the report after all scenarios are executed
        //    _extent.Flush();
        //}
    }
}
