using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class BookAuhtorVM
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public  string Description { get; set; }

        public int AuthorId { get; set; }
        public IList<Author> Authors { get; set; }

        public IFormFile File { get; set; }

        public string ImageUrl { get; set; }
    }
}
