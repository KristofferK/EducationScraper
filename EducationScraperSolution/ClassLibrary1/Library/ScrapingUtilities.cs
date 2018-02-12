using EducationScrapers.Library.WebClients;
using EducationScrapers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationScrapers.Library
{
    internal static class ScrapingUtilities
    {
        public static IWebClient WebClient { get; set; } = new SmartWebClient();
        private static Dictionary<string, EducationLevels> educationLevelFilter;

        static ScrapingUtilities()
        {
            educationLevelFilter = new Dictionary<string, EducationLevels>()
            {
                { "top-up", EducationLevels.TopUpDegree },
                { "professional bachelor", EducationLevels.ProfessionalBachelor },
                { "ap-degree", EducationLevels.AcademyProfession },
                { "academy profession", EducationLevels.AcademyProfession },
            };
        }

        public static string ReadSource(string url, string payload = null)
        {
            return payload == null ? WebClient.Get(url) : WebClient.Post(url, payload);
        }

        public static string[] Split(string s, string delimiter)
        {
            return s.Split(new string[] { delimiter }, StringSplitOptions.None);
        }

        public static string StripTags(object o, bool keepLinebreaks = true)
        {
            if (o == null) return null;
            string s = o.ToString();
            if (keepLinebreaks)
            {
                s = Regex.Replace(s, "<[ /]*br[ /]*>", "\n", RegexOptions.IgnoreCase);
            }
            s = s.Replace("\t", "").Replace('\u0009'.ToString(), "");
            s = s.Replace("\r\n", "\n");
            s = s.Replace("\r", "");
            s = s.Replace("orttelefon", "ORTTELE");
            s = WebUtility.HtmlDecode(Regex.Replace(s, "<[^>]*(>|$)", string.Empty)).Trim(); // Fjerner tags
            s = Regex.Replace(s, "\n\\s+\n", "\n\n");
            s = Regex.Replace(s, "\n{3,}", "\n\n"); // Remove new-line spam
            s = Regex.Replace(s, "\\.([a-zA-ZæøåÆØÅ])", ". ${1}"); // Changes "Word1.Word2" to "Word1. Word2".
            s = Regex.Replace(s, " {2,}", ". ").Trim(); // Fix space-spam
            s = Regex.Replace(s, "\\.{2,}", ".").Trim(); // Fix period-spam
            s = Regex.Replace(s, "\\. [\\. ]+", ". ");
            s = Regex.Replace(s, "\n\\.[ ]*", "\n"); // New lines shouldn't start with a period.
            s = Regex.Replace(s, "\n{3,}", "\n\n"); // Have to fix new-line spam again.
            return s;
        }

        public static EducationLevels? SearchForEducationLevel(string s)
        {
            s = s.ToLower();
            var filter = educationLevelFilter.Where(e => s.Contains(e.Key)).FirstOrDefault();
            return filter.Key != null ? filter.Value : (EducationLevels?)null;
        }

        public static string InBetween(string haystack, string afterThis, string beforeThis)
        {
            return InBetween(haystack, afterThis, beforeThis, 1, 0);
        }

        public static string InBetween(string haystack, string afterThis, string beforeThis,
            int afterIndex, int beforeIndex, bool includeAfterAndBefore = false)
        {
            if (haystack.Contains(afterThis))
            {
                var split = Split(haystack, afterThis);
                if (split.Length > afterIndex)
                {
                    string rv = split[afterIndex];
                    if (rv.Contains(beforeThis))
                    {
                        split = Split(rv, beforeThis);
                        if (split.Length > beforeIndex)
                        {
                            rv = split[beforeIndex];

                            if (includeAfterAndBefore)
                            {
                                rv = $"{afterThis}{rv}{beforeThis}";
                            }

                            return rv;
                        }
                    }
                }
            }
            return null;
        }
    }
}
