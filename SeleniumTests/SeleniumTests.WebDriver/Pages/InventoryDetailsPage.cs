using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;

namespace SeleniumTests.WebDriver.Pages;

class InventoryDetailsPage(DriverManager Manager)
    : AbstractDemoPage(Manager),
        ILoadable<InventoryDetailsPage>
{
    #region Web Element Details
    #endregion

    public InventoryDetailsPage Load()
    {
        throw new NotImplementedException();
    }

    
}
