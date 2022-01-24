using OpenQA.Selenium;

namespace WebAppTests.Helpers;

public class Navigeeri
{
    public static void NavigeeriAvalehele(IWebDriver driver)
    {
        driver.Navigate().GoToUrl(ConnectionInfo.URL);
    }
}