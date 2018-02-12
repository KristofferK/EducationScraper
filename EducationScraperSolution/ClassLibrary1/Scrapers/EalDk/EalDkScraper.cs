using System;
using System.Collections.Generic;
using System.Text;
using EducationScrapers.Models;
using static EducationScrapers.Library.ScrapingUtilities;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using System.Net;

namespace EducationScrapers.Scrapers.EalDk
{
    public class EalDkScraper : IScraper
    {
        public IEnumerable<string> GetLinks()
        {
            var source = ReadSource("https://www.eal.dk/international/education/full-degree-students/Programmes/");
            var pattern = "title=\"link\" href=\"/(international/education/[^\"]+)";

            return Regex.Matches(source, pattern)
                .OfType<Match>()
                .Select(e => "https://www.eal.dk/" + e.Groups[1].Value);
        }

        public Education GetEducationFromLink(string link)
        {
            var source = ReadSource(link);
            var htmlNode = HtmlNode.CreateNode(source);

            var education = new Education(link);

            // Title
            education.Title = htmlNode.SelectSingleNode("//title").InnerText;

            // Description
            var description = htmlNode.SelectSingleNode("//div[@data-tab-content='content']")?.InnerHtml ??
                htmlNode.SelectSingleNode("//div[@data-tab-content='about']")?.InnerHtml;
            education.Description = StripTags(description);

            // Level
            if (link.Contains("/ba-programmes/")) education.Level = EducationLevels.ProfessionalBachelor;
            else if (link.Contains("/ap-degree-programmes/")) education.Level = EducationLevels.AcademyProfession;
            else if (link.Contains("/ba-top-up-programmes/")) education.Level = EducationLevels.TopUpDegree;

            // Languages
            education.Languages = new List<string> { "English" };

            // Duration
            var durationText = htmlNode.SelectSingleNode("//h3[text() = 'Duration']/following-sibling::p[1]").InnerText;
            education.DurationInMonths = int.Parse(durationText.Substring(0, 1)) * 12;
            if (durationText.Contains("½"))
            {
                education.DurationInMonths += 6;
            }

            // Location
            education.Location = ExtractLocation(source);

            // Billeder
            var bannerNode = htmlNode.SelectSingleNode("//div[@class='block--banner__image-container']/img");
            education.Images.Add("https://eal.dk" + bannerNode.Attributes["src"].Value);

            return education;
        }

        private string ExtractLocation(string source)
        {
            source = source.Replace("Department&nbsp;</h3>", "Department</h3>");

            var location = InBetween(source, ">Department</h3>", "</div>");
            location = location.Replace("<a ", "&<a");
            location = StripTags(location);
            location = location.Replace("\n", " ");
            location = location.Replace("Find on Google Maps", "");
            location = location.Substring(0, location.Length - 1);

            return location;
        }
    }
}
