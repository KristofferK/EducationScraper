using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EducationScrapers.Library.WebClients
{
    internal class SmartWebClient : IWebClient
    {
        public string Get(string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.UserAgent] = "Education Scraper";
                return wc.DownloadString(url);
            }
        }

        public string Post(string url, string payload)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.UserAgent] = "Education scraper";
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                wc.Encoding = Encoding.UTF8;
                return wc.UploadString(url, payload);
            }
        }
    }
}
