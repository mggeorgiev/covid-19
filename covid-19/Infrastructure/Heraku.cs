using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using covid_19.Models;
using covid_19.Data.DTOs;
using Newtonsoft.Json;

namespace covid_19.Infrastructure
{
    public class Heraku
    {
        public static allDTO GetAllDTOFromHeraku(string BaseAddress)
        {
            All all = new All();
            //Martin: this will overuse the server's resources
            HttpClient client = new HttpClient();

            //HTTP GET
            if (BaseAddress == "")
                client.BaseAddress = new Uri("https://coronavirus-19-api.herokuapp.com/");

            var responseTask = client.GetAsync("all");
            responseTask.Wait();


            var content = responseTask.Result.Content.ReadAsStringAsync().Result;

            if (responseTask.Result.IsSuccessStatusCode)
            {
                allDTO alls = JsonConvert.DeserializeObject<allDTO>(content);

                return alls;
            }
            else //web api sent error response 
            {
                //log response status here..
                throw new NotImplementedException();
            }
        }

        public static List<countryDTO> GetCountryFromHeraku(string BaseAddress)
        {
            //Country country = new Country();
            //Martin: this will overuse the server's resources
            HttpClient client = new HttpClient();

            //HTTP GET
            if (BaseAddress == "")
                client.BaseAddress = new Uri("https://coronavirus-19-api.herokuapp.com/");

            var responseTask = client.GetAsync("countries");
            responseTask.Wait();


            var content = responseTask.Result.Content.ReadAsStringAsync().Result;

            if (responseTask.Result.IsSuccessStatusCode)
            {
                List<countryDTO> countryList = JsonConvert.DeserializeObject<List<countryDTO>>(content);
                countryList.RemoveAll(t => t.country == "Total:");
                return countryList;

                //countryDTO alls = JsonConvert.DeserializeObject<countryDTO>(content);
                //List<Country> list = new List<Country>();

                //foreach (countryDTO item in countryList)
                //{
                //    Country country = new Country();
                //    country.Name = item.country;
                //    country.Recovered = item.recovered;
                //    country.TodayCases = item.todayCases;
                //    country.TodayDeaths = item.todayDeaths;
                //    country.Active = item.active;
                //    country.Critical = item.critical;
                //    country.Cases = item.cases;
                //    country.Deaths = item.deaths;
                //    country.Date = DateTime.Now;
                //    list.Add(country);
                //}
                //return list;
            }
            else //web api sent error response 
            {
                //log response status here..
                return null;
            }
        }
    }
}
