using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid_19.Models
{
    public class Country
    {
        public int Id { get; set; }
        //public string Country { get; set; }
        public string Name { get; set; }
        public int Cases { get; set; }
        public int TodayCases { get; set; }
        public int Deaths { get; set; }
        public int TodayDeaths { get; set; }
        public int Recovered { get; set; }
        public int Active { get; set; }
        public int Critical { get; set; }

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
        }
    }
}
