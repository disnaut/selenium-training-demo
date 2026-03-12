using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Pages;

namespace SeleniumTests.WebDriver.Tests;

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

    /// <summary>
    /// This test is meant to verify that the sorting functionality of the inventory table works correctly.
    /// Hints:
    /// - You can get a copy of the data rows before clicking the header to verify that the data went back to normal after the third click of the same header.
    /// - You will need to implement some method or series of methods that will determine if the table rows are in the correct order after each click of the header.
    /// - Remember, you have a <tr> that has a number of <td>s inside of it. You don't have to verify every <td>, just the one that corresponds to the header you are clicking on.
    ///     For example, if you are clicking the 'Name' header, you just need to verify that the <td> that corresponds to the 'Name' header is in the correct order.
    /// </summary>
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
    }

    /// <summary>
    /// This test is meant to verify that the filtering functionality of the inventory table works correctly.
    /// Each time the filter input is used, you will need to verify that the rows presented are what should be expected.
    /// Hints:
    /// - You can look at the source code for inventory-list to see how the filter is implemented to get an idea of how to verify the results.
    /// </summary>
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

    /// <summary>
    /// This test is meant to verify that the pagination functionality of the inventory table works correctly.
    /// After each interaction with the paginator, you will need to verify that the correct range of rows is being presented on the screen.
    /// Hints:
    /// - You can always just look for the <mat-option> tag if you are certain that no other <mat-option> tags are on the page.
    /// - You should verify that the number that you want the paginator to be showing is actually the number that is being shown from the list of options.
    /// - The 'x of y' text at the bottom of the table should change as you interact with the paginator, you can use this to verify that the correct range of rows is being shown on the screen. (e.g '1-10 of 50' should change to '11-20 of 50' after clicking next on the paginator)
    /// </summary>
    [Test]
    public void InventoryPageTablePaginationWorks()
    {
        var dashboardPage = page.EnterUsername("testuser")
            .EnterPassword("password123")
            .ClickSignIn();
        var inventoryPage = dashboardPage.ClickInventoryNavItem();

        int currentTableLength = inventoryPage.GetDataRows().Count;

        inventoryPage.ChangePaginatorSizeTo(10);
        int prevTableLength = currentTableLength;

        currentTableLength = inventoryPage.GetDataRows().Count;

        Assert.That(currentTableLength, Is.Not.EqualTo(prevTableLength));
        Assert.That(currentTableLength, Is.EqualTo(10));

        inventoryPage.ClickNextOnPaginator();
        // Verify that Next worked

        inventoryPage.ClickPrevOnPaginator();
        // Verify that Prev worked

        inventoryPage.ClickLastOnPaginator();
        // Verify that Last worked

        inventoryPage.ClickFirstOnPaginator();
        // Verify that First worked
    }

    /// <summary>
    /// This test should be done last. This requires you to create the entire Page-Object-Model for <see cref="EditInventoryDialog"/>.
    /// This test is meant to verify that the in-line edit button works correctly for a given row.
    /// This test also is trusting you to create your own assertions to verify changes 'saved' after clicking the save button on the dialog.
    /// Hints:
    /// - It isn't a bad idea for the "GetRandomDataRow" to use a seed so you can test the same row repeatedly.
    /// - The Increment and Decrement methods are going to be difficult, but once you have one, you'll easily be able to do the other.
    /// - Do not use any methods that will refresh the page because the data is loaded in memory so refreshing the page will cause you to lose any changes you have made because there is no backend.
    /// - Make sure to verify that the changes you made were actually saved. You can do this by checking the text of the row you edited after clicking save and verifying that it matches what you entered into the dialog.
    /// - Remember, you can always look at the source code for the inventory-list and edit-inventory-dialog components to get an idea of how the HTML is structured which will help you with your element selectors.
    /// </summary>
    [Test]
    public void EditInventoryRowSavesSuccessfully()
    {
        var dashboardPage = page.EnterUsername("testuser")
            .EnterPassword("password123")
            .ClickSignIn();
        var inventoryPage = dashboardPage.ClickInventoryNavItem();

        var dialog = inventoryPage.ClickEditButton(inventoryPage.GetRandomDataRow());

        // You are to implement the following methods:

        dialog
            .ClearTextFromSKUInput()
            .ClearTextFromNameInput()
            .ClearTextFromCategoryInput()
            .ClearTextFromLocationInput()
            .ClearTextFromLastUpdatedInput();

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
    }
}
