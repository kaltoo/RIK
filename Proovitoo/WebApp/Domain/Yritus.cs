using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Domain;

[Comment("Toimuvad ja toimunud üritused.")]
public class Yritus : RemovableBaseEntity, IValidatableObject
{
    [Comment("Ürituse nimi.")]
    [Required(ErrorMessage = "See väli on kohustuslik.")]
    [MaxLength(80, ErrorMessage = "Ürituse nime pikkus võib olla maksimaalselt 80 märki.")]
    [Display(Name = "Ürituse nimi")]
    public string Nimi { get; set; } = default!;

    [Comment("Informatsioon ürituse kohta.")]
    [MaxLength(1000, ErrorMessage = "Infovälja sisu võib olla maksimaalselt 1000 märki.")]
    [Display(Name = "Lisainfo")]
    public string? Info { get; set; }

    [Comment("Ürituse toimumise koht.")]
    [MaxLength(125, ErrorMessage = "Koha välja sisu võib olla maksimaalselt 125 märki.")]
    [Display(Name = "Koht")]
    public string? Koht { get; set; }

    [Comment("Ürituse algusaeg.")]
    [Required(ErrorMessage = "Väli on kohustuslik.")]
    [Display(Name = "Toimumise aeg")]
    public DateTime Algus { get; set; }

    public ICollection<Osalemine>? Osalemised { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Valideerime seda, et ürituse toimumise algusaeg oleks tulevikus.
        if (Algus <= DateTime.Now)
            yield return new ValidationResult(
                "Ürituse algusaeg peab olema tulevikus.",
                new[] {nameof(Algus)});
    }
}