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
            try
            {
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
            catch (System.Exception e)
            {
                Console.WriteLine("Error reading from {0}. Message = {1}", e.TargetSite.Name.ToString(), e.Message);
                return null;
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

            try
            {

                responseTask.Wait();


                var content = responseTask.Result.Content.ReadAsStringAsync().Result;

                if (responseTask.Result.IsSuccessStatusCode)
                {
                    List<countryDTO> countryList = JsonConvert.DeserializeObject<List<countryDTO>>(content,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    countryList.RemoveAll(t => t.country == "Total:");
                    return countryList;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    return null;
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error reading from {0}. Message = {1}", e.TargetSite.Name.ToString(), e.Message);
                return null;
            }
        }
    }
}
