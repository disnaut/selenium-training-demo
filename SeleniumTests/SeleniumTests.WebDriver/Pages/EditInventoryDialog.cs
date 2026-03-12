using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;

namespace SeleniumTests.WebDriver.Pages;

class EditInventoryDialog(DriverManager Manager) : ILoadable<EditInventoryDialog>
{
    #region Web Element Details
    // Name

    // SKU

    // Category

    // Status

    // Location

    // Last Updated

    #endregion

    public EditInventoryDialog Load()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog EnterTextIntoNameInput(string text)
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog ClearTextFromNameInput()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog EnterTExtIntoSKUInput(string text)
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog ClearTextFromSKUInput()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog EnterTextIntoCategoryInput(string text)
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog ClearTextFromCategoryInput()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog EnterNumberIntoQuantityInput(int number)
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog IncrementQuantityInput()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog DecrementQuantityInput()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog ClickStatusDropdown()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog EnterTextIntoLocationInput(string text)
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog ClearTextFromLocationInput()
    {
        throw new NotImplementedException();
    }

    public EditInventoryDialog EnterDateIntoLastUpdatedInput(DateTime date)
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickCancelButton()
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickSaveButton()
    {
        throw new NotImplementedException(
            "What can you do with this when there is the possibility that required fields are empty?"
        );
    }
}
