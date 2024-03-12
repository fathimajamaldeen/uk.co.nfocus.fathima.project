READEME: Executing the Test Repositry

The overall objectives was to develop two end-to-end test that uses SpecFlow and WebDriver.

Test Case Details 
Test Case 1 - This test will login to an e-commerce site as a registered user, purchase an item of clothing, apply a discount code and check that the total is correct after the discount & shipping is applied. 
Test Case 2 -This test will login to an e-commerce site as a registered user, purchase an item of clothing and go through checkout. It will capture the order number and check the order is also present in the ‘My Orders’ section of the site.

Prerequisites
* Visual Studio with the following extenstions installed via NuGet Pacakge Manager:
    * ExtentReports.Core
    * FluentAssertions
    * Microsoft.NET.Test.Sdk
    * NUnit
    * NUnit3TestAdapter
    * NunitXml.TestLogger
    * Selenium.Support
    * Selenium.WebDriver
    * SpecFlow.NUnit
    * SpecFlow.Plus.LivingDocPlugin
* Git installed on your machine

To get started
1. Clone the repositry to your local machine using Git:
     git clone https://github.com/fathimajamaldeen/uk.co.nfocus.fathima.project.git
2. Open the cloned repositry in Visual Studio
3. Buil the solectuion to ensure all dependencies are resolved

Executing Tests
1. Create a '.runsettings' file with the following format and fill in the ___ with your login details for the website:
        <?xml version="1.0" encoding="utf-8" ?>
      <RunSettings>
      	<!-- configuration elements -->
      	<RunConfiguration>
      		<EnvironmentVariables>
      			<BROWSER>edge</BROWSER>
      		</EnvironmentVariables>
      	</RunConfiguration>
      	<TestRunParameters>
      		<!--NUnit config parameters as an alternative-->
      		<Parameter name="WebAppURL" value="https://www.edgewordstraining.co.uk/demo-site"/>
      		<Parameter name="WebAppUsername" value="____"/>
      		<Parameter name="WebAppPassword" value="____"/>
      	</TestRunParameters>
      </RunSettings>
2. In Visual Studio, go to the Test Explorer window
3. Click the 'Settings' icon (gear icon) and select 'Configure Run Settings' then click 'Select Solution Wide runsettings File' and add the .runsetting file you have created
4. Now, in Test Explorer, select the tests you wanted to execute
5. Right-click on the selected test and choose 'Run'
     Note: To run all the tests click the 'Run All Tests In View' icon (double arrow icon)

View Testing Report
* Go into the 'Reports' folder that was generated and open the 'Results_'(timeoftest) folder generated
* Click on 'dashboard.html' to view the overall dashboard for the tests generated
* Click on 'index.html' to see each tests individual report



