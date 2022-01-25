using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Pages.Interfaces;

namespace WebApp.Pages;

/// <summary>
///     Võimaldab näha kõikide kustutamata ürituste ülevaadet.
/// </summary>
public class IndexModel : PageModel, IAlertViewCompatible
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<YritusedResultDTO> YritusedTulevikus { get; set; } = new();
    public List<YritusedResultDTO> YritusedMinevikus { get; set; } = new();

    [BindProperty(SupportsGet = true)] public string? ErrorMessage { get; set; } = null;

    [BindProperty(SupportsGet = true)] public string? WarningMessage { get; set; } = null;

    [BindProperty(SupportsGet = true)] public string? SuccessMessage { get; set; } = null;

    public async Task<IActionResult> OnGetAsync()
    {
        // Pärime kõikide ürituste kohta, kuid liigse andmeliikluse vältimiseks kasutame DTO'd.
        var query = _context.Yritused.Where(x => x.KustutamiseKP == null).OrderBy(x => x.Algus);
        var result = await query
            .Select(x =>
                new YritusedResultDTO
                {
                    Id = x.Id,
                    Nimi = x.Nimi,
                    Aeg = x.Algus,
                    Koht = x.Koht ?? "",
                    Osalejaid = x.Osalemised != null
                        ? x.Osalemised
                            .Where(z => z.KustutamiseKP == null)
                            .Sum(y => y.OsalejateArv)
                        : 0
                }
            )
            .OrderBy(x => x.Aeg)
            .ToListAsync();

        var _hetkeAeg = DateTime.Now;

        // Sorteerime üritused vastavalt toimunud ja toimuvateks üritusteks-
        result.ForEach(x =>
        {
            if (x.Aeg > _hetkeAeg)
                YritusedTulevikus.Add(x);
            else
                YritusedMinevikus.Add(x);
        });
        return Page();
    }

    public class YritusedResultDTO
    {
        public int Id { get; set; }
        public string Nimi { get; set; } = default!;
        public DateTime Aeg { get; set; }
        public string Koht { get; set; } = default!;
        public int Osalejaid { get; set; }
    }
}