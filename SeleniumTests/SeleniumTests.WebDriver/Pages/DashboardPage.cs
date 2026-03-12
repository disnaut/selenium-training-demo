using OpenQA.Selenium;
using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;
using SeleniumTests.WebDriver.Structs;

namespace SeleniumTests.WebDriver.Pages;

class DashboardPage(DriverManager Manager) : AbstractDemoPage(Manager), ILoadable<DashboardPage>
{
    private readonly WebElementDetails goToInventoryButton = new(
        By.CssSelector("[data-testid='dashboard-go-to-inventory-button']"),
        //By.XPath("//button[contains(., 'Go To Inventory')]"),
        "Go To Inventory Button"
    );

    private readonly WebElementDetails viewJobsButton = new(
        By.CssSelector("[data-testid='dashboard-view-jobs-button']"),
        //By.XPath("//button[contains(., 'View Jobs')]"),
        "View Jobs Button"
    );

    public DashboardPage Load()
    {
        base.Load();
        Manager.Load(goToInventoryButton).Load(viewJobsButton);
        return this;
    }

    public InventoryPage ClickGoToInventoryButton()
    {
        Manager.Click(goToInventoryButton);
        return new InventoryPage(Manager).Load();
    }

    public JobsPage ClickViewJobsButton()
    {
        Manager.Click(viewJobsButton);
        return new JobsPage(Manager).Load();
    }

    public DashboardPage VerifyOpenInventoryStatsIsDisplayed()
    {
        throw new NotImplementedException();
    }

    public DashboardPage VerifyPendingReviewsStatIsDisplayed()
    {
        throw new NotImplementedException();
    }

    public DashboardPage VerifyQueuedJobsStatIsDisplayed()
    {
        throw new NotImplementedException();
    }

    public DashboardPage VerifyRecentActivityEntrysAreDisplayed(List<RecentActivity> entriesToCheck)
    {
        throw new NotImplementedException();
    }
}
