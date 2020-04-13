using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace covid_19.Models
{
    public class allDTO
    {
        [DisplayName("Cases")]
        public int cases { get; set; }

        [DisplayName("Deaths")]
        public int deaths { get; set; }
        [DisplayName("Recovered")]
        public int recovered { get; set; }
    }
}
