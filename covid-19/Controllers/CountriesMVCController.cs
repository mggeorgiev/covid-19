using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using covid_19.Data;
using covid_19.Models;
using ClosedXML.Excel;
using System.Text;
using System.IO;

namespace covid_19.Controllers
{
    public class CountriesMVCController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesMVCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.OrderByDescending(c=>c.Date).ThenByDescending(x=>x.Cases).ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cases,TodayCases,Deaths,TodayDeaths,Recovered,Active,Critical,Date")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cases,TodayCases,Deaths,TodayDeaths,Recovered,Active,Critical,Date")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AllMVC/Csv
        public async Task<IActionResult> Csv()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id,Name,Date,Cases,Recovered,Deaths,TodayCases,TodayDeaths,Active,Critical,Country");

            var countries = await _context.Countries.ToListAsync();

            foreach (var item in countries)
            {
                builder.AppendLine($"{item.Id}" +
                                   $",{item.Name}" +
                                   $",{item.Date}" +
                                   $",{item.Cases}" +
                                   $",{item.Recovered}" +
                                   $",{item.Deaths}" +
                                   $",{item.TodayCases}" +
                                   $",{item.TodayDeaths}" +
                                   $",{item.Active}" +
                                   $",{item.Critical}" +
                                   $",{item.Name}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString())
                                , "text/csv"
                                , "countries-"+ DateTime.Now.ToString("yyyy-MM-dd HHmmss") +".csv");
        }

        // GET: AllMVC/Excel
        public async Task<IActionResult> Excel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("All");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Date";
                worksheet.Cell(currentRow, 3).Value = "Cases";
                worksheet.Cell(currentRow, 4).Value = "Recovered";
                worksheet.Cell(currentRow, 5).Value = "Deaths";
                worksheet.Cell(currentRow, 6).Value = "TodayCases";
                worksheet.Cell(currentRow, 7).Value = "TodayDeaths";
                worksheet.Cell(currentRow, 8).Value = "Active";
                worksheet.Cell(currentRow, 9).Value = "Critical";
                worksheet.Cell(currentRow, 10).Value = "Country";

                var countries = await _context.Countries.ToListAsync();

                foreach (var item in countries)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Id;
                    worksheet.Cell(currentRow, 2).Value = item.Date;
                    worksheet.Cell(currentRow, 3).Value = item.Cases;
                    worksheet.Cell(currentRow, 4).Value = item.Recovered;
                    worksheet.Cell(currentRow, 5).Value = item.Deaths;
                    worksheet.Cell(currentRow, 6).Value = item.TodayCases;
                    worksheet.Cell(currentRow, 7).Value = item.TodayDeaths;
                    worksheet.Cell(currentRow, 8).Value = item.Active;
                    worksheet.Cell(currentRow, 9).Value = item.Critical;
                    worksheet.Cell(currentRow, 10).Value = item.Name;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "all-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xlsx");
                }
            }
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
