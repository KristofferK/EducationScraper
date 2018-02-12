using System;
using System.Collections.Generic;
using System.Text;
using EducationScrapers.Models;
using static EducationScrapers.Library.ScrapingUtilities;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using System.Net;

namespace EducationScrapers.Scrapers.BaaaDk
{
    public class BaaaDkScraper : IScraper
    {
        public IEnumerable<string> GetLinks()
        {
            var source = ReadSource("https://www.baaa.dk/programmes/");
            source = Split(source, "Select according to your interests</a>")[0];
            var pattern = "<li class=\"\">\\s*<a href=\"/(programmes/[a-z\\-]+/[a-z\\-]+/)\">";
            return Regex.Matches(source, pattern)
                .OfType<Match>()
                .Select(e => "https://www.baaa.dk/" + e.Groups[1].Value);
        }

        public Education GetEducationFromLink(string link)
        {
            var source = ReadSource(link);
            var htmlNode = HtmlNode.CreateNode(source);

            var education = new Education(link);

            // Title
            education.Title = htmlNode.SelectSingleNode("//title").InnerText;

            // Description
            var description = htmlNode.SelectSingleNode("//article[@class='page-text overflow-enabled']").InnerHtml;
            education.Description = StripTags(description);

            // Level
            if (link.Contains("/bachelors-degree/")) education.Level = EducationLevels.ProfessionalBachelor;
            else if (link.Contains("/ap-degree/")) education.Level = EducationLevels.AcademyProfession;
            else if (link.Contains("/bachelors-top-up-degree/")) education.Level = EducationLevels.TopUpDegree;

            // Languages
            education.Languages = new List<string> { "English" };

            // Duration
            education.DurationInMonths = ExtractDuration(education, htmlNode);

            // Location
            education.Location = ExtractLocation(source);

            // Billeder
            education.Images.Add("https://www.baaa.dk" + htmlNode.SelectSingleNode("//picture/source").Attributes["srcset"].Value);

            return education;
        }

        private int ExtractDuration(Education education, HtmlNode htmlNode)
        {
            if (education.Level == EducationLevels.TopUpDegree)
            {
                return 18;
            }

            var pageSubtitle = htmlNode.SelectSingleNode("//h2[@class='page-subtitle']")?.InnerText;
            if (pageSubtitle.Contains(" - "))
            {
                var yearsText = WebUtility.HtmlDecode(Split(pageSubtitle, " - ")[1]);
                var duration = int.Parse(yearsText.Substring(0, 1)) * 12;
                if (yearsText.Contains("½"))
                {
                    duration += 6;
                }
                return duration;
            }

            if (education.Description.Contains("2-year"))
            {
                return 24;
            }

            return -1;
        }

        private string ExtractLocation(string source)
        {
            var location = InBetween(source, "You take the programme at:", "</p><!---->") ??
                InBetween(source, "<h2>Contact info</h2>", "<a href");

            return StripTags(location?.Replace("<br>", " "))?.Replace("\n", " ") ?? "Sønderhøj 30, 8260 Viby";
        }
    }
}