using OpenQA.Selenium;
using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;

namespace SeleniumTests.WebDriver.Pages;

class JobsPage(DriverManager Manager) : AbstractDemoPage(Manager), ILoadable<JobsPage>
{
    private readonly WebElementDetails startPriceUpdateButton = new(
        By.CssSelector("[data-testid='start-price-update-button']"),
        "Start Price Update Button"
    );

    private readonly WebElementDetails startArchiveButton = new(
        By.CssSelector("[data-testid='start-archive-button']"),
        "Start Archive Button"
    );

    private readonly WebElementDetails startRestockSyncButton = new(
        By.CssSelector("[data-testid='start-restock-sync-button']"),
        "Start Restock Sync Button"
    );

    private readonly WebElementDetails refreshButton = new(
        By.CssSelector("[data-testid='refresh-jobs-button']"),
        "Refresh Jobs Button"
    );
    private readonly WebElementDetails jobQueueLoadingContainer = new(
        By.CssSelector("[data-testid='job-queue-loading']"),
        "Job Queue Loading Container"
    );

    private readonly WebElementDetails jobQueueTable = new(
        By.CssSelector("[data-testid='job-queue-table']"),
        "Job Queue Table"
    );

    public JobsPage Load()
    {
        base.Load();
        Manager
            .Load(startPriceUpdateButton)
            .Load(startArchiveButton)
            .Load(startRestockSyncButton)
            .Load(refreshButton)
            .Unload(jobQueueLoadingContainer);
        return this;
    }
}
