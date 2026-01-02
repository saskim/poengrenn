using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
    public class LoginResponse
    {
        public string Brukernavn { get; set; }
        public string Rolle { get; set; }
        public string Token { get; set; }
        public IEnumerable<int> PersonIDer { get; set; }
    }
}
