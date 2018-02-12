using System;
using System.Collections.Generic;
using System.Text;
using EducationScrapers.Models;
using static EducationScrapers.Library.ScrapingUtilities;
using System.Text.RegularExpressions;
using System.Linq;

namespace EducationScrapers.Scrapers.UCNDk
{
    public class UCNDkScraper : IScraper
    {
        public IEnumerable<string> GetLinks()
        {
            var source = ReadSource("https://www.ucn.dk/english/programmes-and-courses?filter=all");
            var pattern = "\n        <a target=\"\" alt=\"\" title=\"\" href=\"/(english/programmes-and-courses/[^\"]+)";
            return Regex.Matches(source, pattern)
                .OfType<Match>()
                .Select(e => "https://www.ucn.dk/" + e.Groups[1].Value)
                .Where(e => !e.Contains("ngành"));
        }

        public Education GetEducationFromLink(string link)
        {
            var education = new Education()
            {
                Link = link,
                DateFetched = DateTime.Now
            };

            return education;
        }
    }
}
