using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Domain;

public abstract class RemovableBaseEntity : BaseEntity
{
    [BindNever]
    [ValidateNever]
    [Comment("Olemi kustutatuks märkimise aeg. Kui välja väärtus ei ole null, siis rakenduses olem nähtav ei ole.")]
    public DateTime? KustutamiseKP { get; set; }
}