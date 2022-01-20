using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace WebApp.Domain;

public enum EIsikTyyp
{
    [Display(Name = "Füüsiline isik")]
    Fyysiline,
    [Display(Name = "Juriidiline isik")]
    Juriidiline
}



public class Isik : BaseEntity, IValidatableObject
{
    public EIsikTyyp Tyyp { get; set; }
    
    [MaxLength(70, ErrorMessage = "See väli võib olla maksimaalselt 70 märki.")]
    [Required(ErrorMessage = "See väli on nõutud.")]
    public string Nimi1 { get; set; } = default!;
    
    [MaxLength(70, ErrorMessage = "See väli võib olla maksimaalselt 70 märki.")]
    [Required(ErrorMessage = "See väli on nõutud.")]
    public string? Nimi2 { get; set; }

    [MaxLength(20, ErrorMessage = "See väli võib olla maksimaalselt 70 märki.")] 
    public string Kood { get; set; } = default!;
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Tyyp.Equals(EIsikTyyp.Fyysiline) && !Kood.All(x => char.IsDigit(x)))
        {
            yield return new ValidationResult(
                $"Isikukood peab koosnema numbritest.",
                new[] { nameof(Kood) });
        } 
    }
}