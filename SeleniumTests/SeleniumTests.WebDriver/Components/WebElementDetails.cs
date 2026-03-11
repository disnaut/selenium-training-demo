using OpenQA.Selenium;

namespace SeleniumTests.WebDriver.Components;

public class WebElementDetails(By locator, string name)
{
    public By Locator { get; set; } = locator;
    public string Name { get; set; } = name;
}
