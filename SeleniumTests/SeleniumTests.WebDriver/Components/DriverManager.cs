using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.WebDriver.Enums;

namespace SeleniumTests.WebDriver.Components;

sealed class DriverManager
{
    public IWebDriver Driver;

    public readonly WebDriverWait Wait;
    private readonly string logFilePath;
    private readonly string resultPath = "./TestResults";
    private readonly string logsPath = string.Empty;

    public DriverManager(IWebDriver driver, int timeout, DriverType type)
    {
        Driver = driver;
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));

        if (!Directory.Exists(resultPath))
            Directory.CreateDirectory(resultPath);

        if (!Directory.Exists($"{resultPath}/Logs/{type.ToString()}"))
            Directory.CreateDirectory($"{resultPath}/Logs/{type.ToString()}");

        logsPath =
            $"{resultPath}/Logs/{type.ToString()}//{type.ToString().ToLower()}_{DateTime.Now:yyyyMMdd_HHmmss}.log";
    }

    public DriverManager Load(WebElementDetails elementDetails)
    {
        Log($"Attempting to load element: {elementDetails.Name}");
        try
        {
            Wait.Until(ExpectedConditions.ElementExists(elementDetails.Locator));
            return this;
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception(
                $"Failed to load element: {elementDetails.Name} after {Wait.Timeout.TotalSeconds} seconds."
            );
        }
    }

    public DriverManager Unload(WebElementDetails elementDetails)
    {
        Log($"Attempting to unload element: {elementDetails.Name}");
        try
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(elementDetails.Locator));
            return this;
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception(
                $"Failed to unload element: {elementDetails.Name} after {Wait.Timeout.TotalSeconds} seconds."
            );
        }
    }

    public DriverManager GoTo(string url)
    {
        Log($"Attempting to navigate to URL: {url}");
        try
        {
            Driver.Navigate().GoToUrl(url);
            Wait.Until(ExpectedConditions.UrlToBe(url));
            return this;
        }
        catch (WebDriverException)
        {
            throw new Exception($"Failed to navigate to URL: {url}");
        }
    }

    public bool IsDisplayed(WebElementDetails elementDetails)
    {
        try
        {
            return Driver.FindElement(elementDetails.Locator).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    public DriverManager Click(WebElementDetails elementDetails)
    {
        Log($"Attempting to click element: {elementDetails.Name}");
        try
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(elementDetails.Locator)).Click();
            return this;
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception(
                $"Element not clickable: {elementDetails.Name} after {Wait.Timeout.TotalSeconds} seconds."
            );
        }
        catch (WebDriverException)
        {
            throw new Exception($"Failed to click element: {elementDetails.Name}");
        }
    }

    public DriverManager TypeIntoTextbox(
        WebElementDetails elementDetails,
        string text,
        bool secret = false
    )
    {
        if (!secret)
            Log($"Attempting to type into textbox: {elementDetails.Name} with text: {text}");
        try
        {
            var element = Wait.Until(
                ExpectedConditions.ElementToBeClickable(elementDetails.Locator)
            );
            element.SendKeys(text);
            return this;
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception(
                $"Textbox not visible: {elementDetails.Name} after {Wait.Timeout.TotalSeconds} seconds."
            );
        }
        catch (WebDriverException)
        {
            throw new Exception($"Failed to type into textbox: {elementDetails.Name}");
        }
    }

    public DriverManager ClearTextBox(WebElementDetails elementDetails)
    {
        Log($"Attempting to clear textbox: {elementDetails.Name}");
        try
        {
            var element = Wait.Until(ExpectedConditions.ElementIsVisible(elementDetails.Locator));
            element.Clear();
            return this;
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception(
                $"Textbox not visible: {elementDetails.Name} after {Wait.Timeout.TotalSeconds} seconds."
            );
        }
        catch (WebDriverException)
        {
            throw new Exception($"Failed to clear textbox: {elementDetails.Name}");
        }
    }

    public DriverManager ScrollTo(WebElementDetails elementDetails)
    {
        Log($"Attempting to scroll to element: {elementDetails.Name}");
        try
        {
            var element = Wait.Until(ExpectedConditions.ElementIsVisible(elementDetails.Locator));
            ((IJavaScriptExecutor)Driver).ExecuteScript(
                "arguments[0].scrollIntoView(true);",
                element
            );
            Wait.Until(ExpectedConditions.ElementIsVisible(elementDetails.Locator));
            return this;
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception(
                $"Element not visible: {elementDetails.Name} after {Wait.Timeout.TotalSeconds} seconds."
            );
        }
        catch (WebDriverException)
        {
            throw new Exception($"Failed to scroll to element: {elementDetails.Name}");
        }
    }

    public DriverManager Log(string msg)
    {
        File.AppendAllText(logsPath, $"{DateTime.Now}: {msg}{Environment.NewLine}");
        return this;
    }
}
