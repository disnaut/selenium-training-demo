using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;

namespace SeleniumTests.WebDriver.Pages;

class JobsPage : AbstractDemoPage, ILoadable<JobsPage>
{
    public JobsPage(DriverManager Manager)
        : base(Manager) { }

    public JobsPage Load()
    {
        base.Load();
        return this;
    }
}
