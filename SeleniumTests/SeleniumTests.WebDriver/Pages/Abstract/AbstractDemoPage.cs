using OpenQA.Selenium;
using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;

namespace SeleniumTests.WebDriver.Pages.Abstract;

abstract class AbstractDemoPage(DriverManager Manager) : ILoadable<AbstractDemoPage>
{
    private readonly WebElementDetails LayoutBrand = new(
        By.XPath("//div[contains(@class, 'layout-brand')]"),
        "Layout Brand Title"
    );

    private readonly WebElementDetails DashBoardNavItem = new(
        By.XPath("//nav//a[contains(@href, 'dashboard')]"),
        "Dashboard Navigation Item"
    );

    private readonly WebElementDetails InventoryNavItem = new(
        By.XPath("//nav//a[contains(@href, 'inventory')]"),
        "Inventory Navigation Item"
    );
    private readonly WebElementDetails JobsNavItem = new(
        By.XPath("//nav//a[contains(@href, 'jobs')]"),
        "Jobs Navigation Item"
    );
    private readonly WebElementDetails ToolBarTitle = new(By.Id("toolbar-title"), "Toolbar Title");
    private readonly WebElementDetails LogoutButton = new(By.Id("logout-btn"), "Logout Button");
    private readonly WebElementDetails SideNav = new(
        By.TagName("mat-sidenav"),
        "Side Navigation Bar"
    );

    public AbstractDemoPage Load()
    {
        Manager
            .Load(SideNav)
            .Load(LayoutBrand)
            .Load(DashBoardNavItem)
            .Load(InventoryNavItem)
            .Load(JobsNavItem)
            .Load(ToolBarTitle)
            .Load(LogoutButton);
        return this;
    }

    public LoginPage Logout()
    {
        Manager.Click(LogoutButton);
        return new LoginPage(Manager).Load();
    }
}
