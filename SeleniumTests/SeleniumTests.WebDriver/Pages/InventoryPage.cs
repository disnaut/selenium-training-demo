using System.Collections.ObjectModel;
using OpenQA.Selenium;
using SeleniumTests.WebDriver.Components;
using SeleniumTests.WebDriver.Interfaces;
using SeleniumTests.WebDriver.Pages.Abstract;

namespace SeleniumTests.WebDriver.Pages;

class InventoryPage(DriverManager Manager) : AbstractDemoPage(Manager), ILoadable<InventoryPage>
{
    private readonly WebElementDetails inventoryFilterInput = new(
        By.CssSelector("[data-testid='inventory-filter-input']"),
        "Inventory Filter Input"
    );

    private readonly WebElementDetails clearFilterButton = new(
        By.CssSelector("[data-testid='inventory-clear-filter-button']"),
        "Clear Filter Button"
    );

    private readonly WebElementDetails inventoryLoadingContainer = new(
        By.CssSelector("[data-testid='inventory-loading-container']"),
        "Inventory Loading Container"
    );

    private readonly WebElementDetails inventoryTable = new(
        By.CssSelector("[data-testid='inventory-table']"),
        "Inventory Table"
    );

    private readonly WebElementDetails skuHeader = new(
        By.CssSelector("[data-testid='inventory-header-sku']"),
        "SKU Column Header"
    );

    private readonly WebElementDetails nameHeader = new(
        By.CssSelector("[data-testid='inventory-header-name']"),
        "Description Column Header"
    );

    private readonly WebElementDetails categoryHeader = new(
        By.CssSelector("[data-testid='inventory-header-category']"),
        "Category Column Header"
    );

    private readonly WebElementDetails quantityHeader = new(
        By.CssSelector("[data-testid='inventory-header-quantity']"),
        "Quantity Column Header"
    );

    private readonly WebElementDetails statusHeader = new(
        By.CssSelector("[data-testid='inventory-header-status']"),
        "Status Column Header"
    );

    private readonly WebElementDetails locationHeader = new(
        By.CssSelector("[data-testid='inventory-header-location']"),
        "Location Column Header"
    );

    private readonly WebElementDetails lastUpdatedHeader = new(
        By.CssSelector("[data-testid='inventory-header-last-updated']"),
        "Last Updated Column Header"
    );

    private readonly WebElementDetails actionsHeader = new(
        By.CssSelector("[data-testid='inventory-header-actions']"),
        "Actions Column Header"
    );

    private readonly WebElementDetails paginator = new(
        By.CssSelector("[data-testid='inventory-paginator']"),
        "Inventory Paginator"
    );

    private readonly WebElementDetails dataRows = new(
        By.CssSelector("[data-testid^='inventory-row-']"),
        "Inventory Data Rows"
    );

    public InventoryPage Load()
    {
        base.Load();
        Manager
            .Load(inventoryFilterInput)
            .Unload(inventoryLoadingContainer)
            .Load(inventoryTable)
            .Load(skuHeader)
            .Load(nameHeader)
            .Load(categoryHeader)
            .Load(quantityHeader)
            .Load(statusHeader)
            .Load(locationHeader)
            .Load(lastUpdatedHeader)
            .Load(actionsHeader)
            .Load(paginator);
        return this;
    }

    public InventoryPage ChangePaginatorSizeTo(int size)
    {
        throw new NotImplementedException();
    }

    public int GetDataRowCount()
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickNextOnPaginator()
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickPrevOnPaginator()
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickLastOnPaginator()
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickFirstOnPaginator()
    {
        throw new NotImplementedException();
    }

    public InventoryPage EnterTextIntoFilter(string text)
    {
        Manager.TypeIntoTextbox(inventoryFilterInput, text);
        return this;
    }

    public ReadOnlyCollection<IWebElement> GetDataRows()
    {
        return Manager.Driver.FindElements(dataRows.Locator);
    }

    public string GetCurrentPaginatorRange()
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClearFilter()
    {
        Manager.Click(clearFilterButton);
        return this;
    }

    public InventoryPage ClickSkuHeader()
    {
        Manager.Click(skuHeader);
        return this;
    }

    public InventoryPage ClickNameHeader()
    {
        Manager.Click(nameHeader);
        return this;
    }

    public InventoryPage ClickCategoryHeader()
    {
        Manager.Click(categoryHeader);
        return this;
    }

    public InventoryPage ClickQuantityHeader()
    {
        Manager.Click(quantityHeader);
        return this;
    }

    public InventoryPage ClickStatusHeader()
    {
        Manager.Click(statusHeader);
        return this;
    }

    public InventoryPage ClickLocationHeader()
    {
        Manager.Click(locationHeader);
        return this;
    }

    public InventoryPage ClickLastUpdatedHeader()
    {
        Manager.Click(lastUpdatedHeader);
        return this;
    }

    public InventoryDetailsPage ClickRow(IWebElement row)
    {
        throw new NotImplementedException();
    }

    public IWebElement GetRandomDataRow()
    {
        throw new NotImplementedException();
    }

    public InventoryDetailsPage ClickEditButton(IWebElement row)
    {
        throw new NotImplementedException();
    }

    public InventoryPage ClickDeleteButton()
    {
        throw new NotImplementedException();
    }
}
