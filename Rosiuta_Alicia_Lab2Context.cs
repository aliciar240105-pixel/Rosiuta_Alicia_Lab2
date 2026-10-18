using Microsoft.EntityFrameworkCore;

public class Rosiuta_Alicia_Lab2Context(DbContextOptions<Rosiuta_Alicia_Lab2Context> options) : DbContext(options)
{
    public DbSet<Rosiuta_Alicia_Lab2.Models.Book> Book { get; set; } = default!;
}
