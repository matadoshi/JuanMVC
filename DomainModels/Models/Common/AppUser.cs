using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels.Models.Common
{
    public class AppUser:IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 255)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 255)]
        public string Surname { get; set; }
        [Required]
        public bool IsActivated { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
