﻿using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support
{
    public class WebDriverWrapper
    {
        private IWebDriver _driver;

        public IWebDriver Driver { get => _driver; set => _driver = value; }
    }
}
