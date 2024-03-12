﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.fathima.project.Support
{
    internal class BillingTable
    {
        internal string _firstName { get; set; }
        internal string _lastName { get; set; }
        internal string _address { get; set; }
        internal string _city { get; set; }
        internal string _postcode { get; set; }
        internal string _phoneNumber { get; set; }

        public BillingTable(string firstName, string lastName, string address, string city, string postcode, string phoneNumber)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._address = address;
            this._city = city;
            this._postcode = postcode;
            this._phoneNumber = phoneNumber;
        }
    }

}