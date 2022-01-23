using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;


namespace WebApp.Domain;

public enum EOsalemineMakseviis
{
    [Display(Name = "Sularaha")]
    Sularaha,
    [Display(Name = "Pangaülekanne")]
    Pangaylekanne
}

[Comment("Vajalik ürituste ja isikute sidumiseks.")]
public class Osalemine : RemovableBaseEntity, IValidatableObject
{
    public int IsikId { get; set; }
    
    public Isik Isik { get; set; } = default!;
    
    public int YritusId { get; set; }
    
    [ValidateNever]
    [BindNever]
    public Yritus Yritus { get; set; } = default!;
    
    [Required(ErrorMessage = "Väli on kohustuslik.")]
    [Comment("Osaleja valitav makseviis. 0 - sularaha, 1 - pangaülekanne.")]
    [Display(Name = "Makseviis")]
    public EOsalemineMakseviis Makseviis { get; set; }
    
    [MaxLength(5000, ErrorMessage = "Lisainfo välja pikkus on maksimaalselt 5000 tähemärki.")]
    [Comment("Lisainfo väli osalejate soovidega. Rakenduse poolt piiratud füüsilise isiku korral varchar(1500). Juriidilise isiku puhul piirang varchar(5000).")]
    [Display(Name = "Lisainfo")]
    public string? Lisainfo { get; set; }
    
    [Comment("Üritusele saabuvate osalejate arv. Füüsilise isiku puhul alati 1. Juriidilise isiku puhul 1 - 1000000.")]
    [Range(1, 1000000, ErrorMessage = "Osalejate arv peab jääma vahemikku 1 - 1000000.")]
    [Display(Name = "Osalejate arv")]
    public int OsalejateArv { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Eraisik - Lisainfo väli (maksimaalselt 1500 tähemärki)
        if (Isik.Tyyp.Equals(EIsikTyyp.Fyysiline))
        {
            if (Lisainfo != null && Lisainfo.Length > 1500)
            {
                yield return new ValidationResult(
                    $"Füüsilise isiku puhul on lisainfo välja maksimaalne pikkus 1500 tähemärki.",
                    new[] { nameof(Lisainfo) });
            }
        }
    }
}