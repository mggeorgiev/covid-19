using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using covid_19.Models;

namespace covid_19.Controllers
{
    public class WebController : Controller
    {
        public IActionResult Index()
        {
            var all = new allDTO();

            if(all != null)
                return View(all);

            throw new NotImplementedException();
        }
    }
}