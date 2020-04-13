using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid_19.Data.DTOs
{
    public class countryArray
    {
        public countryArray[] Property1 { get; set; }
    }

    public class countryDTO
    {
        public string country { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public int casesPerOneMillion { get; set; }
        public int deathsPerOneMillion { get; set; }
        public int totalTests { get; set; }
        public int testsPerOneMillion { get; set; }

    }

}
