using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;

namespace Rosiuta_Alicia_Lab2.Pages.Authors;

public class DeleteModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public DeleteModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Author Author { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var author = await _context.Author.FirstOrDefaultAsync(m => m.ID == id);
        if (author is null)
        {
            return NotFound();
        }
        Author = author;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        // incarcam si cartile, ca EF sa le puna PublisherID/AuthorID = null inainte de stergere
        var author = await _context.Author
            .Include(x => x.Books)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (author != null)
        {
            Author = author;
            _context.Author.Remove(Author);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
