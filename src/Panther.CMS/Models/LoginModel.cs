using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Panther.CMS.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username invalid.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Passowrd invalid.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
