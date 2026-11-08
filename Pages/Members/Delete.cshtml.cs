using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;

namespace Rosiuta_Alicia_Lab2.Pages.Members;

public class DeleteModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public DeleteModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Member Member { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
        if (member is null)
        {
            return NotFound();
        }
        Member = member;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Member
            .Include(m => m.Borrowings)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (member != null)
        {
            Member = member;
            _context.Member.Remove(Member);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
