# EducationScraper
Project in c# (ASP.NET Core MVC) for scraping educations from a few Danish academies. The scrapers will support caching to reduce the load on the websites being scraped.

## The final project will include
* Will scrape a few Danish academies for the educations that they offer.
* The user will access the scrapers through the browsers. For this ASP.NET Core MVC will be used.
* The actual scrapers will be in a Class Library (.NET Core) project to seperate it from the ASP.NET project.
* The scrapers will be using a caching strategy, that will reduce the load on the target websites.

## Implemented
* Some scrapers
  * UCNDk. University College Nordjylland. https://www.ucn.dk/english/programmes-and-courses?filter=all
* Caching
  * FairEducationCachingStrategy
    * Data from within 2 hours ago is considered fresh, and will remain in the cache.
    * Data older than 24 hours is considered stale, and will have to be regathered.
    * Data between 2 and 24 hours will slowly be phased out. The newer the data, the higher the chance of it remaining in the cache. Keep percentage is described as "100 - (100 / 24 * hours)".
  * RigidCachingStrategy
    * Data from within 12 hours is considered fresh, and will remain in the the cache.
    * Data older than 12 hours, is considered stale, and will have to be regathered