using System;
using System.IO;
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

        [ThreadStatic]
        static AventStack.ExtentReports.ExtentReports extent;
        AventStack.ExtentReports.ExtentTest scenario, step;
        static AventStack.ExtentReports.ExtentTest feature;
        static string reportpath = System.IO.Directory.GetParent(@"../../../").FullName
            + Path.DirectorySeparatorChar + "Result"
            + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyyHHmmss");

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportpath);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            feature = extent.CreateTest(context.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            scenario = feature.CreateNode(context.ScenarioInfo.Title);

            _driver = new EdgeDriver();
            _driver.Manage().Window.Maximize();
            _scenarioContext["myDriver"] = _driver;
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext context)
        {
            step = scenario.CreateNode(context.StepContext.StepInfo.Text);
        }


        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            if (context.TestError == null)
            {
                step.Log(Status.Pass, context.StepContext.StepInfo.Text);
            }
            else if (context.TestError != null)
            {
                step.Log(Status.Fail, context.StepContext.StepInfo.Text);
            }
        }


        [AfterFeature]
        public static void AfterFeature()
        {
            // Flush the report after all scenarios are executed
            extent.Flush();
        }

        [AfterScenario]
        public void TearDown()
        {
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            loginpage.LogOut();
            _driver.Quit();
        }
    }
}
