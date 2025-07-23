using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApi.Models
{
    public class Author : History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly? Birthdate { get; set; }
        public string? Nationality { get; set; }
        public ICollection<Book> Books { get; set; }
        public Author()
        {
            
        }
    }
}
