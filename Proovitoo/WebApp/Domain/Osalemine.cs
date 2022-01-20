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


public class Osalemine : RemovableBaseEntity
{
    public int IsikId { get; set; }
    
    [ValidateNever]
    [BindNever]
    public Isik Isik { get; set; } = default!;

    public int YritusId { get; set; }
    
    [ValidateNever]
    [BindNever]
    public Yritus Yritus { get; set; } = default!;
    
    public EOsalemineMakseviis Makseviis { get; set; }
    
    [MaxLength(5000, ErrorMessage = "rgergerg")]
    public string? Lisainfo { get; set; }
    
    [Comment("Üritusele saabuvate osalejate arv. Füüsilise isiku puhul alati 1. Juriidilise isiku puhul 1 - 1000000.")]
    [Range(1, 1000000, ErrorMessage = "Osalejate arv peab jääma vahemikku 1 - 1000000.")]
    public int OsalejateArv { get; set; }
    
    

}