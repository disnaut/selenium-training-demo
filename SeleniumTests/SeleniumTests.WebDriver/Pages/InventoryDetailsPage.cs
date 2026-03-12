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
        base.Load();
        throw new NotImplementedException();
    }

    public InventoryDetailsPage ClickInspectionNotesDropdown()
    {
        throw new NotImplementedException();
    }

    public string GetInspectionNotes()
    {
        throw new NotImplementedException();
    }

    public InventoryDetailsPage ClickInventoryHIstoryDropdown()
    {
        throw new NotImplementedException();
    }

    public string GetInventoryHistory()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog ClickEditButton()
    {
        throw new NotImplementedException();
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }

    public string GetSKU()
    {
        throw new NotImplementedException();
    }

    public string GetCategory()
    {
        throw new NotImplementedException();
    }

    public string GetQuantity()
    {
        throw new NotImplementedException();
    }

    public string GetLocation()
    {
        throw new NotImplementedException();
    }

    public string GetLastUpdated()
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickBackToInventory()
    {
        throw new NotImplementedException();
    }
}
