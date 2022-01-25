using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebAppTests.Domain;
using WebAppTests.Domain.Shared;

namespace WebAppTests.Helpers;

public class Lisa
{
    public static void LisaYritus(Yritus yritus, IWebDriver driver)
    {
        if (yritus.Nimi != null) driver.FindElement(By.Id("Yritus_Nimi")).SendKeys(yritus.Nimi);
        if (yritus.Algus != null)
            driver.FindElement(By.Id("Yritus_Algus")).SendKeys(Utils.GetInputForDateBox((DateTime) yritus.Algus));
        if (yritus.Koht != null) driver.FindElement(By.Id("Yritus_Koht")).SendKeys(yritus.Koht);
        if (yritus.Lisainfo != null) driver.FindElement(By.Id("Yritus_Info")).SendKeys(yritus.Lisainfo);
    }

    public static void LisaOsaleja(FysOsaleja fysOsaleja, IWebDriver driver, bool resetFields)
    {
        new SelectElement(driver.FindElement(By.Id("Osalemine_Isik_Tyyp"))).SelectByText("Füüsiline isik");

        if (resetFields)
        {
            driver.FindElement(By.Id("Osalemine_Isik_Nimi1")).Clear();
            driver.FindElement(By.Id("Osalemine_Isik_Nimi2")).Clear();
            driver.FindElement(By.Id("Osalemine_Isik_Kood")).Clear();
            driver.FindElement(By.Id("Osalemine_Lisainfo")).Clear();
        }

        if (fysOsaleja.Eesnimi != null) driver.FindElement(By.Id("Osalemine_Isik_Nimi1")).SendKeys(fysOsaleja.Eesnimi);
        if (fysOsaleja.Perekonnanimi != null)
            driver.FindElement(By.Id("Osalemine_Isik_Nimi2")).SendKeys(fysOsaleja.Perekonnanimi);
        if (fysOsaleja.Isikukood != null)
            driver.FindElement(By.Id("Osalemine_Isik_Kood")).SendKeys(fysOsaleja.Isikukood);
        if (fysOsaleja.Makseviis != null)
            switch (fysOsaleja.Makseviis)
            {
                case EMakseViis.Sularaha:
                    new SelectElement(driver.FindElement(By.Id("Osalemine_Makseviis"))).SelectByText("Sularaha");
                    ;
                    break;
                case EMakseViis.PangaYlekanne:
                    new SelectElement(driver.FindElement(By.Id("Osalemine_Makseviis"))).SelectByText("Pangaülekanne");
                    ;
                    break;
            }

        if (fysOsaleja.Lisainfo != null) driver.FindElement(By.Id("Osalemine_Lisainfo")).SendKeys(fysOsaleja.Lisainfo);
    }

    public static void LisaOsaleja(JurOsaleja jurOsaleja, IWebDriver driver, bool resetFields)
    {
        new SelectElement(driver.FindElement(By.Id("Osalemine_Isik_Tyyp"))).SelectByText("Juriidiline isik");

        if (resetFields)
        {
            driver.FindElement(By.Id("Osalemine_Isik_Nimi1")).Clear();
            driver.FindElement(By.Id("Osalemine_Isik_Kood")).Clear();
            driver.FindElement(By.Id("Osalemine_Lisainfo")).Clear();
        }

        if (jurOsaleja.Nimi != null) driver.FindElement(By.Id("Osalemine_Isik_Nimi1")).SendKeys(jurOsaleja.Nimi);
        if (jurOsaleja.Registrikood != null)
            driver.FindElement(By.Id("Osalemine_Isik_Kood")).SendKeys(jurOsaleja.Registrikood);
        if (jurOsaleja.OsalejateArv != null)
        {
            var _tempx = driver.FindElement(By.Id("Osalemine_OsalejateArv"));
            _tempx.Click();
            _tempx.Clear();
            _tempx.SendKeys(jurOsaleja.OsalejateArv.ToString());
        }

        if (jurOsaleja.Makseviis != null)
            switch (jurOsaleja.Makseviis)
            {
                case EMakseViis.Sularaha:
                    new SelectElement(driver.FindElement(By.Id("Osalemine_Makseviis"))).SelectByText("Sularaha");
                    ;
                    break;
                case EMakseViis.PangaYlekanne:
                    new SelectElement(driver.FindElement(By.Id("Osalemine_Makseviis"))).SelectByText("Pangaülekanne");
                    ;
                    break;
            }

        if (jurOsaleja.Lisainfo != null) driver.FindElement(By.Id("Osalemine_Lisainfo")).SendKeys(jurOsaleja.Lisainfo);
    }
}