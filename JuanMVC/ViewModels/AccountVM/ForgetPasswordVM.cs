using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.ViewModels.AccountVM
{
    public class ForgetPasswordVM
    {
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
