using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain;
using WebApp.Pages.Interfaces;

namespace WebApp.Pages;

/// <summary>
/// Võimaldab näha ürituse detailandmeid koos osalejate nimekirjaga. Vajab ürituse ID'd
/// Võimaldab kustutada ürituselt osalejat. Selleks vajab osaleja ID'd
/// </summary>
public class YrituseDetailid : PageModel, ITitleViewCompatible, IAlertViewCompatible
{
    [BindProperty(SupportsGet = true)]
    public string? ErrorMessage { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? WarningMessage { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? SuccessMessage { get; set; }
    public string TitleViewName { get; set; } = "Osavõtjad";
    
    private readonly WebApp.DAL.AppDbContext _context;

    [BindProperty(SupportsGet = true)]
    public int? Id { get; set; }
    
    [BindProperty(SupportsGet = true)]

    public int? KustutaOsalejaId { get; set; }

    public YrituseDetailid(WebApp.DAL.AppDbContext context)
    {
        _context = context;
    }

    public Yritus? Yritus { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        // Osaleja kustutamine
        if (KustutaOsalejaId != null)
        {
            var _toBeDeleted = await _context.Osalemised
                .Where(x => x.KustutamiseKP == null)
                .FirstOrDefaultAsync(x => x.Id.Equals(KustutaOsalejaId));

            if (_toBeDeleted == null)
            {
                return RedirectToPage(nameof(Index), 
                    new {ErrorMessage = "Osaleja kustutamine ebaõnnestus. Ei leitud ID: " + KustutaOsalejaId});
            }
            
            _toBeDeleted.KustutamiseKP = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage(nameof(YrituseDetailid), 
                    new{Id = _toBeDeleted.YritusId, WarningMessage = "Osaleja kustutatud."});
            }
            catch (Exception e)
            {
                return RedirectToPage(nameof(Index), 
                    new {ErrorMessage = "Osaleja kustutamine ebaõnnestus. Andmebaasi viga."});
            }
        }
        
        
        if (Id == null)
        {
            return RedirectToPage(nameof(Index), new {ErrorMessage = "Ürituse ID puudu."});
        }
        
        // Leiame ürituse ja kaasame ka sellele registreeritud mittekustutatud osalejad. 
        Yritus = await _context.Yritused
            .Include(x => x.Osalemised
                .Where(x => x.KustutamiseKP == null))
            .ThenInclude(x => x.Isik)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(Id) && x.KustutamiseKP == null );
        
        if (Yritus == null)
        {
            return RedirectToPage(nameof(Index), new {ErrorMessage = "Sellise ID'ga üritust ei leitud. ID: " + Id});
        }
        
        return Page();
    }

    
}