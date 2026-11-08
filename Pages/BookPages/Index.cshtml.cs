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
    public BookData BookD { get; set; }
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public string TitleSort { get; set; }
    public string AuthorSort { get; set; }

    public string CurrentFilter { get; set; }

    // autorul ales din dropdown (null = toti autorii) - de la lab 2
    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
    {
        ViewData["AuthorList"] = new SelectList(_context.Set<Author>(), "ID", "FullName", AuthorID);

        BookD = new BookData();

        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString;

        var books = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .AsNoTracking()
            .AsQueryable();

        if (AuthorID != null)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        BookD.Books = await books
            .OrderBy(b => b.Title)
            .ToListAsync();

        // cautare dupa titlu sau dupa numele autorului
        if (!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(s =>
                   (s.Author != null && s.Author.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                || (s.Author != null && s.Author.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                || s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        if (id != null)
        {
            BookID = id.Value;
            Book? book = BookD.Books
                .Where(i => i.ID == id.Value).SingleOrDefault();
            if (book != null && book.BookCategories != null)
            {
                BookD.Categories = book.BookCategories.Select(s => s.Category!);
            }
        }

        // sortare
        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                break;
            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Author?.FullName ?? "");
                break;
            case "author":
                BookD.Books = BookD.Books.OrderBy(s => s.Author?.FullName ?? "");
                break;
            default:
                BookD.Books = BookD.Books.OrderBy(s => s.Title);
                break;
        }
    }
}
