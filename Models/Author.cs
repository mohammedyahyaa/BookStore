using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string FullName { get; set; }
    }
}
