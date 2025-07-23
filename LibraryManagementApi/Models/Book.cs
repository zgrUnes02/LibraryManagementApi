using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApi.Models
{
    public class Book : History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateOnly PublishDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public Book()
        {
            
        }
    }
}
