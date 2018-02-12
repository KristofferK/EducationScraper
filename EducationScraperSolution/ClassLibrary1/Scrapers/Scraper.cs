using EducationScrapers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationScrapers.Scrapers
{
    public class Scraper
    {
        public IScraper ScrapingStrategy { get; set; }
        public int ScrapingLimit { get; set; }

        public Scraper(IScraper scrapingStraetgy, int scrapingLimit = 10)
        {
            ScrapingStrategy = scrapingStraetgy;
            ScrapingLimit = scrapingLimit;
        }

        public IEnumerable<Education> Scrape()
        {
            var educations = new List<Education>();
            var scraped = 0;

            var links = ScrapingStrategy.GetLinks();
            foreach (var link in links)
            {
                if (scraped++ < ScrapingLimit)
                {
                    var education = ScrapingStrategy.GetEducationFromLink(link);
                    if (education != null)
                    {
                        educations.Add(education);
                    }
                }
            }

            return educations;
        }
    }
}
