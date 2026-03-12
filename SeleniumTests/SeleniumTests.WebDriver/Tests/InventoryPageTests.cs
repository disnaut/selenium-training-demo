using SeleniumTests.WebDriver.Enums;
using SeleniumTests.WebDriver.Pages;

namespace SeleniumTests.WebDriver.Tests;

[TestFixture(DriverType.Chrome)]
[TestFixture(DriverType.Edge)]
class InventoryPageTests(DriverType driverType) : AbstractSeleniumTest(driverType)
{
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
    public void InventoryPageTableActionsWork()
    {
        var inventoryPage = page.EnterUsername("testuser")
            .EnterPassword("password123")
            .ClickSignIn()
            .ClickInventoryNavItem()
            .ChangePaginatorSizeTo(10);

        var currentTableLength = inventoryPage.GetDataRowCount();
        Assert.That(currentTableLength, Is.Not.EqualTo(5));
        Assert.That(currentTableLength, Is.EqualTo(10));

        // TODO: Assert that the paginator shows the next range of numbers
        inventoryPage.ClickNextOnPaginator();

        // TODO: Assert that the paginator shows the previous range of numbers
        inventoryPage.ClickPrevOnPaginator();

        // TODO: Assert that the paginator shows the last range of numbers
        inventoryPage.ClickLastOnPaginator();

        // TODO: Assert that the paginator shows the first range of numbers
        inventoryPage.ClickFirstOnPaginator();

        // Enter text that will give rows
        inventoryPage.EnterTextIntoFilter("Widget");

        // Clear filter
        inventoryPage.ClearFilter();

        // Enter text that will give no rows
        inventoryPage.EnterTextIntoFilter("asdfasdf");

        #region Header Sorting

        // Assert that clicking the header multiple times toggles between ascending, descending, and no sort
        inventoryPage.ClickSkuHeader();
        inventoryPage.ClickSkuHeader();
        inventoryPage.ClickSkuHeader();

        // Assert that clicking the header multiple times toggles between ascending, descending, and no sort
        inventoryPage.ClickNameHeader();
        inventoryPage.ClickNameHeader();
        inventoryPage.ClickNameHeader();

        // Assert that clicking the header multiple times toggles between ascending, descending, and no sort
        // Practice with numbers
        inventoryPage.ClickQuantityHeader();
        inventoryPage.ClickQuantityHeader();
        inventoryPage.ClickQuantityHeader();

        // Assert that clicking the header multiple times toggles between ascending, descending, and no sort
        // Practice with Dates
        inventoryPage.ClickLastUpdatedHeader();
        inventoryPage.ClickLastUpdatedHeader();
        inventoryPage.ClickLastUpdatedHeader();

        // Pick a random row and click the row to open the Edit Inventory Dialog
        var dialog = inventoryPage.ClickRow(inventoryPage.GetRandomDataRow());

        
        #endregion
    }
}
