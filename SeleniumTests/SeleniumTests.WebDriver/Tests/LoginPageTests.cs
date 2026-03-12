using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Pages;

namespace SeleniumTests.WebDriver.Tests;

[TestFixture(DriverType.Chrome)]
[TestFixture(DriverType.Edge)]
class LoginPageTests(DriverType driverType) : AbstractSeleniumTest(driverType)
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
    }

    [Test]
    public void EnterUserNameOnly()
    {
        page.EnterUsername("testuser").ClickSignInExpectingErrors();
        Assert.That(page.VerifyPasswordRequiredErrorMessage());
    }

    [Test]
    public void EnterPasswordOnly()
    {
        page.EnterPassword("password123").ClickSignInExpectingErrors();
        Assert.That(page.VerifyUsernameRequiredErrorMessage());
    }

    [Test]
    public void ClickSignInWithNoCredentials()
    {
        page.ClickSignInExpectingErrors();
        Assert.Multiple(() =>
        {
            Assert.That(page.VerifyUsernameRequiredErrorMessage());
            Assert.That(page.VerifyPasswordRequiredErrorMessage());
        });
    }
}
