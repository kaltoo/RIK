using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;
using WebApp.Pages.Interfaces;

namespace WebApp.Pages;

/// <summary>
///     Võimaldab vaadata ja muuta osaleja andmeid. Vajab osaleja ID väärtust.
/// </summary>
public class OsalejaDetailid : PageModel, ITitleViewCompatible
{
    private readonly AppDbContext _context;

    public OsalejaDetailid(AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty(SupportsGet = true)] public int? Id { get; set; }
    
    [BindProperty] public Osalemine? Osalemine { get; set; }

    public string TitleViewName { get; set; } = "Osavõtja info";

    public async Task<IActionResult> OnGetAsync()
    {
        // Kontrollime, kas külastaja pani osalemise ID kaasa
        if (Id == null)
            return RedirectToPage(nameof(Index), new {ErrorMessage = "Osaleja vaatamine ebaõnnestus. Puudub ID."});

        Osalemine = await _context.Osalemised.Include(x => x.Isik)
            .Where(x => x.KustutamiseKP == null)
            .FirstOrDefaultAsync(x => x.Id.Equals(Id));

        if (Osalemine == null)
            return RedirectToPage(nameof(Index),
                new {ErrorMessage = "Osaleja vaatamine ebaõnnestus. Ei leitud osalejat ID'ga " + Id});

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || Osalemine == null) return Page();

        // Tagame, et baasi ei jõua ebavajalikke andmeid
        if (Osalemine.Isik.Tyyp.Equals(EIsikTyyp.Fyysiline))
            Osalemine.OsalejateArv = 1;
        else if (Osalemine.Isik.Tyyp.Equals(EIsikTyyp.Juriidiline)) Osalemine.Isik.Nimi2 = null;

        _context.Attach(Osalemine).State = EntityState.Modified;

        // Kontrollime, et osalemist pole keegi juba kustutanud.
        var _exists = await _context.Osalemised
            .AnyAsync(x => x.Id == Osalemine.Id && x.KustutamiseKP == null);

        if (!_exists)
            return RedirectToPage(nameof(YrituseDetailid),
                new {Id = Osalemine!.YritusId, ErrorMessage = "See osaleja on kellegi poolt juba kustutatud."});

        try
        {
            await _context.SaveChangesAsync();
            return RedirectToPage(nameof(YrituseDetailid),
                new {Id = Osalemine!.YritusId, SuccessMessage = "Osaleja andmed muudetud."});
        }
        catch (Exception e)
        {
            return RedirectToPage(nameof(Index), new {ErrorMessage = "Osaleja muutmine ebaõnnestus. Andmebaasi viga."});
        }
    }
}