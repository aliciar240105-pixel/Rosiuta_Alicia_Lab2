using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;

namespace Rosiuta_Alicia_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public IndexModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    // autorul ales din dropdown (null = toti autorii)
    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["AuthorList"] = new SelectList(_context.Set<Author>(), "ID", "FullName", AuthorID);

        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .AsQueryable();

        if (AuthorID != null)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        Book = await books.ToListAsync();
    }
}
