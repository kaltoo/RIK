using System;
using System.Threading;
using NUnit.Framework;
using WebAppTests.Domain;
using WebAppTests.Domain.Shared;
using WebAppTests.Helpers;

namespace WebAppTests;

[TestFixture]
public class WebAppTests : BaseTest
{
    [Test]
    [Order(1)]
    public void SaabLisadaJaKustutadaYritusi()
    {
        Navigeeri.NavigeeriAvalehele(driver);
        var _yritus = Yritus.Genereeri();
        Vajuta.VajutaNuppu(Vajuta.ENupp.AVALEHT_LISAYRITUS, driver);
        Lisa.LisaYritus(_yritus, driver);
        Vajuta.VajutaNuppu(Vajuta.ENupp.LISAYRITUS_LISA, driver);
        Assert.True(
            Kontrolli.YritusKorrektseteAndmetegaAsubAvalehel(_yritus, driver, Kontrolli.EYrituseTyyp.Tulevased, 0));
        Vajuta.VajutaAvaleheYrituseKustutamiseLinki(_yritus, driver);
        Assert.False(
            Kontrolli.YritusKorrektseteAndmetegaAsubAvalehel(_yritus, driver, Kontrolli.EYrituseTyyp.Tulevased, 0));
    }

    [Test]
    [Order(2)]
    public void EiSaaLisadaJaYritusiMinevikuKuupaevaga()
    {
        Navigeeri.NavigeeriAvalehele(driver);
        var _yritus = Yritus.Genereeri();
        _yritus.Algus = DateTime.Now.AddDays(-5);
        Vajuta.VajutaNuppu(Vajuta.ENupp.AVALEHT_LISAYRITUS, driver);
        Lisa.LisaYritus(_yritus, driver);
        Vajuta.VajutaNuppu(Vajuta.ENupp.LISAYRITUS_LISA, driver);
        Assert.True(driver.PageSource.Contains("Ürituse algusaeg peab olema tulevikus"));
    }

    [Test]
    [Order(3)]
    public void EiSaaLisadaYritustIlmaNimeta()
    {
        Navigeeri.NavigeeriAvalehele(driver);
        var _yritus = Yritus.Genereeri();
        _yritus.Nimi = null;
        Vajuta.VajutaNuppu(Vajuta.ENupp.AVALEHT_LISAYRITUS, driver);
        Lisa.LisaYritus(_yritus, driver);
        Vajuta.VajutaNuppu(Vajuta.ENupp.LISAYRITUS_LISA, driver);
        Assert.True(driver.PageSource.Contains("See väli on kohustuslik."));
    }

