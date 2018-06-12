using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;           
using OpenQA.Selenium.Firefox;   
using OpenQA.Selenium.Chrome;    
using OpenQA.Selenium.IE;

namespace SeleniumTests
{
    [TestClass]
    public class MySeleniumTests
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;

        public MySeleniumTests()
        {
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void ChomeLoginLogOffTest()
        {
            driver.Navigate().GoToUrl(appURL + "/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("flavio_tosta@qatbrasil.com");
            driver.FindElement(By.Id("Password")).SendKeys("Aa123456$");
            driver.FindElement(By.Id("Password")).Submit();

            // Verify logged user name
            var welcomeMessage = driver.FindElement(By.CssSelector("#logoutForm > ul > li:nth-child(1) > a")).Text.Trim();

            Assert.AreEqual(welcomeMessage, "Hello flavio_tosta@qatbrasil!");

            // Log Off
            driver.FindElement(By.CssSelector("#logoutForm > ul > li:nth-child(2) > a")).Click();

            // Verify register button is shown
            var registerButtons = driver.FindElements(By.CssSelector("#registerLink"));

            Assert.AreEqual(registerButtons.Count, 1);
        }
        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest()
        {
            appURL = "http://vitsalistics-env.skaz2bsukr.us-east-2.elasticbeanstalk.com/";

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"));
                    break;
                case "Firefox":
                    driver = new FirefoxDriver(Environment.GetEnvironmentVariable("GeckoWebDriver"));
                    break;
                case "IE":
                    driver = new InternetExplorerDriver(Environment.GetEnvironmentVariable("IEWebDriver"));
                    break;
                default:
                    driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"));
                    break;
            }
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
