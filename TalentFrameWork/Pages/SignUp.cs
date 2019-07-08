using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TalentFrameWork.Global;
using static TalentFrameWork.Global.Definition;


namespace TalentFrameWork.Pages
{
    class SignUp
    {
        #region Define Xpath

        //Find Xpath for SignUp button
        private IWebElement signupbtn => Definition.driver.FindElement(By.XPath("//*[@id='home']/div/div/div/div[1]/div/button"));

        //Find Xpath for First Name
        private IWebElement firstname => Definition.driver.FindElement(By.Name("firstName"));

        //Find Xpath for Last Name
        private IWebElement lastname => Global.Definition.driver.FindElement(By.Name("lastName"));

        //Find Xpath for Email Address 
        private IWebElement email => Global.Definition.driver.FindElement(By.Name("email"));

        //Find Xpath for Password 
        private IWebElement password => Global.Definition.driver.FindElement(By.Name("password"));

        //Find xpath for "SignUp As" talent radio button
        private IWebElement talentradiobtn => Global.Definition.driver.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[5]/div/div/div[1]/div/input"));

        //Find Xpath for IAgree checkbox button
        private IWebElement iagree => Global.Definition.driver.FindElement(By.Name("terms"));

        //Find Xpath for join button
        private IWebElement join => Global.Definition.driver.FindElement(By.XPath("//*[@id='submit-btn']"));

        #endregion

        public void Register()
        {
            //Populate Excel Sheet
            Global.Definition.ExcelOperations.PopulateInCollection(Global.Definition.ReadJson().ExcelPath,"SignUp");
            Thread.Sleep(500);

            //Enter URL
            Definition.driver.Navigate().GoToUrl(Definition.ExcelOperations.ReadData(1, "URL"));
            Thread.Sleep(500);

            //Click on SignUp Button
            signupbtn.Click();
            Thread.Sleep(500);

            //Enter  First Name
            firstname.SendKeys(Definition.ExcelOperations.ReadData(1, "FirstName"));
            Thread.Sleep(500);

            //Enter Last Name
            lastname.SendKeys(Definition.ExcelOperations.ReadData(1, "LastName"));
            Thread.Sleep(500);

            //Enter Email Address
            email.SendKeys(Definition.ExcelOperations.ReadData(1, "Email"));
            Thread.Sleep(500);

            //Enter Password
            password.SendKeys(Definition.ExcelOperations.ReadData(1, "Password"));
            Thread.Sleep(500);

            // Click on talent radio button
            talentradiobtn.Click();
            Thread.Sleep(500);

            //Click on I Agree Checkbox button
            iagree.Click();

            //Click on join button
            join.Click();
            Thread.Sleep(500);

           //Capture ScreenShot
             SaveScreenShotClass.SaveScreenshot(Definition.driver, "SIGN UP PAGE");
            Thread.Sleep(1000);
         
        }
    }
}
