using EducationScrapers.Library.Caching;
using EducationScrapers.Library.Caching.CachingStrategies;
using EducationScrapers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EducationScrapers.Scrapers
{
    public class Scraper
    {
        public IScraper ScrapingStrategy { get; set; }
        public IEducationCachingStrategy CachingStrategy { get; set; }
        public int ScrapingLimit { get; set; }
        public double? DelayInSecondsBetweenRequests { get; set; }

        public Scraper(IScraper scrapingStraetgy, IEducationCachingStrategy cachingStrategy, int scrapingLimit = 10)
        {
            ScrapingStrategy = scrapingStraetgy;
            ScrapingLimit = scrapingLimit;
            CachingStrategy = cachingStrategy;
        }

        public IEnumerable<Education> Scrape()
        {
            var educations = new List<Education>();

            var cacheIdentifier = ScrapingStrategy.GetType().Name;
            var cacheManager = new EducationCache(CachingStrategy, cacheIdentifier);
            var cache = cacheManager.Load();
            var scraped = 0;

            var links = ScrapingStrategy.GetLinks();
            foreach (var link in links)
            {
                if (cache.ContainsKey(link))
                {
                    educations.Add(cache[link]);
                }
                else if (scraped < ScrapingLimit)
                {
                    if (DelayInSecondsBetweenRequests.HasValue)
                    {
                        Thread.Sleep(Convert.ToInt32(DelayInSecondsBetweenRequests.Value * 1000));
                    }
                    var education = ScrapingStrategy.GetEducationFromLink(link);
                    if (education != null)
                    {
                        educations.Add(education);
                        scraped++;
                    }
                }
            }

            cacheManager.Save(educations);
            return educations;
        }
    }
}
