using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain;
using WebApp.Pages.Interfaces;

namespace WebApp.Pages;

/// <summary>
/// Võimaldab lisada uut üritust.
/// Võimaldab kustutada üritust. Kustutamiseks vaja ürituse ID.
/// </summary>
public class YrituseLisamine : PageModel, ITitleViewCompatible
{
    private readonly WebApp.DAL.AppDbContext _context;

    public YrituseLisamine(WebApp.DAL.AppDbContext context)
    {
        _context = context;
    }
    public string TitleViewName { get; set; } = "Ürituse lisamine";
    
    [BindProperty(SupportsGet = true)]
    public int? KustutaYritusId { get; set; } = null;
    public async Task<IActionResult> OnGetAsync()
    {
        // Ürituse kustutamine
        if (KustutaYritusId != null)
        {
            var toBeDeleted =
                await _context.Yritused
                    .Include(x => x.Osalemised)
                    .FirstOrDefaultAsync(x => x.Id == KustutaYritusId && x.KustutamiseKP == null);

            if (toBeDeleted == null)
            {
                return RedirectToPage(nameof(Index), new{WarningMessage = "Ürituse kustutamine ebaõnnestus. Ei leitud sellist üritust. ID: " + KustutaYritusId});
            }
            else
            {
                toBeDeleted.KustutamiseKP = DateTime.Now;
                if (toBeDeleted.Osalemised != null && toBeDeleted.Osalemised.Count > 0)
                {
                    foreach (var _osalemine in toBeDeleted.Osalemised)
                    {
                        _osalemine.KustutamiseKP = DateTime.Now;
                    }
                }
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage(nameof(Index), new{WarningMessage = "Üritus kustutatud."});
                }
                catch (Exception e)
                {
                    return RedirectToPage(nameof(Index), new{ErrorMessage = "Ürituse kustutamine ebaõnnestus. Andmebaasi viga."});
                }
            }
        }
        return Page();
    }
    
    [BindProperty]
    public Yritus? Yritus { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid && Yritus != null)
        {
            _context.Yritused.Add(Yritus);
            await _context.SaveChangesAsync();
            return RedirectToPage(nameof(Index), new{SuccessMessage = "Uus üritus lisatud!"});
        }
        return Page();
    }

    
}