using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Models;
using Rosiuta_Alicia_Lab2.ViewModels;

namespace Rosiuta_Alicia_Lab2.Pages.Publishers;

public class IndexModel : PageModel
{
    private readonly Rosiuta_Alicia_Lab2Context _context;

    public IndexModel(Rosiuta_Alicia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public PublisherIndexData PublisherData { get; set; }
    public int PublisherID { get; set; }
    public int BookID { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();
        PublisherData.Publishers = await _context.Publisher
            .Include(i => i.Books)
                .ThenInclude(c => c.Author)
            .OrderBy(i => i.PublisherName)
            .ToListAsync();

        if (id != null)
        {
            PublisherID = id.Value;
            Publisher? publisher = PublisherData.Publishers
                .Where(i => i.ID == id.Value).SingleOrDefault();
            if (publisher != null)
            {
                PublisherData.Books = publisher.Books;
            }
        }
    }
}
