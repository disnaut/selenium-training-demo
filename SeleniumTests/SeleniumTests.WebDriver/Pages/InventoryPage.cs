using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;

namespace SeleniumTests.WebDriver.Pages;

class InventoryPage : AbstractDemoPage, ILoadable<InventoryPage>
{
    public InventoryPage(DriverManager Manager)
        : base(Manager) { }

    public InventoryPage Load()
    {
        base.Load();
        return this;
    }
}
