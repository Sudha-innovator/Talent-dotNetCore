using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TalentFrameWork.Global;
using TalentFrameWork.Pages;

namespace TalentFrameWork.Test
{
    [TestFixture]
    class Program
    {
        [Test]
        public void SignInPage()
        {
            using (Definition.driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
               Definition.driver.Manage().Window.Maximize();
                SignIn newSignIn = new SignIn();
                newSignIn.LoginSteps();
            }
        }
        [Test]
        public void SignUpPage()
        {
            using (Definition.driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {

                Definition.driver.Manage().Window.Maximize();
                SignUp newSignUp = new SignUp();
                newSignUp.Register();

            }
        }
        
       
    }
}