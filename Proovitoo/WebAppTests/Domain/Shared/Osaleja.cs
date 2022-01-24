namespace WebAppTests.Domain.Shared;

public enum EMakseViis
{
    Sularaha,
    PangaYlekanne
}
public abstract class Osaleja
{
    public EMakseViis? Makseviis { get; set; }
    public string? Lisainfo { get; set; }
}