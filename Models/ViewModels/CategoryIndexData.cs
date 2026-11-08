using Rosiuta_Alicia_Lab2.Models;

namespace Rosiuta_Alicia_Lab2.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
