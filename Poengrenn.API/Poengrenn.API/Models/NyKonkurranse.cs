using System;
using System.Collections.Generic;
using System.Linq;

namespace Poengrenn.API.Models
{
    public class NyKonkurranse
    {
        public string TypeID { get; set; }
        public string Navn { get; set; }
        public List<DateTime> Datoer { get; set; }
    }
}
