using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using covid_19.Models;
using covid_19.Controllers;
using covid_19.Data;
using Microsoft.EntityFrameworkCore;

namespace covid_19.Controllers
{
    public class LocalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Countries
        public async Task<IActionResult> ListCountries(string searchString, string sortOrder, string currentFilter)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.GenreSortParam = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewBag.ItemsSortParam = sortOrder == "Items" ? "items_desc" : "Items";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentSearch = searchString;

            if (!String.IsNullOrEmpty(currentFilter))
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var countries = await _context.Countries.OrderByDescending(c => c.Date).ThenByDescending(x => x.Cases).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
                countries = countries.Where(c => c.Name.Contains(searchString)).ToList();

            return View(countries);
        }
    }
}