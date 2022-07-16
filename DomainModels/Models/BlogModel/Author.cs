using DomainModels.Models.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels.Models.BlogModel
{
    public class Author:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Image { get; set; }
        [Required,NotMapped]
        public IFormFile Photo { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
