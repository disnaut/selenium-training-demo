using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Pages;

namespace SeleniumTests.WebDriver.Tests;

class DashboardPageTests(DriverType driverType) : AbstractSeleniumTest(driverType)
{
    DashboardPage page;

    [SetUp]
    public void Setup()
    {
        Manager.Log($"Beginning '{TestContext.CurrentContext.Test.Name}'");
        int attemptsRemaining = 2;

        bool loaded = false;
        var startPage = new LoginPage(Manager);

        while (attemptsRemaining > 0 && !loaded)
        {
            try
            {
                Manager.Log($"Attempting to get {page}. Attempts remaining: {attemptsRemaining}");
                page = startPage
                    .GoTo()
                    .EnterUsername("testuser")
                    .EnterPassword("password123")
                    .ClickSignIn();
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
    public void DashboardQuickActionButtonsWork() { }
}