    [Test]
    [Order(4)]
    public void SaabLisadaYrituseleOsalisi()
    {
        Navigeeri.NavigeeriAvalehele(driver);
        var _yritus = Yritus.Genereeri();
        Vajuta.VajutaNuppu(Vajuta.ENupp.AVALEHT_LISAYRITUS, driver);
        Lisa.LisaYritus(_yritus, driver);
        Vajuta.VajutaNuppu(Vajuta.ENupp.LISAYRITUS_LISA, driver);

        var _jurosaleja = JurOsaleja.Genereeri();
        var _fysosaleja = FysOsaleja.Genereeri();

        Vajuta.VajutaAvaleheYrituseOsalejaLisamiseLinki(_yritus, driver);
        Lisa.LisaOsaleja(_jurosaleja, driver, false);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);
        Vajuta.VajutaAvaleheYrituseOsalejaLisamiseLinki(_yritus, driver);
        Lisa.LisaOsaleja(_fysosaleja, driver, false);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);
        Assert.True(Kontrolli.YritusKorrektseteAndmetegaAsubAvalehel(_yritus, driver, Kontrolli.EYrituseTyyp.Tulevased,
            1 + (int) _jurosaleja!.OsalejateArv));
        Vajuta.VajutaAvaleheYrituseKustutamiseLinki(_yritus, driver);
    }
    [Test]
    [Order(5)]
    public void FyysiliseIsikuLisainfoOnPiiratud1500Margini()
    {
        Navigeeri.NavigeeriAvalehele(driver);
        var _yritus = Yritus.Genereeri();
        Vajuta.VajutaNuppu(Vajuta.ENupp.AVALEHT_LISAYRITUS, driver);
        Lisa.LisaYritus(_yritus, driver);
        Vajuta.VajutaNuppu(Vajuta.ENupp.LISAYRITUS_LISA, driver);

        var _jurosaleja = JurOsaleja.Genereeri();
        var _fysosaleja = FysOsaleja.Genereeri();
        
        Vajuta.VajutaAvaleheYrituseOsalejaLisamiseLinki(_yritus, driver);

        _fysosaleja.Lisainfo = Utils.RandomString(1501, Utils.RandomMode.LettersAndNumbers);
        _jurosaleja.Lisainfo = Utils.RandomString(5000, Utils.RandomMode.LettersAndNumbers);
        
        Lisa.LisaOsaleja(_fysosaleja, driver, false);
     
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);
        
        Assert.True(driver.PageSource.Contains("Füüsilise isiku puhul on lisainfo välja maksimaalne pikkus 1500 tähemärki"));
        Lisa.LisaOsaleja(_jurosaleja, driver, true);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);
        
        Assert.True(Kontrolli.YritusKorrektseteAndmetegaAsubAvalehel(_yritus, driver, Kontrolli.EYrituseTyyp.Tulevased, (int)_jurosaleja!.OsalejateArv!));
        Vajuta.VajutaAvaleheYrituseKustutamiseLinki(_yritus, driver);
    }

    [Test]
    [Order(6)]
    public void SaabLisadaMuutaKustutadaOsalejateAndmeid()
    {
        Navigeeri.NavigeeriAvalehele(driver);

        var _yritus = Yritus.Genereeri();
        Vajuta.VajutaNuppu(Vajuta.ENupp.AVALEHT_LISAYRITUS, driver);
        Lisa.LisaYritus(_yritus, driver);
        Vajuta.VajutaNuppu(Vajuta.ENupp.LISAYRITUS_LISA, driver);

        var _jurosaleja = JurOsaleja.Genereeri();
        var _jurosaleja2 = JurOsaleja.Genereeri();

        var _fysosaleja = FysOsaleja.Genereeri();
        var _fysosaleja2 = FysOsaleja.Genereeri();

        _jurosaleja2.Lisainfo = null;
        _fysosaleja2.Lisainfo = null;

        _jurosaleja.Makseviis = EMakseViis.Sularaha;
        _fysosaleja.Makseviis = EMakseViis.PangaYlekanne;
        

        Vajuta.VajutaAvaleheYrituseOsalejaLisamiseLinki(_yritus, driver);
        Lisa.LisaOsaleja(_jurosaleja, driver, false);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);

        Vajuta.VajutaAvaleheYrituseOsalejaLisamiseLinki(_yritus, driver);
        Lisa.LisaOsaleja(_jurosaleja2, driver, false);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);

        Vajuta.VajutaAvaleheYrituseOsalejaLisamiseLinki(_yritus, driver);
        Lisa.LisaOsaleja(_fysosaleja, driver, false);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);

        Vajuta.VajutaAvaleheYrituseOsalejaLisamiseLinki(_yritus, driver);
        Lisa.LisaOsaleja(_fysosaleja2, driver, false);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJALISAMINE_LISA, driver);

        Vajuta.VajutaAvaleheYrituseLinki(_yritus, Vajuta.EYrituseTyyp.Tulevased, driver);

        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_fysosaleja, driver));
        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_fysosaleja2, driver));

        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_jurosaleja, driver));
        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_jurosaleja2, driver));

        var _sjurosaleja = JurOsaleja.Genereeri();
        var _sjurosaleja2 = JurOsaleja.Genereeri();

        var _sfysosaleja = FysOsaleja.Genereeri();
        var _sfysosaleja2 = FysOsaleja.Genereeri();

        Vajuta.VajutaOsalejateLeheOsalejaMuutmiseLinki(_fysosaleja, driver);
        Lisa.LisaOsaleja(_sfysosaleja, driver, true);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJAMUUTMINE_MUUDA, driver);

        Vajuta.VajutaOsalejateLeheOsalejaMuutmiseLinki(_fysosaleja2, driver);
        Lisa.LisaOsaleja(_sfysosaleja2, driver, true);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJAMUUTMINE_MUUDA, driver);

        Vajuta.VajutaOsalejateLeheOsalejaMuutmiseLinki(_jurosaleja, driver);
        Lisa.LisaOsaleja(_sjurosaleja, driver, true);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJAMUUTMINE_MUUDA, driver);
        Vajuta.VajutaOsalejateLeheOsalejaMuutmiseLinki(_jurosaleja2, driver);
        Lisa.LisaOsaleja(_sjurosaleja2, driver, true);
        Vajuta.VajutaNuppu(Vajuta.ENupp.OSAVOTJAMUUTMINE_MUUDA, driver);

        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_fysosaleja, driver));
        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sfysosaleja, driver));
        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_fysosaleja2, driver));
        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sfysosaleja2, driver));

        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_jurosaleja, driver));
        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sjurosaleja, driver));
        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_jurosaleja2, driver));
        Assert.True(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sjurosaleja2, driver));

        Vajuta.VajutaOsalejateLeheOsalejaKustutamiseLinki(_sfysosaleja, driver);
        Vajuta.VajutaOsalejateLeheOsalejaKustutamiseLinki(_sfysosaleja2, driver);
        Vajuta.VajutaOsalejateLeheOsalejaKustutamiseLinki(_sjurosaleja, driver);
        Vajuta.VajutaOsalejateLeheOsalejaKustutamiseLinki(_sjurosaleja2, driver);

        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sfysosaleja, driver));
        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sfysosaleja2, driver));
        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sjurosaleja, driver));
        Assert.False(Kontrolli.OsalejaEksisteeribLehelKorrektseteAndmetega(_sjurosaleja2, driver));

        Navigeeri.NavigeeriAvalehele(driver);
        Vajuta.VajutaAvaleheYrituseKustutamiseLinki(_yritus, driver);
    }
}