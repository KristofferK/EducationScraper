# EducationScraper
Project in c# (ASP.NET Core MVC) for scraping educations from a few Danish academies. The scrapers will support caching to reduce the load on the websites being scraped.

## Overview
* Will scrape a few Danish academies for the educations that they offer.
* The user will access the scrapers through the browsers. For this ASP.NET Core MVC will be used.
* The actual scrapers will be in a Class Library (.NET Core) project to seperate it from the ASP.NET project.
* The scrapers will be using a caching strategy, that will reduce the load on the target websites.