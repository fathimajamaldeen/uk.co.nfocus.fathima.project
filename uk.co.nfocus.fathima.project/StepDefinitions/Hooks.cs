using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using uk.co.nfocus.fathima.project.Support.POMClasses;
using uk.co.nfocus.fathima.project.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace uk.co.nfocus.fathima.project.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private WebDriverWrapper _driverWrapper;

        private readonly ScenarioContext _scenarioContext;
        private static AventStack.ExtentReports.ExtentReports s_extent;
        private AventStack.ExtentReports.ExtentTest s_scenario, s_step;
        private static AventStack.ExtentReports.ExtentTest s_feature;
        private static string s_reportpath = System.IO.Directory.GetParent(@"../../../").FullName
            + Path.DirectorySeparatorChar + "Reports"
            + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.DirectorySeparatorChar;

        public Hooks(ScenarioContext scenarioContext, WebDriverWrapper driverWrapper)
        {
            _scenarioContext = scenarioContext;
            _driverWrapper = driverWrapper;
        }

        //Runs once before all tests
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Configures ExtentReports
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(s_reportpath);
            s_extent = new AventStack.ExtentReports.ExtentReports();
            s_extent.AttachReporter(htmlReporter);
        }

        //Runs before each scenario
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            //Create a new ExtentTest for the scenario
            s_scenario = s_extent.CreateTest(context.ScenarioInfo.Title);

            string browser = Environment.GetEnvironmentVariable("BROWSER");

            Console.WriteLine("Browser set to: " + browser);
            if (browser == null) //Sanitising the input that was fetched from envirnomental variable
            {
                browser = "edge";
                Console.WriteLine("BROWSER env not set: Setting to Edge");
            }
            //Instantiate a browser based on variable
            switch (browser)
            {
                case "edge":
                    _driverWrapper.Driver = new EdgeDriver();
                    break;
                case "firefox":
                    _driverWrapper.Driver = new FirefoxDriver();
                    break;
                case "chromeheadless":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--headless"); //Chrome *used* to have a seperate rendering engine for headless. https://developer.chrome.com/docs/chromium/new-headless
                    _driverWrapper.Driver = new ChromeDriver(chromeOptions);
                    //You may want to set a defined window size...
                    break;
                case "firefoxheadless":
                    FirefoxOptions firefoxoptions = new FirefoxOptions();
                    firefoxoptions.AddArgument("--headless");
                    _driverWrapper.Driver = new FirefoxDriver(firefoxoptions);
                    break;
                default:
                    _driverWrapper.Driver = new ChromeDriver();
                    break;
            }
            //Make the window full screen
            _driverWrapper.Driver.Manage().Window.Maximize();

            //Get the starting URL from the runsettings file 
            string startURL = null;
            startURL = TestContext.Parameters["WebAppURL"];
            //If there is no value then failing the test and adding to report 
            //Else set the driver url to it 
            if (startURL == null)
            {
                s_scenario.Fail("There is no URL for the website");
                Assert.Fail("There is no URL for the website");
            }
            else
            {
                _driverWrapper.Driver.Url = startURL;
            }
        }

        //Runs before each step
        [BeforeStep]
        public void BeforeStep(ScenarioContext context)
        {
            //Create a new ExtentTest node for the step
            s_step = s_scenario.CreateNode(context.StepContext.StepInfo.Text);
        }

        //Runs after each step
        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            //Log the status of the step based on the scenario execution status
            var stepStatus = context.ScenarioExecutionStatus;
            switch (stepStatus)
            {
                case ScenarioExecutionStatus.OK:
                    s_step.Log(Status.Pass, context.StepContext.StepInfo.Text);
                    break;
                case ScenarioExecutionStatus.TestError:
                    //When the test fails 
                    s_step.Log(Status.Fail, $"{context.StepContext.StepInfo.Text}. Test failure reason: {context.TestError.Message}");
                    //Creates a screenshot instance
                    Screenshots screenshotHelper = new Screenshots(_driverWrapper.Driver);
                    //Make a unique name for the screenshot
                    string screenshotName = $"{context.StepContext.StepInfo.Text}_{DateTime.Now:yyyyMMddHHmm}.png";
                    //Constructs full path for saving screenshot to the same folder as where the report is
                    string screenshotPath = Path.Combine(s_reportpath, screenshotName);
                    HelperLib myHelper = new HelperLib(_driverWrapper.Driver);
                    //Move the page down to get a clearer screenshot
                    myHelper.ScrollOnPageVertically(250);
                    // Wait until scrolling action is completed
                    myHelper.WaitForPageToScroll(10);
                    //Actually captures the screenshot
                    screenshotHelper.TakeScreenshot(screenshotPath);
                    //Add the screenshot to the Extent Report
                    s_step.AddScreenCaptureFromPath(screenshotPath);
                    break;
                case ScenarioExecutionStatus.UndefinedStep:
                    s_step.Log(Status.Warning, $"{context.StepContext.StepInfo.Text} - Step status: {stepStatus}");
                    break;
                case ScenarioExecutionStatus.BindingError:
                    s_step.Log(Status.Error, $"{context.StepContext.StepInfo.Text} - Step status: {stepStatus}");
                    break;
                case ScenarioExecutionStatus.StepDefinitionPending:
                    s_step.Log(Status.Skip, $"{context.StepContext.StepInfo.Text} - Step status: {stepStatus}");
                    break;
                default:
                    s_step.Log(Status.Info, $"Step status: {stepStatus}");
                    break;
            }
        }

        //Runs after each feature
        [AfterFeature]
        public static void AfterFeature()
        {
            // Flush the report after all scenarios are executed
            s_extent.Flush();
        }

        //Runs after Test 1 only to clean up the cart
        [AfterScenario("@Test1")]
        public void Cleanup()
        {
            if (ScenarioContext.Current.TestError == null)
            {
                //Removes the coupon and item from the cart 
                CartPOM cart = new CartPOM(_driverWrapper.Driver);
                cart.RemoveCouponCode();
                cart.RemoveItemFromCart();
                HelperLib myHelper = new HelperLib(_driverWrapper.Driver);
                myHelper.WaitForPageToLoad(3);
            }
            else
            {
                TearDown();
            }
        }

        //Runs after each scenario
        [AfterScenario]
        public void TearDown()
        {
            if (ScenarioContext.Current.TestError == null)
            {
                //Perform cleanup actions
                LoginPagePOM loginpage = new LoginPagePOM(_driverWrapper.Driver);
                loginpage.LogOut();
            }
            if (_driverWrapper.Driver != null)
            {
                _driverWrapper.Driver.Quit();
                _driverWrapper.Driver.Dispose();
            }
        }

    }
}