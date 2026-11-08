using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;

namespace Rosiuta_Alicia_Lab2.Pages.Borrowings;

public class CreateModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public CreateModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        PopulateDropDowns();
        return Page();
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateDropDowns();
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    // in listele derulante afisam numele membrilor si detaliile cartilor (nu ID-urile)
    private void PopulateDropDowns()
    {
        var bookList = _context.Book
            .Include(b => b.Author)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
            });

        ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
    }
}
