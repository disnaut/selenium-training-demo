using OpenQA.Selenium;
using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;

namespace SeleniumTests.WebDriver.Pages;

class LoginPage(DriverManager Manager) : ILoadable<LoginPage>
{
    const string Url = "http://localhost:4200";
    private readonly WebElementDetails usernameInput = new(
        By.CssSelector("[data-testid='login-username-input']"),
        "Username Input"
    );

    private readonly WebElementDetails usernameRequiredErrorMessage = new(
        By.XPath(
            "//input[@data-testid='login-username-input']/ancestor::mat-form-field//mat-error"
        ),
        "Username Required Error Message"
    );

    private readonly WebElementDetails passwordInput = new(
        By.CssSelector("[data-testid='login-password-input']"),
        "Password Input"
    );

    private readonly WebElementDetails passwordRequiredErrorMessage = new(
        By.XPath(
            "//input[@data-testid='login-password-input']/ancestor::mat-form-field//mat-error"
        ),
        "Password Required Error Message"
    );

    private readonly WebElementDetails submitButton = new(
        By.CssSelector("[data-testid='login-submit-button']"),
        "Submit Button"
    );

    private readonly WebElementDetails loadingOverlay = new(
        By.CssSelector("[data-testid='login-loading-overlay']"),
        "Loading Overlay"
    );

    private readonly WebElementDetails errorMessage = new(
        By.CssSelector("[data-testid='login-error-message']"),
        "Error Message"
    );

    private readonly WebElementDetails title = new(By.TagName("mat-card-title"), "Login Title");
    private readonly WebElementDetails subTitle = new(By.TagName("mat-card-subtitle"), "Sub Title");

    public LoginPage Load()
    {
        Manager
            .Load(title)
            .Load(subTitle)
            .Load(usernameInput)
            .Load(passwordInput)
            .Load(submitButton);
        return this;
    }

    public LoginPage GoTo()
    {
        Manager.GoTo(Url);
        return Load();
    }

    public LoginPage EnterUsername(string username)
    {
        Manager.TypeIntoTextbox(usernameInput, username);
        return this;
    }

    public LoginPage EnterPassword(string password)
    {
        Manager.TypeIntoTextbox(passwordInput, password, true);
        return this;
    }

    public DashboardPage ClickSignIn()
    {
        Manager.Click(submitButton);
        Manager.Unload(loadingOverlay);
        return new DashboardPage(Manager).Load();
    }

    public LoginPage ClickSignInExpectingErrors()
    {
        Manager.Click(submitButton);
        return Load();
    }

    public bool VerifyUsernameRequiredErrorMessage()
    {
        return Manager.Load(usernameRequiredErrorMessage).IsDisplayed(usernameRequiredErrorMessage);
    }

    public bool VerifyPasswordRequiredErrorMessage()
    {
        return Manager.Load(passwordRequiredErrorMessage).IsDisplayed(passwordRequiredErrorMessage);
    }

    public string GetLoginErrorMessage()
    {
        return Manager.Load(errorMessage).GetTextFromWebElement(errorMessage);
    }
}
