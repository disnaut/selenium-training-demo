using OpenQA.Selenium;
using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;

namespace SeleniumTests.WebDriver.Pages;

class LoginPage(DriverManager Manager) : ILoadable<LoginPage>
{
    const string Url = "http://localhost:4200";
    private readonly WebElementDetails usernameInput = new(
        By.CssSelector("[data-testid='login-username-input']"),
        "Username Input"
    );

    private readonly WebElementDetails passwordInput = new(
        By.CssSelector("[data-testid='login-password-input']"),
        "Password Input"
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
}
