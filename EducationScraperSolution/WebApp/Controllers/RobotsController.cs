using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationScrapers.Scrapers;

namespace WebApp.Controllers
{
    public class RobotsController : Controller
    {
        public IActionResult UCNDk()
        {
            var result = new Scraper(new EducationScrapers.Scrapers.UCNDk.UCNDkScraper(), 75).Scrape();
            return Json(result);
        }
    }
}