using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace WebAppTests;

[TestFixture]
public class FunktsionaalsedNoudedTestid : BaseTest
{

    [Test]
    [Order(1)]
    public void AvalehtAvaneb()
    {
        driver.Navigate().GoToUrl(ConnectionInfo.URL);
        
        // 2022-01-23T18:47
        
        // TBD

    }
}