using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Pages;

namespace SeleniumTests.WebDriver.Tests;

[TestFixture(DriverType.Chrome)]
[TestFixture(DriverType.Edge)]
class DashboardPageTests(DriverType driverType) : AbstractSeleniumTest(driverType)
{
    LoginPage page;

    [SetUp]
    public void Setup()
    {
        Manager.Log($"Beginning '{TestContext.CurrentContext.Test.Name}'");
        int attemptsRemaining = 2;

        bool loaded = false;
        page = new(Manager);

        while (attemptsRemaining > 0 && !loaded)
        {
            try
            {
                Manager.Log($"Attempting to get {page}. Attempts remaining: {attemptsRemaining}");
                page.GoTo();
                loaded = true;
            }
            catch (Exception ex)
            {
                Manager.Log(
                    $"Failed to load page. Attempts remaining: {attemptsRemaining - 1}. Exception: {ex.Message}"
                );
                attemptsRemaining--;
            }
        }
    }

    [Test]
    public void DashboardQuickActionButtonsWork()
    {
        page.EnterUsername("testuser").EnterPassword("password123");

        var dashboardPage = page.ClickSignIn();

        dashboardPage.ClickGoToInventoryButton();
        Assert.That(
            Manager.Driver.Url,
            Does.Contain("inventory"),
            "Clicking 'Go To Inventory' did not navigate to the inventory page"
        );
        Manager.Driver.Navigate().Back();
        dashboardPage.ClickViewJobsButton();
        Assert.That(
            Manager.Driver.Url,
            Does.Contain("jobs"),
            "Clicking 'View Jobs' did not navigate to the jobs page"
        );
    }
}
