using EducationScrapers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Library.Caching.CachingStrategies
{
    public class DefaultEducationCachingStrategy : IEducationCachingStrategy
    {
        private static Random rnd = new Random();

        public bool KeepInCache(Education education)
        {
            TimeSpan diff = DateTime.Now - education.DateFetched;
            return rnd.Next(0, 101) <= KeepPercentageChance(diff.TotalHours);
        }

        private double KeepPercentageChance(double hours)
        {
            if (hours > 24) return -1;
            if (hours < 2) return 100;
            return 100 - (100 / 24 * hours);
        }
    }
}
