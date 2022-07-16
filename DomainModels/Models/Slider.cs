using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModels.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string MainTitle { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string RedirectUrl { get; set; }
    }
}
