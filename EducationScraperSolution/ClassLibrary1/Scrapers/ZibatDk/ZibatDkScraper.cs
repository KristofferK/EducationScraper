using System;
using System.Collections.Generic;
using System.Text;
using EducationScrapers.Models;
using static EducationScrapers.Library.ScrapingUtilities;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using System.Net;

namespace EducationScrapers.Scrapers.ZibatDk
{
    public class ZibatDkScraper : IScraper
    {
        public IEnumerable<string> GetLinks()
        {
            var source = ReadSource("http://zibat.dk/programmes-open-for-admission-for-september/");
            source = InBetween(source, "<div id=\"post\">", "</div> <!-- ENTRY END");
            var pattern = "<a href=\"/([^\"]+)";

            return Regex.Matches(source, pattern)
                .OfType<Match>()
                .Select(e => "http://zibat.dk/" + e.Groups[1].Value)
                .Distinct();
        }

        public Education GetEducationFromLink(string link)
        {
            var source = ReadSource(link);
            var htmlNode = HtmlNode.CreateNode(source);

            var education = new Education(link);

            // Title
            education.Title = htmlNode.SelectSingleNode("//h2").InnerText;

            // Description
            var description = htmlNode.SelectSingleNode("//div[@id='post']")?.InnerHtml;
            description = description.Replace("<li>", "* ");
            education.Description = StripTags(description);

            // Level
            if (education.Title.StartsWith("AP ")) education.Level = EducationLevels.AcademyProfession;
            else if (education.Title.StartsWith("Bachelor")) education.Level = EducationLevels.TopUpDegree; // All their bachelors are topups.

            // Languages
            education.Languages = new List<string> { "English" };

            // Duration
            var semesters = InBetween(source, "Semesters: ", "</br>");
            education.DurationInMonths = int.Parse(semesters) * 6;

            // Location
            var location = StripTags(InBetween(source, " is offered at</h4></div>", "</div>"));
            education.Location = location.Replace("\n", " & ");

            // Billeder
            var imageNode = htmlNode.SelectSingleNode("//div[@id='main-site']/img");
            if (imageNode != null)
            {
                education.Images.Add(imageNode.Attributes["src"].Value);
            }

            return education;
        }
    }
}
