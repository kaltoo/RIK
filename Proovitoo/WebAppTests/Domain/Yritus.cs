using System;

namespace WebAppTests.Domain;

public class Yritus
{
    public string? Nimi { get; set; }
    public DateTime? Algus { get; set; }
    public string? Koht { get; set; }
    public string? Lisainfo { get; set; }

    public static Yritus Genereeri()
    {
        return new Yritus
        {
            Nimi = Utils.RandomString(20, Utils.RandomMode.LettersOnly),
            Algus = DateTime.Now.AddHours(new Random().Next(2, 130)),
            Koht = Utils.RandomString(15, Utils.RandomMode.LettersAndNumbers),
            Lisainfo = Utils.RandomString(200, Utils.RandomMode.LettersAndNumbers)
        };
    }
}