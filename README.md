Overall Objectives
Develop two end-to-end test that uses SpecFlow and WebDriver.

Test Case 1
This test will login to an e-commerce site as a registered user, purchase an item of clothing, apply a 
discount code and check that the total is correct after the discount & shipping is applied. 

Test Case 2
This test will login to an e-commerce site as a registered user, purchase an item of clothing and go 
through checkout. It will capture the order number and check the order is also present in the ‘My 
Orders’ section of the site.

To generate a livingdoc, first run the tests and then on command prompt, do 'cd bin/Debug/net6.0' and then execute the following 'livingdoc test-assembly uk.co.nfocus.fathima.project.dll -t TestExecution.json'
