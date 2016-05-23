using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    class WebsiteSeleniumTests
    {
        private const string c_BaseUrl = "https://farmplan.co.uk";

        private static IWebDriver CreateChromeDriver(string baseUrl)
        {
            IWebDriver chromeDriver = new ChromeDriver();          
            chromeDriver.Url = baseUrl;

            return chromeDriver;
        }

        [Test]
        public void Website_has_correct_title()
        {
            var driver = CreateChromeDriver(c_BaseUrl);
            const string expectedTitle = "Farmplan - Market-leading farm software and IT solutions.";
            Assert.AreEqual(expectedTitle, driver.Title);
        }

        [Test]
        public void Request_callback_pops_form()
        {
            var driver = CreateChromeDriver(c_BaseUrl);

            var requestCallBack = driver.FindElement(
                //XPath for 'Request call back' button
                By.XPath("/ html / body / div[4] / header / div[2] / div / div / p[1] / a"));

            requestCallBack.Click();

            Assert.IsNotNull(driver.FindElement(By.Name("gform_unique_id")));
        }

        [Test]
        public void Request_call_back_form_contains_Correct_fields()
        {
            var driver = CreateChromeDriver(c_BaseUrl);

            var requestCallBack = driver.FindElement(
                //XPath for 'Request call back' button
                By.XPath("/ html / body / div[4] / header / div[2] / div / div / p[1] / a"));

            requestCallBack.Click();

            var form = driver.FindElement(By.XPath("//*[starts-with(@id,'gform_')]"));

            Assert.IsNotNull(form.FindElement(By.ClassName("email")));
            Assert.IsNotNull(form.FindElement(By.ClassName("title")));
            Assert.IsNotNull(form.FindElement(By.ClassName("first-name")));
            Assert.IsNotNull(form.FindElement(By.ClassName("last-name")));
            Assert.IsNotNull(form.FindElement(By.ClassName("farm")));
            //etc, etc
        }

        [Test]
        public void Request_a_call_back_form_requires_email()
        {
            var driver = CreateChromeDriver(c_BaseUrl);

            var requestCallBack = driver.FindElement(
                //XPath for 'Request call back' button
                By.XPath("/ html / body / div[4] / header / div[2] / div / div / p[1] / a"));

            requestCallBack.Click();

            var form = driver.FindElement(By.XPath("//*[starts-with(@id,'gform_')]"));

            var email = form.FindElement(By.XPath(@"//*[@name=""input_1""]"));
            email.SendKeys(Keys.Enter);

            var expectedValidationMessage = "This field is required.";
            var actualValidationMessage = form.FindElement(By.ClassName("validation_message")).Text;

            Assert.AreEqual(expectedValidationMessage,actualValidationMessage);
        }

        [Test]
        public void Privacy_settings_are_submitted()
        {
            var driver = CreateChromeDriver(c_BaseUrl);

            var requestCallBack = driver.FindElement(
                //XPath for 'Request call back' button
                By.XPath("/ html / body / div[4] / header / div[2] / div / div / p[1] / a"));

            requestCallBack.Click();

            var form = driver.FindElement(By.XPath("//*[starts-with(@id,'gform_')]"));

            form.FindElement(By.XPath(@"//*[@name=""input_1""]")).SendKeys("a@b.com");
            form.FindElement(By.XPath(@"//*[@name=""input_1_2""]")).SendKeys("a@b.com");
            form.FindElement(By.XPath(@"//*[@name=""input_92""]")).SendKeys("a");
            form.FindElement(By.XPath(@"//*[@name=""input_14""]")).SendKeys("a");
            form.FindElement(By.XPath(@"//*[@name=""input_15""]")).SendKeys("a");
            form.FindElement(By.XPath(@"//*[@name=""input_4""]")).SendKeys("a");
            form.FindElement(By.XPath(@"//*[@name=""input_16""]")).SendKeys("a");
            form.FindElement(By.XPath(@"//*[@name=""input_18""]")).SendKeys("a");
            form.FindElement(By.XPath(@"//*[@name=""input_19""]")).SendKeys("a");
            form.FindElement(By.XPath(@"//*[@name=""input_6""]")).SendKeys("a");

            form.FindElement(By.XPath(@"//*[@class=""gchoice_12_90_1""]")).Click();
            form.FindElement(By.XPath(@"//*[@class=""gchoice_12_91_1""]")).Click();

            //form.Submit();

            //Assert something here - not sure if I can assert something in the outgoing request, 
            // or verify on the webserver
        }
    }
}
