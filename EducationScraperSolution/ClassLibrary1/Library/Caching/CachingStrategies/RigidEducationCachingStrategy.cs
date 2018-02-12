using System;
using System.Collections.Generic;
using System.Text;
using EducationScrapers.Models;

namespace EducationScrapers.Library.Caching.CachingStrategies
{
    public class RigidEducationCachingStrategy : IEducationCachingStrategy
    {
        public bool KeepInCache(Education education)
        {
            TimeSpan diff = DateTime.Now - education.DateFetched;
            return diff.TotalHours < 12;
        }
    }
}
