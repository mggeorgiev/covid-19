using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace covid_19.Models
{
    public class Country
    {
        public int Id { get; set; }
        [DisplayName("Country")]
        public string Name { get; set; }
        [DisplayName("Cases")]
        public int Cases { get; set; }
        [DisplayName("Today Cases")]
        public int TodayCases { get; set; }
        [DisplayName("Deaths")]
        public int Deaths { get; set; }
        [DisplayName("Today Deaths")]
        public int TodayDeaths { get; set; }
        [DisplayName("Recovered")]
        public int Recovered { get; set; }
        [DisplayName("Active")]
        public int Active { get; set; }
        [DisplayName("Critical")]
        public int Critical { get; set; }
        [DisplayName("Cases Per 1M")]
        public int casesPerOneMillion { get; set; }
        [DisplayName("Deaths Per 1M")]
        public int deathsPerOneMillion { get; set; }
        [DisplayName("Total Tests")]
        public int totalTests { get; set; }
        [DisplayName("Tests Per 1M")]
        public int testsPerOneMillion { get; set; }
        public DateTime Date { get; set; }

        public Country() 
        {
        
        }

        public Country (covid_19.Data.DTOs.countryDTO countryDTO)
        {
            this.Active = countryDTO.active;
            this.Cases = countryDTO.cases;
            this.Critical = countryDTO.critical;
            this.Deaths = countryDTO.deaths;
            this.Name = countryDTO.country;
            this.Recovered = countryDTO.recovered;
            this.TodayCases = countryDTO.todayCases;
            this.TodayDeaths = countryDTO.todayDeaths;
            this.testsPerOneMillion = countryDTO.testsPerOneMillion;
            this.totalTests = countryDTO.totalTests;
            this.deathsPerOneMillion = countryDTO.deathsPerOneMillion;
            this.casesPerOneMillion = countryDTO.casesPerOneMillion;
        }
    }
}
