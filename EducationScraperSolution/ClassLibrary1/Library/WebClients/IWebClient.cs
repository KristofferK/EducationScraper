using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Library.WebClients
{
    internal interface IWebClient
    {
        string Get(string url);
        string Post(string url, string payload);
    }
}