﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using covid_19.Data;
using covid_19.Models;
using System.Text;

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
            //if (sortOrder == "desc")
            //    var all = await _context.All
            //        .FirstOrDefaultAsync(m => m.Id == id);

            var all = await _context.All.ToListAsync();
            //sortOrder == "desc" ? all.OrderByDescending(a => a.Date) : all.OrderBy(a => a.Date);
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

        public async Task<IActionResult> Csv()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id,Date,Cases,Recovered,Deaths");

            //var alls = new All();
            var all = await _context.All.ToListAsync();

            foreach (var item in all)
            {
                builder.AppendLine($"{item.Id},{item.Date},{item.Cases},{item.Recovered},{item.Deaths}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "all.csv");
        }

        public IActionResult Excel()
        {
            throw new NotImplementedException();
        }

        private bool AllExists(int id)
        {
            return _context.All.Any(e => e.Id == id);
        }
    }
}
