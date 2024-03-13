using OpenQA.Selenium;

namespace uk.co.nfocus.fathima.project.Support.POMClasses
{
    internal class BillingDetailsPOM
    {
        private IWebDriver _driver; //Field to store WebDriver instance
        public BillingDetailsPOM(IWebDriver driver) //Constructor to intitalise the WebDriver instance
        {
            _driver = driver; //Assign the WebDriver instance passed into the field
        }

        //Locators - finding elements on the page
        private IWebElement _firstNameField => _driver.FindElement(By.CssSelector("#billing_first_name"));
        private IWebElement _lastNameField => _driver.FindElement(By.CssSelector("#billing_last_name"));
        private IWebElement _addressField => _driver.FindElement(By.CssSelector("#billing_address_1"));
        private IWebElement _cityField => _driver.FindElement(By.CssSelector("#billing_city"));
        private IWebElement _postcodeField => _driver.FindElement(By.CssSelector("#billing_postcode"));
        private IWebElement _phoneNumberField => _driver.FindElement(By.CssSelector("#billing_phone"));
        private IWebElement _emailField => _driver.FindElement(By.CssSelector("#billing_email"));
        private IWebElement _placeOrderButton => _driver.FindElement(By.CssSelector("#place_order"));

        //Method to set the first name field whilst clearing the field first and returns the instance
        public BillingDetailsPOM SetFirstName(string firstName)
        {
            _firstNameField.Clear();
            _firstNameField.SendKeys(firstName);
            Console.WriteLine("Added first name");
            return this; 
        }

        //Method to set the last name field whilst clearing the field first and returns the instance
        public BillingDetailsPOM SetLastName(string lastName)
        {
            _lastNameField.Clear();
            _lastNameField.SendKeys(lastName);
            Console.WriteLine("Added last name");
            return this;
        }

        //Method to set the address field whilst clearing the field first and returns the instance
        public BillingDetailsPOM SetAddress(string address)
        {
            _addressField.Clear();
            _addressField.SendKeys(address);
            Console.WriteLine("Added billing addres");
            return this;
        }

        //Method to set the city field whilst clearing the field first and returns the instance
        public BillingDetailsPOM SetCity(string city)
        {
            _cityField.Clear();
            _cityField.SendKeys(city);
            Console.WriteLine("Added billing city");
            return this;
        }

        //Method to set the postcode field whilst clearing the field first and returns the instance
        public BillingDetailsPOM SetPostcode(string postcode)
        {
            _postcodeField.Clear();
            _postcodeField.SendKeys(postcode);
            Console.WriteLine("Added billing postcode");
            return this;
        }

        //Method to set the phone number field whilst clearing the field first and returns the instance
        public BillingDetailsPOM SetPhoneNumber(string phoneNumber)
        {
            _phoneNumberField.Clear();
            _phoneNumberField.SendKeys(phoneNumber);
            Console.WriteLine("Added billing phone number");
            return this;
        }

        //Method to set the billing email field whilst clearing the field first and return the instance
        public BillingDetailsPOM SetEmail(string email)
        {
            _emailField.Clear();
            _emailField.SendKeys(email);
            Console.WriteLine("Added email address");
            return this;
        }
        
        //Method to click on the place order button and returns the instance
        public BillingDetailsPOM PlaceOrder()
        {
            //Waiting for the place order button to be clickable
            HelperLib myHelper = new HelperLib(_driver);
            myHelper.WaitForElementDisabled(By.CssSelector("#place_order"), 2);
            _placeOrderButton.Click();
            return this;
        }

        //Method to fill in the billing details
        public void FillInBillingDetails(BillingTable BillingInformation)
        {
            SetFirstName(BillingInformation._firstName);
            SetLastName(BillingInformation._lastName);
            SetAddress(BillingInformation._address);
            SetCity(BillingInformation._city);
            SetPostcode(BillingInformation._postcode);
            SetPhoneNumber(BillingInformation._phoneNumber);
            SetEmail(BillingInformation._email);
        }

        //Create a BillingTable object using the provided feature table
        public BillingTable CreateBillingDetail(Table BillingInfo)
        {
            //Extracts individual field values using the GetFieldValue method from the helperlib
            HelperLib myHelper = new HelperLib(_driver);
            string firstName = myHelper.GetFieldValue(BillingInfo, "First Name");
            string lastName = myHelper.GetFieldValue(BillingInfo, "Last Name");
            string address = myHelper.GetFieldValue(BillingInfo, "Address");
            string city = myHelper.GetFieldValue(BillingInfo, "City");
            string postcode = myHelper.GetFieldValue(BillingInfo, "Postcode");
            string phoneNumber = myHelper.GetFieldValue(BillingInfo, "Phone Number");
            string email = myHelper.GetFieldValue(BillingInfo, "Email");
            //Creates a new BillingTable object with the extracted field values
            return new BillingTable(firstName, lastName, address, city, postcode, phoneNumber, email);
        }


    }
}
