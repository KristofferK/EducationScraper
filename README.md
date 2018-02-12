# EducationScraper
Project in c# (ASP.NET Core MVC) for scraping educations from a few Danish academies. The scrapers will support caching to reduce the load on the websites being scraped.

## Goals for the final project
* It should be able to scrape a few Danish academies for the educations that they offer.
* Users should be able to access the scrapers through a browser of their choice. For this ASP.NET Core MVC will be used.
* The actual scrapers should be in a seperate project. This project will be using Class Library (.NET Core) to seperate it from the actual website.
* The scrapers should be using a caching strategy. This is necessary to reduce the load on the target websites.

## Implemented so far
* View and controller for selected the desired scrapers (ASP.NET Core MVC)
* Scrapers (.NET Core Class Library )
  * UCNDk. University College of Northren Denmark. https://www.ucn.dk/english/programmes-and-courses?filter=all
  * BaaaDk. Business Academy Aarhus. https://www.baaa.dk/programmes/
  * EalDk. Lillebælt Academy. https://www.eal.dk/international/education/full-degree-students/Programmes/
* Caching
  * FairEducationCachingStrategy
    * Data from within 2 hours ago is considered fresh, and will remain in the cache.
    * Data older than 24 hours is considered stale, and will have to be regathered.
    * Data between 2 and 24 hours will slowly be phased out. The newer the data, the higher the chance of it remaining in the cache. Keep percentage is described as "100 - (100 / 24 * hours)".
  * RigidEducationCachingStrategy
    * Data from within 12 hours is considered fresh, and will remain in the the cache.
    * Data older than 12 hours, is considered stale, and will have to be regathered.
  * NoEducationCachingStraegy
    * Considers all data to be stale. Basically you don't have caching. Mostly useful for when developing or debugging a scraper.

## Not implemented yet
* Not all scrapers have been implemented yet.