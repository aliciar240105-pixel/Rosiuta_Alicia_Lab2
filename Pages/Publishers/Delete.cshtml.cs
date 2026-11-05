using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;

namespace Rosiuta_Alicia_Lab2.Pages.Publishers;

public class DeleteModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public DeleteModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Publisher Publisher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publisher = await _context.Publisher.FirstOrDefaultAsync(m => m.ID == id);
        if (publisher is null)
        {
            return NotFound();
        }
        Publisher = publisher;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        // incarcam si cartile, ca EF sa le puna PublisherID/AuthorID = null inainte de stergere
        var publisher = await _context.Publisher
            .Include(x => x.Books)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (publisher != null)
        {
            Publisher = publisher;
            _context.Publisher.Remove(Publisher);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
