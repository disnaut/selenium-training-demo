namespace SeleniumTests.WebDriver.Structs;

readonly struct RecentActivity
{
    public string Activity { get; init; }
    public string Description { get; init; }
    public string Time { get; init; }
}
