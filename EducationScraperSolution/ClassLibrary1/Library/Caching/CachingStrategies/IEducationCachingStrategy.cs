using EducationScrapers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Library.Caching.CachingStrategies
{
    public interface IEducationCachingStrategy
    {
        bool KeepInCache(Education education);
    }
}
