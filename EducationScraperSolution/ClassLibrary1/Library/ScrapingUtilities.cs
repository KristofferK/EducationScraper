using EducationScrapers.Library.WebClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Library
{
    internal static class ScrapingUtilities
    {
        public static IWebClient WebClient { get; set; } = new SmartWebClient();

        public static string ReadSource(string url, string payload = null)
        {
            return payload == null ? WebClient.Get(url) : WebClient.Post(url, payload);
        }
    }
}
