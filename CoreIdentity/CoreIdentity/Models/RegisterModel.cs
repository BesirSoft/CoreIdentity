using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Kullanıcı Adı:")]

        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Şifre:")]
        public string Password { get; set; }
        
    }
}
