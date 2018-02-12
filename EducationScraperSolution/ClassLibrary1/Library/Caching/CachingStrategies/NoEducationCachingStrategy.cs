using System;
using System.Collections.Generic;
using System.Text;
using EducationScrapers.Models;

namespace EducationScrapers.Library.Caching.CachingStrategies
{
    public class NoEducationCachingStrategy : IEducationCachingStrategy
    {
        public bool KeepInCache(Education education)
        {
            return false;
        }
    }
}
