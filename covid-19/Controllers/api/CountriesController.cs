using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using covid_19.Data;
using covid_19.Models;
using System.Net.Http;
using Newtonsoft.Json;
using covid_19.Data.DTOs;

namespace covid_19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry()
        {
            //Country country = new Country();

            HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync(path);

            client.BaseAddress = new Uri("https://coronavirus-19-api.herokuapp.com/");
            //HTTP GET


            var responseTask = client.GetAsync("countries");
            responseTask.Wait();
            var content = responseTask.Result.Content.ReadAsStringAsync().Result;

            if (responseTask.Result.IsSuccessStatusCode)
            {
                List<countryDTO> countryList = JsonConvert.DeserializeObject<List<countryDTO>>(content);


                //countryDTO alls = JsonConvert.DeserializeObject<countryDTO>(content);
                List<Country> list = new List<Country>();

                foreach (countryDTO item in countryList)
                {
                    Country country = new Country();
                    country.Name = item.country;
                    country.Recovered = item.recovered;
                    country.TodayCases = item.todayCases;
                    country.TodayDeaths = item.todayDeaths;
                    country.Active = item.active;
                    country.Critical = item.critical;
                    country.Cases = item.cases;
                    country.Deaths = item.deaths;
                    country.Date = DateTime.Now;
                    list.Add(country);
                }

                _context.Countries.AddRange(list);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else //web api sent error response 
            {
                //log response status here..

                throw new NotImplementedException();
            }

            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return country;
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
