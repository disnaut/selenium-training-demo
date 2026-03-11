using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;

namespace SeleniumTests.WebDriver.Pages.Abstract;

class DashboardPage : AbstractDemoPage, ILoadable<DashboardPage>
{
    public DashboardPage(DriverManager Manager)
        : base(Manager) { }

    public DashboardPage Load()
    {
        base.Load();
        return this;
    }
}
