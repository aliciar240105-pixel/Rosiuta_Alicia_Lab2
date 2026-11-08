using Microsoft.EntityFrameworkCore;

public class Rosiuta_Alicia_Lab2Context(DbContextOptions<Rosiuta_Alicia_Lab2Context> options) : DbContext(options)
{
    public DbSet<Rosiuta_Alicia_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Rosiuta_Alicia_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Rosiuta_Alicia_Lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<Rosiuta_Alicia_Lab2.Models.Category> Category { get; set; } = default!;
}
