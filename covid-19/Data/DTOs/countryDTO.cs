using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Country")]
        public string country { get; set; }
        [DisplayName("Cases")]
        public int cases { get; set; }
        [DisplayName("Today Cases")]
        public int todayCases { get; set; }
        [DisplayName("Deaths")]
        public int deaths { get; set; }
        
        [DisplayName("Today Deaths")]
        public int todayDeaths { get; set; }
        [DisplayName("Recovered")]
        public int recovered { get; set; }
        [DisplayName("Active")]
        public int active { get; set; }
        public int critical { get; set; }
        [DisplayName("Cases Per 1M")]
        public int casesPerOneMillion { get; set; }
        [DisplayName("Deaths Per 1M")]
        public int deathsPerOneMillion { get; set; }
        [DisplayName("Total Tests")]
        public int totalTests { get; set; }
        [DisplayName("Tests Per 1M")]
        public int testsPerOneMillion { get; set; }

    }

}
