using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;
using SeleniumTests.WebDriver.Structs;

namespace SeleniumTests.WebDriver.Pages;

class DashboardPage : AbstractDemoPage, ILoadable<DashboardPage>
{
    public DashboardPage(DriverManager Manager)
        : base(Manager) { }

    public DashboardPage Load()
    {
        base.Load();
        return this;
    }

    public InventoryPage ClickGoToInventoryButton()
    {
        throw new NotImplementedException();
    }

    public JobsPage ClickViewJobsButton()
    {
        throw new NotImplementedException();
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
