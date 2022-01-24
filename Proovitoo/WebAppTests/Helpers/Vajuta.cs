using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebAppTests.Domain;
using WebAppTests.Domain.Shared;

namespace WebAppTests.Helpers;

public class Vajuta
{
    public enum ENupp
    {
        AVALEHT_LISAYRITUS,
        LISAYRITUS_LISA,
        LISAYRITUS_TAGASI,
        OSAVOTJALISAMINE_LISA,
        OSAVOTJALISAMINE_TAGASI,
        OSAVOTJAMUUTMINE_MUUDA,
        OSAVOTJAMUUTMINE_TAGASI,
        OSAVOTJAD_TAGASI,
        OSAVOTJAD_LISA
    }
    public enum EYrituseTyyp
    {
        Tulevased,
        Toimunud
    }

    public static void VajutaNuppu(ENupp nupp, IWebDriver driver)
    {
        switch (nupp)
        {
            case ENupp.AVALEHT_LISAYRITUS:
               driver.FindElement(By.PartialLinkText("LISA ÜRITUS")).Click();
                break;
            case ENupp.LISAYRITUS_LISA:
                driver.FindElement(By.Id("lisaYritusButton")).Click();
                break;
            case ENupp.LISAYRITUS_TAGASI:
                driver.FindElement(By.Id("backToMenuButton")).Click();
                break;
            case ENupp.OSAVOTJALISAMINE_LISA:
                driver.FindElement(By.Id("lisaOsalejaButton")).Click();
                break;
            case ENupp.OSAVOTJALISAMINE_TAGASI:
                driver.FindElement(By.Id("tagasiButton")).Click();
                break;
            case ENupp.OSAVOTJAMUUTMINE_MUUDA:
                driver.FindElement(By.Id("muudaOsalejaButton")).Click();
                break;
            case ENupp.OSAVOTJAMUUTMINE_TAGASI:
                driver.FindElement(By.Id("tagasiButton")).Click();
                break;
            case ENupp.OSAVOTJAD_TAGASI:
                driver.FindElement(By.Id("tagasiButton")).Click();
                break;
            case ENupp.OSAVOTJAD_LISA:
                driver.FindElement(By.Id("lisaOsalejaButton")).Click();
                break;
        }
    }

    #region YrituseToimingud
    public static void VajutaAvaleheYrituseLinki(Yritus yritus, EYrituseTyyp tyyp, IWebDriver driver)
    {
        switch (tyyp)
        {
            case EYrituseTyyp.Tulevased:
                driver.FindElement(By.Id("TulevasedYritusedDiv"))
                    .FindElement(By.PartialLinkText(yritus.Nimi))
                    .Click();
                break;
            case EYrituseTyyp.Toimunud:
                driver.FindElement(By.Id("ToimunudYritusedDiv"))
                    .FindElement(By.PartialLinkText(yritus.Nimi))
                    .Click();
                break;
        }
    }

    public static void VajutaAvaleheYrituseOsalejaLisamiseLinki(Yritus yritus, IWebDriver driver)
    {
        driver.FindElement(By.Id("TulevasedYritusedDiv"))
            ?.FindElements(By.TagName("tr"))
            ?.FirstOrDefault(x => x.Text!.ToLower().Contains(yritus.Nimi!.ToLower()))
            ?.FindElement(By.PartialLinkText("Lisa"))
            .Click();
    }

    public static void VajutaAvaleheYrituseKustutamiseLinki(Yritus yritus, IWebDriver driver)
    {
        driver.FindElement(By.Id("TulevasedYritusedDiv"))
            ?.FindElements(By.TagName("tr"))
            ?.FirstOrDefault(x => x.Text!.ToLower().Contains(yritus.Nimi!.ToLower()))
            ?.FindElement(By.ClassName("removeimage"))
            .Click();
    }
    #endregion
    #region OsalejaMuutmine
    public static void VajutaOsalejateLeheOsalejaMuutmiseLinki(FysOsaleja fysOsaleja, IWebDriver driver)
    {
        VajutaOsalejateLeheOsalejaMuutmiseLinki(fysOsaleja.Eesnimi + " " + fysOsaleja.Perekonnanimi, driver);
    }

    public static void VajutaOsalejateLeheOsalejaMuutmiseLinki(JurOsaleja jurOsaleja, IWebDriver driver)
    {
        VajutaOsalejateLeheOsalejaMuutmiseLinki(jurOsaleja.Nimi, driver);
    }

    private static void VajutaOsalejateLeheOsalejaMuutmiseLinki(string nimi, IWebDriver driver)
    {
        driver.FindElement(By.PartialLinkText(nimi)).Click();
    }
    #endregion
    #region OsalejaKustutamine
    public static void VajutaOsalejateLeheOsalejaKustutamiseLinki(FysOsaleja fysOsaleja, IWebDriver driver)
    {
        VajutaOsalejateLeheOsalejaKustutamiseLinki(fysOsaleja.Eesnimi + " " + fysOsaleja.Perekonnanimi, driver);
    }

    public static void VajutaOsalejateLeheOsalejaKustutamiseLinki(JurOsaleja jurOsaleja, IWebDriver driver)
    {
        VajutaOsalejateLeheOsalejaKustutamiseLinki(jurOsaleja.Nimi, driver);
    }

    private static void VajutaOsalejateLeheOsalejaKustutamiseLinki(string nimi, IWebDriver driver)
    {
        driver
            ?.FindElements(By.TagName("tr"))
            ?.FirstOrDefault(x => x.Text.ToLower().Contains(nimi.ToLower()))
            ?.FindElement(By.PartialLinkText("Kustuta")).Click();
    }
    #endregion


    }