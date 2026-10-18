using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public async Task OnGetAsync()
    {
        Book = await _context.Book.ToListAsync();
    }
}
