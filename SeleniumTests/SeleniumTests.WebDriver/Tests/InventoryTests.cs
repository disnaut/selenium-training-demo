using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Pages;

namespace SeleniumTests.WebDriver.Tests;

enum SortOrder
{
    ASC,
    DESC,
    NONE,
}

[TestFixture(DriverType.Chrome)]
[TestFixture(DriverType.Edge)]
class InventoryTests(DriverType driverType) : AbstractSeleniumTest(driverType)
{
    // Our landing page is the login page no matter what test we are on.
    LoginPage page;

    [SetUp]
    public void Setup()
    {
        Manager.Log($"Beginning '{TestContext.CurrentContext.Test.Name}'");
        int attemptsRemaining = 2;

        bool loaded = false;
        page = new(Manager);

        while (attemptsRemaining > 0 && !loaded)
        {
            try
            {
                Manager.Log($"Attempting to get {page}. Attempts remaining: {attemptsRemaining}");
                page.GoTo();
                loaded = true;
            }
            catch (Exception ex)
            {
                Manager.Log(
                    $"Failed to load page. Attempts remaining: {attemptsRemaining - 1}. Exception: {ex.Message}"
                );
                attemptsRemaining--;
            }
        }
    }

    [Test]
    public void InventoryPageTableSortWorks()
    {
        var dashboardPage = page.EnterUsername("testuser")
            .EnterPassword("password123")
            .ClickSignIn();

        var inventoryPage = dashboardPage.ClickInventoryNavItem();

        // This one is to help you deal with sort with mixed characters
        inventoryPage.ClickSkuHeader();
        inventoryPage.ClickSkuHeader();
        inventoryPage.ClickSkuHeader();

        // This is just alphanumeric characters
        inventoryPage.ClickNameHeader();
        inventoryPage.ClickNameHeader();
        inventoryPage.ClickNameHeader();

        // This is just numbers
        inventoryPage.ClickQuantityHeader();
        inventoryPage.ClickQuantityHeader();
        inventoryPage.ClickQuantityHeader();
        inventoryPage.ClickQuantityHeader();

        // This is dates
        inventoryPage.ClickLastUpdatedHeader();
        inventoryPage.ClickLastUpdatedHeader();
        inventoryPage.ClickLastUpdatedHeader();

        // You are to implement some method or series of methods that
        // will determine if the table rows are in the correct order after each click of the header.

        // Hint: You can get a copy of the data rows before clicking the header to verify
        // that the data went back to normal after the third click of the same header.
    }

    [Test]
    public void InventoryPageTableFilterWorks()
    {
        const string NO_ITEMS_MESSAGE = "No inventory items match the current filter.";
        var dashboardPage = page.EnterUsername("testuser")
            .EnterPassword("password123")
            .ClickSignIn();
        var inventoryPage = dashboardPage.ClickInventoryNavItem();

        // Enter text that will give some rows, and verify the results are correct based on what is in the filter input.
        inventoryPage.EnterTextIntoFilter("INV-1002");

        // Clear filter and verify the table is back to normal.
        inventoryPage.ClearFilter();
        // Verify that the table is back to normal.

        // Enter text that will return no rows and verify that the text on the screen says 'No inventory items match the current filter.'.
        inventoryPage.EnterTextIntoFilter("I Don't Exist");

        Assert.That(inventoryPage.GetNoItemsMessage(), Is.EqualTo(NO_ITEMS_MESSAGE));
    }

    [Test]
    public void InventoryPageTablePaginationWorks()
    {
        var dashboardPage = page.EnterUsername("testuser")
            .EnterPassword("password123")
            .ClickSignIn();
        var inventoryPage = dashboardPage.ClickInventoryNavItem();

        // Change the paginator size to 10 and verify that there are 10 rows in the table.
        int currentTableLength = inventoryPage.GetDataRows().Count;

        inventoryPage.ChangePaginatorSizeTo(10);
        int prevTableLength = currentTableLength;

        currentTableLength = inventoryPage.GetDataRows().Count;

        Assert.That(currentTableLength, Is.Not.EqualTo(prevTableLength));
        Assert.That(currentTableLength, Is.EqualTo(10));

        // You are to implement some method or series of methods that will determine if
        // the paginator is showing the correct range of numbers
        // after clicking next, prev, last, and first on the paginator.

        // Hint: How can you find the 'x of y' text at the bottom of the table?
        inventoryPage.ClickNextOnPaginator();
        inventoryPage.ClickPrevOnPaginator();
        inventoryPage.ClickLastOnPaginator();
        inventoryPage.ClickFirstOnPaginator();
    }

    [Test]
    public void EditInventoryRowSavesSuccessfully()
    {
        var dashboardPage = page.EnterUsername("testuser")
            .EnterPassword("password123")
            .ClickSignIn();
        var inventoryPage = dashboardPage.ClickInventoryNavItem();

        var dialog = inventoryPage.ClickEditButton(inventoryPage.GetRandomDataRow());

        // You are to implement the following methods:

        dialog.ClearTextFromSKUInput();
        dialog.ClearTextFromNameInput();
        dialog.ClearTextFromCategoryInput();
        dialog.ClearTextFromLocationInput();
        dialog.ClearTextFromLastUpdatedInput();

        // Assign your own values to these variables.
        string newSku = string.Empty;
        string newName = string.Empty;
        string newCategory = string.Empty;
        string newLocation = string.Empty;
        DateTime newLastUpdated = DateTime.Now;
        int newQuantity = 0;

        // And then use those variables later for these methods:
        dialog.EnterTextIntoSKUInput(newSku);
        dialog.EnterTextIntoNameInput(newName);
        dialog.EnterTextIntoCategoryInput(newCategory);
        dialog.ClickStatusDropdown();
        dialog.SelectRandomStatusFromDropdown();
        dialog.IncrementQuantityInput();
        dialog.DecrementQuantityInput();
        dialog.EnterNumberIntoQuantityInput(newQuantity);
        dialog.EnterTextIntoLocationInput(newLocation);
        dialog.EnterDateIntoLastUpdatedInput(newLastUpdated);
        dialog.ClickSaveButton();

        // Now find the row you edited and verify that all the changes you made are in the table.
        // IMPORTANT: You cannot refresh the page, and i would advise you to use the filter bar.

        // You are to implement your own way of verifying the changes.
    }
}
