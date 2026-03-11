using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Pages;
using SeleniumTests.WebDriver.Tests;

namespace SeleniumTests.WebDriver;

[TestFixture(DriverType.Chrome)]
[TestFixture(DriverType.Edge)]
class UnitTest1(DriverType driverType) : AbstractSeleniumTest(driverType)
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
    public void LoginSuccessfully()
    {
        page.EnterUsername("testuser").EnterPassword("password123");
        Thread.Sleep(5000);
    }
}
