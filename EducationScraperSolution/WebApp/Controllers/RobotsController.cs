using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationScrapers.Scrapers;
using EducationScrapers.Library.Caching.CachingStrategies;
using EducationScrapers.Scrapers.UCNDk;
using EducationScrapers.Scrapers.BaaaDk;

namespace WebApp.Controllers
{
    public class RobotsController : Controller
    {
        public IActionResult UCNDk()
        {
            var scrapingStrategy = new UCNDkScraper();
            var cachingStrategy = new FairEducationCachingStrategy();
            var result = new Scraper(scrapingStrategy, cachingStrategy).Scrape();
            return Json(result);
        }

        public IActionResult BaaaDk()
        {
            var scrapingStrategy = new BaaaDkScraper();
            var cachingStrategy = new FairEducationCachingStrategy();
            var result = new Scraper(scrapingStrategy, cachingStrategy).Scrape();
            return Json(result);
        }
    }
}