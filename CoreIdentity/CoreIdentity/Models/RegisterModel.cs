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
        public string Name { get; set; }
        [Required]
        [Display(Name = "Kullanıcı Adı:")]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
