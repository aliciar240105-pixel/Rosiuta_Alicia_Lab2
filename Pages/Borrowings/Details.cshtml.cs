using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;

namespace Rosiuta_Alicia_Lab2.Pages.Borrowings;

public class DetailsModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public DetailsModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // aducem si membrul, cartea, autorul si editura ca sa le afisam
        var borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b.Author)
            .Include(b => b.Book)
                .ThenInclude(b => b.Publisher)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (borrowing == null)
        {
            return NotFound();
        }
        Borrowing = borrowing;
        return Page();
    }
}
