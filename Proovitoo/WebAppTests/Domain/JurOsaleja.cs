using System;

namespace WebAppTests.Domain.Shared;

public class JurOsaleja : Osaleja
{
    public string? Nimi { get; set; }
    public string? Registrikood { get; set; }
    public int? OsalejateArv { get; set; }

    public static JurOsaleja Genereeri()
    {
        return new JurOsaleja
        {
            Nimi = Utils.RandomString(20, Utils.RandomMode.LettersOnly),
            Registrikood = Utils.RandomString(8, Utils.RandomMode.NumbersOnly),
            OsalejateArv = new Random().Next(1, 140),
            Makseviis = EMakseViis.PangaYlekanne,
            Lisainfo = Utils.RandomString(150, Utils.RandomMode.LettersAndNumbers)
        };
    }
}