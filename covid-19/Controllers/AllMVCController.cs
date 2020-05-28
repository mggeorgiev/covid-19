using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using covid_19.Data;
using covid_19.Models;
using System.Text;
using ClosedXML.Excel;
using System.IO;
using covid_19.Data.Migrations;
using ClosedXML;

namespace covid_19.Controllers
{
    public class AllMVCController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllMVCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AllMVC
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.DateSortParm = sortOrder == "asc" ? "date_desc" : "asc";
            ViewBag.CasesSortParam = sortOrder == "cases_asc" ? "cases_desc" : "cases_asc";
            ViewBag.DeathsSortParam = sortOrder == "deaths_asc" ? "deaths_desc" : "deaths_asc";
            ViewBag.RecoveredSortParam = sortOrder == "recovered_asc" ? "recovered_desc" : "recovered_asc";
            ViewBag.CurrentSort = sortOrder;

            //var filter = from record in _context.All
            //             group record by new
            //             {
            //                 record.Date.Year,
            //                 record.Date.Month,
            //                 record.Date.Day,
            //             }
            //                into g
            //             select new
            //             {
            //                  Date = g.Max(c => c.Date)
            //             };
            //var distinctDates = await filter.ToListAsync();

            //var allDistinct = await (from record in _context.All
            //                         where distinctDates.Contains(new { record.Date })
            //                         select record).ToListAsync();
            //var allDistinct = _context.All
            //                                .Where(x => distinctDates.Contains(new { x.Date }))
            //                                .Tolist();

            //var all = await _context.All.ToListAsync();
            var all = await _context.All.FromSqlRaw("SELECT* FROM[All]" +
                                                    "WHERE [Date] in " +
                                                            "(SELECT TOP(1000) Max([Date])" +
                                                                    "FROM[All]" +
                                                                    "GROUP BY YEAR([Date])," +
                                                                    "Month([Date])," +
                                                                    "Day([Date]))"
                                                    ).ToListAsync();

            switch (sortOrder)
            {
                case "asc":
                    all = all.OrderBy(a => a.Date).ToList();
                    break;
                case "cases_asc":
                    all = all.OrderBy(a => a.Cases).ToList();
                    break;
                case "cases_desc":
                    all = all.OrderByDescending(a => a.Cases).ToList();
                    break;
                case "deaths_asc":
                    all = all.OrderBy(a => a.Deaths).ToList();
                    break;
                case "deaths_desc":
                    all = all.OrderByDescending(a => a.Deaths).ToList();
                    break;
                case "recovered_asc":
                    all = all.OrderBy(a => a.Recovered).ToList();
                    break;
                case "recovered_desc":
                    all = all.OrderByDescending(a => a.Recovered).ToList();
                    break;
                default:
                    all = all.OrderByDescending(a => a.Date).ToList();
                    break;
            }
            return View(all);
        }

        // GET: AllMVC
        public async Task<IActionResult> List(string sortOrder)
        {
            ViewBag.DateSortParm = sortOrder == "asc" ? "date_desc" : "asc";
            ViewBag.CasesSortParam = sortOrder == "cases_asc" ? "cases_desc" : "cases_asc";
            ViewBag.DeathsSortParam = sortOrder == "deaths_asc" ? "deaths_desc" : "deaths_asc";
            ViewBag.RecoveredSortParam = sortOrder == "recovered_asc" ? "recovered_desc" : "recovered_asc";
            ViewBag.CurrentSort = sortOrder;

            var all = await _context.All.ToListAsync();

            switch (sortOrder)
            {
                case "asc":
                    all = all.OrderBy(a => a.Date).ToList();
                    break;
                case "cases_asc":
                    all = all.OrderBy(a => a.Cases).ToList();
                    break;
                case "cases_desc":
                    all = all.OrderByDescending(a => a.Cases).ToList();
                    break;
                case "deaths_asc":
                    all = all.OrderBy(a => a.Deaths).ToList();
                    break;
                case "deaths_desc":
                    all = all.OrderByDescending(a => a.Deaths).ToList();
                    break;
                case "recovered_asc":
                    all = all.OrderBy(a => a.Recovered).ToList();
                    break;
                case "recovered_desc":
                    all = all.OrderByDescending(a => a.Recovered).ToList();
                    break;
                default:
                    all = all.OrderByDescending(a => a.Date).ToList();
                    break;
            }
            return View(all);
        }

        // GET: Load
        public async Task<IActionResult> Load()
        {
            var allDTO = covid_19.Infrastructure.Heraku.GetAllDTOFromHeraku("");
            var all = new All(allDTO)
            { 
                Date = DateTime.Now 
            };

            _context.Add(all);
            await _context.SaveChangesAsync();

            return View("Index", await _context.All.OrderByDescending(a => a.Date).ToListAsync());
        }

        // GET: AllMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var all = await _context.All
                .FirstOrDefaultAsync(m => m.Id == id);
            if (all == null)
            {
                return NotFound();
            }

            return View(all);
        }

        // GET: AllMVC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Cases,Deaths,Recovered")] All all)
        {
            if (ModelState.IsValid)
            {
                _context.Add(all);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(all);
        }

        // GET: AllMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var all = await _context.All.FindAsync(id);
            if (all == null)
            {
                return NotFound();
            }
            return View(all);
        }

        // POST: AllMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Cases,Deaths,Recovered")] All all)
        {
            if (id != all.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(all);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllExists(all.Id))
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
            return View(all);
        }

        // GET: AllMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var all = await _context.All
                .FirstOrDefaultAsync(m => m.Id == id);
            if (all == null)
            {
                return NotFound();
            }

            return View(all);
        }

        // POST: AllMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var all = await _context.All.FindAsync(id);
            _context.All.Remove(all);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AllMVC/Csv
        public async Task<IActionResult> Csv()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id,Date,Cases,Recovered,Deaths");

            var all = await _context.All.ToListAsync();

            foreach (var item in all)
            {
                builder.AppendLine($"{item.Id},{item.Date},{item.Cases},{item.Recovered},{item.Deaths}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "all.csv");
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

                var all = await _context.All.ToListAsync();
    
                foreach (var item in all)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Id;
                    worksheet.Cell(currentRow, 2).Value = item.Date;
                    worksheet.Cell(currentRow, 3).Value = item.Cases;
                    worksheet.Cell(currentRow, 4).Value = item.Recovered;
                    worksheet.Cell(currentRow, 5).Value = item.Deaths;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "all-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") +".xlsx");
                }
            }
        }

        private bool AllExists(int id)
        {
            return _context.All.Any(e => e.Id == id);
        }
    }
}
