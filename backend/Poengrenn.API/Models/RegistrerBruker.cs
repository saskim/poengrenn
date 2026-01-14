using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Poengrenn.API.Models
{
    public class RegistrerBruker
    {
        [Required]
        public string Brukernavn { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Passord { get; set; }
    }
}
