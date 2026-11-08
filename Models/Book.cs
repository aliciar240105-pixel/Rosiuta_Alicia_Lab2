using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rosiuta_Alicia_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; } //navigation property

        public int? AuthorID { get; set; }
        public Author? Author { get; set; } //navigation property

        public ICollection<Borrowing>? Borrowings { get; set; }

        [Display(Name = "Categories")]
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
