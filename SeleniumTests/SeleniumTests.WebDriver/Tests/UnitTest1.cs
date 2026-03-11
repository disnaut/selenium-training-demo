using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Tests;

namespace SeleniumTests.WebDriver;

[TestFixture(DriverType.Chrome)]
[TestFixture(DriverType.Edge)]
class UnitTest1(DriverType driverType) : AbstractSeleniumTest(driverType)
{
    const string BaseUrl = "http://localhost:4200";
    private IWebDriver Driver;
    private WebDriverWait Wait;

    [SetUp]
    public void Setup()
    {
        Driver = new ChromeDriver();
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }

    [Test]
    public void LoginSuccessfully()
    {
        Driver.Navigate().GoToUrl(BaseUrl);
        Wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("[data-testid='login-username-input']")
                )
            )
            .SendKeys("testuser");
        Wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("[data-testid='login-password-input']")
                )
            )
            .SendKeys("password123");
        Wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("[data-testid='login-submit-button']")
                )
            )
            .Click();

        Wait.Until(
            ExpectedConditions.InvisibilityOfElementLocated(
                By.CssSelector("[data-testid='login-loading-overlay']")
            )
        );

        Thread.Sleep(5000);
    }

    [TearDown]
    public void Teardown()
    {
        Driver.Quit();
        Driver.Dispose();
    }
}
