using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Domain;

public enum EIsikTyyp
{
    [Display(Name = "Füüsiline isik")] Fyysiline,
    [Display(Name = "Juriidiline isik")] Juriidiline
}

[Comment("Isikuandmed.")]
public class Isik : BaseEntity, IValidatableObject
{
    public EIsikTyyp Tyyp { get; set; }

    [Comment("Füüsilise isiku eesnimi või juriidilise isiku nimi.")]
    [MaxLength(70, ErrorMessage = "See väli võib olla maksimaalselt 70 märki.")]
    [Required(ErrorMessage = "See väli on nõutud.")]
    public string Nimi1 { get; set; } = default!;

    [Comment("Füüsilise isiku perekonnanimi. Juriidilise isiku puhul väli tühi.")]
    [MaxLength(70, ErrorMessage = "See väli võib olla maksimaalselt 70 märki.")]
    public string? Nimi2 { get; set; }

    [Comment("Füüsilise isiku isikukood või juriidilise isiku registrikood.")]
    [MaxLength(20, ErrorMessage = "See väli võib olla maksimaalselt 70 märki.")]
    [Required(ErrorMessage = "See väli on kohustuslik.")]
    public string Kood { get; set; } = default!;

    public ICollection<Osalemine>? Osalemised { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Juriidilise isiku valideerimine
        if (Tyyp.Equals(EIsikTyyp.Juriidiline))
        {
            // Peab koosnema ainult numbritest
            if (!Kood.All(x => char.IsDigit(x)))
                yield return new ValidationResult(
                    "Registrikood peab koosnema numbritest.",
                    new[] {nameof(Kood)});

            // Peab olema 8 kohaline
            if (Kood.Length != 8)
                yield return new ValidationResult(
                    "Registrikood peab olema 8 kohaline.",
                    new[] {nameof(Kood)});
        }

        // Füüsilise isiku valideerimine
        if (Tyyp == EIsikTyyp.Fyysiline)
        {
            // Perekonnanimi ole tühi (Andmebaasis nõue puudub)
            if (string.IsNullOrWhiteSpace(Nimi2))
                yield return new ValidationResult(
                    "See väli on nõutud.",
                    new[] {nameof(Nimi2)});

            // Isikukoodi pikkus 11 märki
            if (Kood.Length != 11)
                yield return new ValidationResult(
                    "Isikukood peab olema 11 kohaline.",
                    new[] {nameof(Kood)});

            // Isikukood koosneb ainult numbritest
            if (!Kood.All(x => char.IsDigit(x)))
                yield return new ValidationResult(
                    "Isikukood peab koosnema numbritest.",
                    new[] {nameof(Kood)});

            // Isikukoodi valideerimine
            if (Kood.All(x => char.IsDigit(x)) && Kood.Length == 11)
            {
                var _kood = Kood.ToCharArray().Select(c => c.ToString()).ToArray();
                var _year = 1600 + int.Parse(_kood[0]) * 100;
                _year = _year + int.Parse(_kood[1] + _kood[2]);
                var _month = int.Parse(_kood[3] + _kood[4]);
                var _day = int.Parse(_kood[5] + _kood[6]);

                var _valid = true;
                try
                {
                    var _Date = new DateTime(_year, _month, _day);
                }
                catch (Exception e)
                {
                    _valid = false;
                }

                if (!_valid)
                    yield return new ValidationResult(
                        "Isikukoodi formaat on vale.",
                        new[] {nameof(Kood)});
            }
        }
    }
}