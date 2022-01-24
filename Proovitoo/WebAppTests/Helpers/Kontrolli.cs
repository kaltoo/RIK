using System;
using System.Linq;
using OpenQA.Selenium;
using WebAppTests.Domain;
using WebAppTests.Domain.Shared;

namespace WebAppTests.Helpers;

public class Kontrolli
{
    public enum EYrituseTyyp
    {
        Tulevased,
        Toimunud
    }
    
    public static bool YritusKorrektseteAndmetegaAsubAvalehel(Yritus yritus, IWebDriver driver, EYrituseTyyp tyyp, int osalejaid)
    {
        switch (tyyp)
        {
            case EYrituseTyyp.Tulevased:
            {
                var _temp =
                    driver.FindElement(By.Id("TulevasedYritusedDiv"))
                        .FindElements(By.TagName("tr"))
                        .FirstOrDefault(x => x
                            .Text
                            .ToLower()
                            .Contains(yritus.Nimi!.ToLower()));
                
                if (_temp == null) return false;

                var _containsRightDateTime = _temp.Text
                    .ToLower()
                    .Contains(((DateTime) yritus.Algus).ToString("dd.MM.yyyy HH:mm"));

                var _containsOsalejad = _temp.Text
                    .ToLower()
                    .Contains($"osavõtjad ({osalejaid.ToString()})");

                var _containsKoht = true;

                if (yritus.Koht != null)
                {
                    _containsKoht = _temp.Text
                        .ToLower()
                        .Contains(yritus.Koht.ToLower());
                }

                return (_containsRightDateTime && _containsOsalejad && _containsKoht);
            }
               
            case EYrituseTyyp.Toimunud:
                return driver.FindElement(By.Id("ToimunudYritusedDiv"))
                    .Text
                    .ToLower()
                    .Contains(yritus.Nimi!.ToLower());
        }
        return false;
    }

    public static bool OsalejaEksisteeribLehelKorrektseteAndmetega(FysOsaleja fysOsaleja, IWebDriver driver)
    {
        var _temp =
            driver.FindElements(By.TagName("tr"))
                .FirstOrDefault(x => x
                    .Text
                    .ToLower()
                    .Contains(fysOsaleja.Eesnimi!.ToLower()));
                
        if (_temp == null) return false;

        if (_temp.Text.ToLower().Contains(fysOsaleja.Isikukood!.ToLower()))
        {
            return true;
        }
        
        return false;
    }
    public static bool OsalejaEksisteeribLehelKorrektseteAndmetega(JurOsaleja jurOsaleja, IWebDriver driver)
    {
        var _temp =
            driver.FindElements(By.TagName("tr"))
                .FirstOrDefault(x => x
                    .Text
                    .ToLower()
                    .Contains(jurOsaleja.Nimi!.ToLower()));
                
        if (_temp == null) return false;

        if (_temp.Text.ToLower().Contains(jurOsaleja.Registrikood!.ToLower())
            && _temp.Text.ToLower().Contains($"[Osaleb: {jurOsaleja.OsalejateArv.ToString()}]".ToLower()))
        {
            return true;
        }
        
        return false;
    }
}