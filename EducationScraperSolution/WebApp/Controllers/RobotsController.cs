using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationScrapers.Scrapers;
using EducationScrapers.Library.Caching.CachingStrategies;
using EducationScrapers.Scrapers.UCNDk;

namespace WebApp.Controllers
{
    public class RobotsController : Controller
    {
        public IActionResult UCNDk()
        {
            var scrapingStrategy = new UCNDkScraper();
            var cachingStrategy = new DefaultEducationCachingStrategy();
            var result = new Scraper(scrapingStrategy, cachingStrategy).Scrape();
            return Json(result);
        }
    }
}