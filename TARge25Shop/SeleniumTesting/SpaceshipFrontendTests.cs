using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SeleniumTesting
{
    public class SpaceshipFrontendTests
    {
        [Fact]
        public void Should_NavigateToCreate_AddSpaceShip_WithCoreData_ReturnToIndex()
        {
            IWebDriver = new FirefoxDriver();
            DriverCommand.Url = "https://localhost:7227";
            IWebElement createInIndex = driver.FindElement(By.Id("CreateInIndex"));
        }
    }
}
