using System;
using System.Collections.Generic;
using System.Text;
using EducationScrapers.Models;
using static EducationScrapers.Library.ScrapingUtilities;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;

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
            var source = ReadSource(link);
            var htmlNode = HtmlNode.CreateNode(source);

            if (!source.Contains("<b>Language</b>"))
            {
                return null;
            }

            var education = new Education()
            {
                Link = link,
                DateFetched = DateTime.Now
            };

            // Title
            var imageWithTitle = htmlNode.SelectSingleNode("//div[contains(@class, 'image active')]");
            education.Title = imageWithTitle.Attributes["attr-headline"].Value;

            // Description
            var description = htmlNode.SelectSingleNode("//div[@class='col-md-8 text-padding']").InnerHtml;
            description = Split(description, "<div class=\"col-md-2 col-md-offset-1 study-info\">")[0];
            description = description.Replace("<li>", "* ");
            education.Description = StripTags(description);

            // Level
            var level = htmlNode.SelectSingleNode("//h2[@class='h1']/following-sibling::p[1]").InnerText.Trim();
            education.Level = SearchForEducationLevel(level);

            // Languages
            var languageNode = htmlNode.SelectSingleNode("//b[text() = 'Language']/following-sibling::text[1]");
            education.Languages = languageNode.InnerText
                .Split('\n')
                .Select(e => e.Trim())
                .Where(e => !string.IsNullOrWhiteSpace(e));

            // Duration
            var duration = htmlNode.SelectSingleNode("//b[text() = 'Duration']/following-sibling::p[1]").InnerText;
            education.DurationInMonths = int.Parse(duration.Substring(0, 1)) * 12;
            if (duration.Contains("½"))
            {
                education.DurationInMonths += 6;
            }

            // Location
            var location = htmlNode.SelectSingleNode("//b[text() = 'Location']/following-sibling::p[1]").InnerHtml;
            education.Location = location.Replace("<br>", " & ");

            return education;
        }
    }
}