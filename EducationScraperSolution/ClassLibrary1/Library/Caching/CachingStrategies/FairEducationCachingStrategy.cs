using EducationScrapers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Library.Caching.CachingStrategies
{
    public class FairEducationCachingStrategy : IEducationCachingStrategy
    {
        public int FreshHours { get; set; } = 2;
        public int StaleHours { get; set; } = 24;

        private static Random rnd = new Random();

        public bool KeepInCache(Education education)
        {
            TimeSpan diff = DateTime.Now - education.DateFetched;
            return rnd.Next(0, 101) <= KeepPercentageChance(diff.TotalHours);
        }

        private double KeepPercentageChance(double hours)
        {
            if (hours > StaleHours) return -1;
            if (hours < FreshHours) return 100;
            return 100 - (100 / StaleHours * hours);
        }
    }
}
