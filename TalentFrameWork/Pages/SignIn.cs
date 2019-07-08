using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TalentFrameWork.Global;
using static TalentFrameWork.Global.Definition;

namespace TalentFrameWork.Pages
{
    class SignIn
    {
        #region WebElements
        //Get Email Address
        private IWebElement Email => Definition.driver.FindElement(By.Id("email"));

        //Get the Password
        private IWebElement Password => Definition.driver.FindElement(By.Id("password"));

        //Xpath for Login button
        private IWebElement LoginBtn => Definition.driver.FindElement(By.XPath("//button[@type='submit']"));

        //Xpath for RememberMe checkbox
        private IWebElement RememberMe => Definition.driver.FindElement(By.XPath("//input[@type='checkbox']"));

        //Xpath for forgotPassword
        private IWebElement ForgotPwd => Definition.driver.FindElement(By.XPath("//a[@href]/span[text()='Forgot your password?']"));

        //XPath for tab LoginAsTalent
        private IWebElement LoginAsTalent => Definition.driver.FindElement(By.XPath("//div[@role = 'tab' and text() = 'Login as Talent']"));

        //XPath for tab LoginAsEmployer
        private IWebElement LoginAsEmployer => Definition.driver.FindElement(By.XPath("//div[@role = 'tab' and text() = 'Login as Employer']"));
        #endregion

        #region Login
        public void LoginSteps()
        {
            //Populate Excel sheet for SignIn page
            Definition.ExcelOperations.PopulateInCollection(Definition.ReadJson().ExcelPath, "SignIn");
            Thread.Sleep(2000);

            //Navigae to the Url
            Definition.driver.Navigate().GoToUrl(Definition.ExcelOperations.ReadData(2, "URL"));
            Thread.Sleep(5000);

            //Enter Email address 
            Email.SendKeys(Definition.ExcelOperations.ReadData(2, "Email"));
            Thread.Sleep(2000);

            //Enter Password - get from the excel 
            Password.SendKeys(Definition.ExcelOperations.ReadData(2, "Password"));
            Thread.Sleep(2000);
            
            //Click on Login button
            LoginBtn.Click();
            Thread.Sleep(5000);

            //Capture Screenshot
            SaveScreenShotClass.SaveScreenshot(Definition.driver, "LOGIN");

        }

        #endregion
    }
}
