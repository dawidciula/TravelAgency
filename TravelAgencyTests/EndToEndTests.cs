using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using OpenQA.Selenium.Support;
using Xunit.Abstractions;

namespace UbbRentalBike.Tests
{
    public class ParticipantTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;
        private readonly ITestOutputHelper _output;

        public ParticipantTests(ITestOutputHelper output)
        {
            _driver = new ChromeDriver();
            _baseUrl = "http://localhost:5015/Participant";
            _output = output;
        }

        [Fact]
        public void CanAddParticipant()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            
            var addParticipantLink = _driver.FindElement(By.LinkText("Add Participant"));
            addParticipantLink.Click();

            _driver.FindElement(By.Id("Name")).SendKeys("John");
            _driver.FindElement(By.Id("Surname")).SendKeys("Doe");
            _driver.FindElement(By.Id("DateOfBirth")).SendKeys("1998.07.21");
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("john.doe@example.com");


            
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            
            

            var lastRow = _driver.FindElement(By.CssSelector("table.table tbody tr:last-child"));
            Assert.Contains("John", lastRow.Text);
            Assert.Contains("Doe", lastRow.Text);
            Assert.Contains("21.07.1998", lastRow.Text);
            Assert.Contains("john.doe@example.com", lastRow.Text);
            
            var deleteLink = lastRow.FindElement(By.LinkText("Delete"));
            deleteLink.Click();
            var confirmButton = _driver.FindElement(By.CssSelector("input[type='submit'][value='Delete']"));
            confirmButton.Click();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
