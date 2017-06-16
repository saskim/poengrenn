using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poengrenn.API.Models
{
    public class LoginBruker
    {
        [Required]
        public string Brukernavn { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Passord { get; set; }
    }
}