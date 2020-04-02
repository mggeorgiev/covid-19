using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using covid_19.Models;
using covid_19.Data.DTOs;
using System.Net.Http;
using Newtonsoft.Json;

namespace covid_19.Controllers
{
    public class WebController : Controller
    {
        public IActionResult Index()
        {
            var all = new allDTO();

            all = GetAllFromHeraku("");

            if(all != null)
                return View(all);

            throw new NotImplementedException();
        }

        public IActionResult Countries()
        {
            var countries = new covid_19.Data.DTOs.countryDTO();

            if (countries != null)
                return View(countries);

            throw new NotImplementedException();
        }

        public static allDTO GetAllFromHeraku(string BaseAddress)
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
    }
}