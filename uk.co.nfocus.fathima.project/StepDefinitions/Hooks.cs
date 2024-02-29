using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.fathima.project.Support.POMClasses;
using uk.co.nfocus.fathima.project.Support;

namespace uk.co.nfocus.fathima.project.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Before]
        public void SetUp()
        {
            _driver = new EdgeDriver();
            _driver.Manage().Window.Maximize();
            _scenarioContext["myDriver"] = _driver; //When putting stuff in to scenario context we lose the type information
        }

        [After]
        public void TearDown()
        {
            LoginPagePOM loginpage = new LoginPagePOM(_driver);
            loginpage.LogOut();
            _driver.Quit();
        }
    }
}
