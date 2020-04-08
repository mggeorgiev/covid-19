using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using covid_19.Models;
using covid_19.Data.DTOs;
using System.Net.Http;
using Newtonsoft.Json;
using covid_19.Infrastructure;
using covid_19.Data;

namespace covid_19.Controllers
{
    public class WebController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var all = new allDTO();

            all = Infrastructure.Heraku.GetAllDTOFromHeraku("");

            if(all != null)
                return View(nameof(Index), all);

            return View();
        }

        public async Task<IActionResult> Save(int cases, int deaths, int recovered, DateTime timestamp)
        {
            var allDTO = new allDTO();

            allDTO.cases = cases;
            allDTO.deaths = deaths;
            allDTO.recovered = recovered;

            //all = Infrastructure.Heraku.GetAllDTOFromHeraku("");

            if (allDTO != null)
            {
                var all = new All(allDTO);
                all.Date = timestamp;

                _context.Add(all);
                await _context.SaveChangesAsync();

                return View(nameof(Index), allDTO);
            }

            return View();
        }

        public IActionResult Countries()
        {
            var countries = new covid_19.Data.DTOs.countryDTO();

            if (countries != null)
                return View(countries);

            throw new NotImplementedException();
        }
    }
}