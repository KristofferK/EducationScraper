using EducationScrapers.Library.WebClients;
using EducationScrapers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Scrapers
{
    public interface IScraper
    {
        IEnumerable<string> GetLinks();
        Education GetEducationFromLink(string link);
    }
}
