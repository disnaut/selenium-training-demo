using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Enums;

namespace SeleniumTests.WebDriver.Tests;

abstract class AbstractSeleniumTest
{
    public DriverManager Manager;
    private readonly DriverType browser = DriverType.Chrome;

    public AbstractSeleniumTest(DriverType driverType)
    {
        int timeout = int.Parse(TestContext.Parameters["timeout"] ?? "10");
        bool headless = bool.Parse(TestContext.Parameters["headless"] ?? "false");

        IWebDriver driver;
        ChromiumOptions options =
            driverType == DriverType.Chrome ? new ChromeOptions() : new EdgeOptions();

        options.AcceptInsecureCertificates = true;

        if (headless)
        {
            options.AddArgument("--headless");
        }

        options.AddArgument("--disable-web-security");
        options.AddArgument("--disable-features=CrossSiteDocumentBlockingIfIsolating");
        driver =
            driverType == DriverType.Chrome
                ? new ChromeDriver(options as ChromeOptions)
                : new EdgeDriver(options as EdgeOptions);

        browser = driverType;
        Manager = new DriverManager(driver, timeout, driverType);
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Manager.Log($"Beginning Test Run for {browser.ToString()}.");
        Manager.Driver.Manage().Window.Maximize();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Manager.Driver.Close();
        Manager.Driver.Quit();
        Manager.Log($"Ending Test Run for {browser.ToString()}.");
    }

    [TearDown]
    public void TearDown()
    {
        Manager.Log(
            $"Test '{TestContext.CurrentContext.Test.Name}' completed with status: {TestContext.CurrentContext.Result.Outcome.Status}"
        );
    }
}
