using covid_19.Data;
using covid_19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace covid_19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AllController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<All>>> GetAll()
        {
            return await _context.All.ToListAsync();
        }

        //public static async void GetAllFromHeraku(string endpoint)
        //{
        //    //Define your baseUrl
        //    string baseUrl = "https://coronavirus-19-api.herokuapp.com/";
        //    //Have your using statements within a try/catch blokc that will catch any exceptions.
        //    try
        //    {
        //        //HttpClient client_veni = new HttpClient();
        //        //We will now define your HttpClient with your first using statement which will use a IDisposable.
        //        using (HttpClient client = new HttpClient())
        //        {
        //            //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
        //            //The HttpResponseMessage which contains status code, and data from response.
        //            using (HttpResponseMessage res = await client.GetAsync(baseUrl + endpoint))
        //            {
        //                //Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
        //                using (HttpContent content = res.Content)
        //                {
        //                    //Now assign your content to your data variable, by converting into a string using the await keyword.
        //                    var data = await content.ReadAsStringAsync();
        //                    //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
        //                    if (data != null)
        //                    {
        //                        //Now log your data in the console
        //                        Console.WriteLine("data------------{0}", data);

        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("NO Data----------");
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine("Exception Hit------------");
        //        Console.WriteLine(exception);
        //    }
        //}

        // GET: api/All/5
        [HttpGet("{id}")]
        public async Task<ActionResult<All>> GetAll(int id)
        {
            var all = await _context.All.FindAsync(id);

            if (all == null)
            {
                return NotFound();
            }

            return all;
        }

        // PUT: api/All/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAll(int id, All all)
        {
            if (id != all.Id)
            {
                return BadRequest();
            }

            _context.Entry(all).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllExists(id))
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

        // POST: api/All
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<All>> PostAll()
        {

            All all = new All();

            all = GetAllFromHeraku("");

            if (all != null )

            _context.All.Add(all);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = all.Id }, all);
        }

        // DELETE: api/All/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<All>> DeleteAll(int id)
        {
            var all = await _context.All.FindAsync(id);
            if (all == null)
            {
                return NotFound();
            }

            _context.All.Remove(all);
            await _context.SaveChangesAsync();

            return all;
        }

        private All GetAllFromHeraku(string BaseAddress)
        {
            All all = new All();
            //Martin: this will overuse the server's resources
            HttpClient client = new HttpClient();

            //HTTP GET
            if(BaseAddress == "")
                client.BaseAddress = new Uri("https://coronavirus-19-api.herokuapp.com/");

            var responseTask = client.GetAsync("all");
            responseTask.Wait();


            var content = responseTask.Result.Content.ReadAsStringAsync().Result;

            if (responseTask.Result.IsSuccessStatusCode)
            {
                allDTO alls = JsonConvert.DeserializeObject<allDTO>(content);

                all.Cases = alls.cases;
                all.Deaths = alls.deaths;
                all.Recovered = alls.recovered;
                all.Date = DateTime.Now;

                return all;
            }
            else //web api sent error response 
            {
                //log response status here..
                return all;
            }
        }
        private bool AllExists(int id)
        {
            return _context.All.Any(e => e.Id == id);
        }
    }
}
