namespace WebAppTests.Domain.Shared;

public class FysOsaleja : Osaleja
{
    public string? Eesnimi { get; set; }
    public string? Perekonnanimi { get; set; }
    public string? Isikukood { get; set; }

    public static FysOsaleja Genereeri()
    {
        return new FysOsaleja
        {
            Eesnimi = Utils.RandomString(20, Utils.RandomMode.LettersOnly),
            Perekonnanimi = Utils.RandomString(20, Utils.RandomMode.LettersOnly),
            Isikukood = "3930912" + Utils.RandomString(4, Utils.RandomMode.NumbersOnly),
            Makseviis = EMakseViis.Sularaha,
            Lisainfo = Utils.RandomString(100, Utils.RandomMode.LettersAndNumbers)
        };
    }
}