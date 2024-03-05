using System;
using System.IO;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using uk.co.nfocus.fathima.project.Support.POMClasses;
using NUnit.Framework.Interfaces;

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
            + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.DirectorySeparatorChar;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //Runs once before all tests
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Configures ExtentReports
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportpath);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        //Runs before each feature
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            //Create a new ExtentTest for the feature 
            feature = extent.CreateTest(context.FeatureInfo.Title);
        }

        //Runs before each scenario
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            //Create a new ExtentTest for the scenario
            scenario = feature.CreateNode(context.ScenarioInfo.Title);

            //Initialise WebDriver
            _driver = new EdgeDriver();
            _driver.Manage().Window.Maximize();
            _scenarioContext["myDriver"] = _driver;
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext context)
        {
            //Create a new ExtentTest node for the step
            step = scenario.CreateNode(context.StepContext.StepInfo.Text);
        }


        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            //Log the status of the step based on the scenario execution status
            var stepStatus = context.ScenarioExecutionStatus;
            switch (stepStatus)
            {
                case ScenarioExecutionStatus.OK:
                    step.Log(Status.Pass, context.StepContext.StepInfo.Text);
                    break;
                case ScenarioExecutionStatus.TestError:
                    step.Log(Status.Fail, context.StepContext.StepInfo.Text);
                    break;
                case ScenarioExecutionStatus.UndefinedStep:
                    step.Log(Status.Warning, $"Step status: {stepStatus}");
                    break;
                case ScenarioExecutionStatus.BindingError:
                    step.Log(Status.Error, $"Step status: {stepStatus}");
                    break;
                case ScenarioExecutionStatus.StepDefinitionPending:
                    step.Log(Status.Skip, $"Step status: {stepStatus}");
                    break;
                default:
                    step.Log(Status.Info, $"Step status: {stepStatus}");
                    break;
            }
        }

        //Runs after each feature
        [AfterFeature]
        public static void AfterFeature()
        {
            // Flush the report after all scenarios are executed
            extent.Flush();
        }

        //Runs after each scenario
        [AfterScenario]
        public void TearDown()
        {
            //Perform cleanup actions
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            loginpage.LogOut();
            _driver.Quit();
        }
    }
}
