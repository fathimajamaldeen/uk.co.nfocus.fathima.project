namespace uk.co.nfocus.fathima.project.Support
{

    //This class represents a support billing table with
    //internal properties for storing billing information.
    internal class BillingTable
    {
        //Internal properties for storing billing details.
        internal string _firstName { get; set; }
        internal string _lastName { get; set; }
        internal string _address { get; set; }
        internal string _city { get; set; }
        internal string _postcode { get; set; }
        internal string _phoneNumber { get; set; }
        internal string _email { get; set; }

        //Constructor for initializing billing details.
        public BillingTable(string firstName, string lastName, 
            string address, string city, string postcode, 
            string phoneNumber, string email)
        {
            //Initialise billing details with provided values.
            this._firstName = firstName;
            this._lastName = lastName;
            this._address = address;
            this._city = city;
            this._postcode = postcode;
            this._phoneNumber = phoneNumber;
            this._email = email;
        }
    }
}
