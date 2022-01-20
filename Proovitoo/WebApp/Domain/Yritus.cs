using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;

namespace WebApp.Domain;

public class Yritus : RemovableBaseEntity
{
    [MaxLength(80, ErrorMessage = "Ürituse nime pikkus võib olla maksimaalselt 80 märki.")]
    public string Nimi { get; set; } = default!;
    [MaxLength(1000, ErrorMessage = "Infovälja sisu võib olla maksimaalselt 1000 märki.")]
    public string? Info { get; set; }
    [MaxLength(125, ErrorMessage = "65u56u")]
    public string? Koht { get; set; }
    public DateTime Algus { get; set; }
   
    
}