using DomainModels.Models.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels.Models.BlogModel
{
    public class Blog:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int BlogCategoryId { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string DetailImage { get; set; }
        [NotMapped,Required]
        public IFormFile Photo { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public Author Author { get; set; }
    }
}
