using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebAppTests;

public abstract class BaseTest
{
    
    protected IWebDriver driver = default!;
    
    [SetUp]
    public void Setup()
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AcceptInsecureCertificates = true;
        driver = new ChromeDriver(chromeOptions);
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }
    
    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
    }


}