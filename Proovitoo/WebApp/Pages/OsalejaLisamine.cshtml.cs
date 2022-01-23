using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain;
using WebApp.Pages.Interfaces;

namespace WebApp.Pages;


/// <summary>
/// Võimaldab lisada üritusele osalejaid. Vajab toimimiseks ürituse ID'd.
/// </summary>
public class OsalejaLisamine : PageModel, ITitleViewCompatible
{
    public string TitleViewName { get; set; } = "Osavõtja lisamine";

    [BindProperty(SupportsGet = true)]
    public int? YrituseId { get; set; } = null;
    
    
    private readonly WebApp.DAL.AppDbContext _context;

    public OsalejaLisamine(WebApp.DAL.AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        
        // Kui ürituse ID pole kaasa antud, saadame veateate.
        if (YrituseId == null)
        {
            return RedirectToPage(nameof(Index), new {ErrorMessage = "Osaleja lisamiseks on vajalik ürituse ID."});
        }

        // Kontrollime, kas andmebaasis eksisteerib üritus sellise antud ID'ga. Kontrollime, et see poleks kustutatud.
        var _yritus = await _context.Yritused.Where(x => x.KustutamiseKP == null).FirstOrDefaultAsync(x => x.Id.Equals(YrituseId));
        
        if (_yritus == null)
        {
            return RedirectToPage(nameof(Index), new {ErrorMessage = "Ei leidnud sellist üritust, mille ID on " + YrituseId});
        }
        
        // Kontrollime, et üritus ei ole juba toimunud.
        if (_yritus.Algus < DateTime.Now)
        {
            return RedirectToPage(nameof(YrituseDetailid), new {Id = _yritus.Id, ErrorMessage = "Toimunud üritusele ei saa osalejaid lisada."});
        }
        
        return Page();
    }

    [BindProperty]
    public Osalemine? Osalemine { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (YrituseId == null)
        {
            return RedirectToPage(nameof(Index), new {ErrorMessage = "Osaleja lisamiseks on vajalik ürituse ID."});
        }
        
        if (ModelState.IsValid && Osalemine != null)
        {
            var _yritus = await _context.Yritused
                .Where(x => x.KustutamiseKP == null)
                .FirstOrDefaultAsync(x => x.Id.Equals(YrituseId));

            if (_yritus == null)
            {
                return RedirectToPage(nameof(Index), new {ErrorMessage = "Osaleja lisamine ebaõnnestus. Sellise ID'ga üritust ei leitud. ID: " + YrituseId});
            }
            
            if (_yritus.Algus < DateTime.Now)
            {
                return RedirectToPage(nameof(YrituseDetailid), new {Id = _yritus.Id, ErrorMessage = "Toimunud üritusele ei saa osalejaid lisada."});
            }

            Osalemine!.Yritus = _yritus;
            
            // Tagame, et baasi ei jõua ebavajalikke andmeid
            if (Osalemine.Isik.Tyyp.Equals(EIsikTyyp.Fyysiline))
            {
                Osalemine.OsalejateArv = 1;
            } else if (Osalemine.Isik.Tyyp.Equals(EIsikTyyp.Juriidiline))
            {
                Osalemine.Isik.Nimi2 = null;
            }

            _context.Osalemised.Add(Osalemine);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage(nameof(Index), new {SuccessMessage = "Osaleja lisamine õnnestus."});
            }
            catch (Exception e)
            {
                return RedirectToPage(nameof(Index), new {ErrorMessage = "Osaleja lisamine ebaõnnestus. Andmebaasi probleem."});
            }
        }
        
        return Page();
    }

    
}