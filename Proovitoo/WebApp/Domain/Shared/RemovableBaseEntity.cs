using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApp.Domain;

public abstract class RemovableBaseEntity : BaseEntity
{
    [BindNever]
    [ValidateNever]
    public DateTime? KustutamiseKP { get; set; }
}