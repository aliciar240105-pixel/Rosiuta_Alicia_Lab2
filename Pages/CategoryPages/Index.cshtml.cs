using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;
using Rosiuta_Alicia_Lab2.ViewModels;

namespace Rosiuta_Alicia_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public IndexModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public CategoryIndexData CategoryData { get; set; }
    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData = new CategoryIndexData();
        CategoryData.Categories = await _context.Category
            .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            Category? category = CategoryData.Categories
                .Where(c => c.ID == id.Value).SingleOrDefault();
            if (category != null && category.BookCategories != null)
            {
                // cartile din categoria selectata (prin tabela de legatura BookCategory)
                CategoryData.Books = category.BookCategories
                    .Where(bc => bc.Book != null)
                    .Select(bc => bc.Book!);
            }
        }
    }
}
