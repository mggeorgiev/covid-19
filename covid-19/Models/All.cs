using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid_19.Models
{
    public class All
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int Cases { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }

        public double GetRemaining() 
        {
            return Cases - Deaths - Recovered;
        }

        public double GetRemainingPercent()
        {
            return (Cases - Deaths - Recovered) * 100/ Cases;
        }

        public double GetRecoveredPercent()
        {
            return (Recovered) * 100 / Cases;
        }

        public double GetDeathPercent()
        {
            return (Deaths) * 100 / Cases;
        }

        public double GetRecoveredToDeath()
        {
            return (Recovered/Deaths);
        }

        public All() {}

        public All(allDTO allDTO)
        {
            this.Cases = allDTO.cases;
            this.Deaths = allDTO.deaths;
            this.Recovered = allDTO.recovered;
        }
    }
}
