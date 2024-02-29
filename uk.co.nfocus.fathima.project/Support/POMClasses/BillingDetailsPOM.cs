using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class BillingDetailsPOM
    {
        private IWebDriver _driver; 
        public BillingDetailsPOM(IWebDriver driver) 
        {
            _driver = driver; 
        }

        //Locators - finding elements on the page
        private IWebElement _firstNameField => _driver.FindElement(By.CssSelector("#billing_first_name"));
        private IWebElement _lastNameField => _driver.FindElement(By.CssSelector("#billing_last_name"));
        private IWebElement _addressField => _driver.FindElement(By.CssSelector("#billing_address_1"));
        private IWebElement _cityField => _driver.FindElement(By.CssSelector("#billing_city"));
        private IWebElement _postcodeField => _driver.FindElement(By.CssSelector("#billing_postcode"));
        private IWebElement _phoneNumberField => _driver.FindElement(By.CssSelector("#billing_phone"));
        private IWebElement _placeOrderButton => _driver.FindElement(By.CssSelector("#place_order"));

        //Service method - doing things with elemenet on the page
        public BillingDetailsPOM SetFirstName(string firstName)
        {
            _firstNameField.Clear();
            _firstNameField.SendKeys(firstName);
            Console.WriteLine("Added first name");
            return this;
        }
        public BillingDetailsPOM SetLastName(string lastName)
        {
            _lastNameField.Clear();
            _lastNameField.SendKeys(lastName);
            Console.WriteLine("Added last name");
            return this;
        }
        public BillingDetailsPOM SetAddress(string address)
        {
            _addressField.Clear();
            _addressField.SendKeys(address);
            Console.WriteLine("Added billing addres");
            return this;
        }
        public BillingDetailsPOM SetCity(string city)
        {
            _cityField.Clear();
            _cityField.SendKeys(city);
            Console.WriteLine("Added billing city");
            return this;
        }
        public BillingDetailsPOM SetPostcode(string postcode)
        {
            _postcodeField.Clear();
            _postcodeField.SendKeys(postcode);
            Console.WriteLine("Added billing postcode");
            return this;
        }
        public BillingDetailsPOM SetPhoneNumber(string phoneNumber)
        {
            _phoneNumberField.Clear();
            _phoneNumberField.SendKeys(phoneNumber);
            Console.WriteLine("Added billing phone number");
            return this;
        }

        public BillingDetailsPOM PlaceOrder()
        {
            _placeOrderButton.Click();
            return this;
        }


    }
}
