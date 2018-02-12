using EducationScrapers.Library.Caching.CachingStrategies;
using EducationScrapers.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EducationScrapers.Library.Caching
{
    class EducationCache : LocalCache
    {
        public IEducationCachingStrategy CachingStrategy { get; set; }
        public EducationCache(IEducationCachingStrategy cachingStrategy, string identifier) : base("education_", identifier)
        {
            CachingStrategy = cachingStrategy;
        }

        public Dictionary<string, Education> Load()
        {
            var educations = new Dictionary<string, Education>();
            var array = FileToJArray(filepath);

            if (array != null)
            {
                foreach (var element in array)
                {
                    var education = JsonConvert.DeserializeObject<Education>(element.ToString());
                    if (CachingStrategy.KeepInCache(education))
                    {
                        educations[education.Link] = education;
                    }
                }
            }

            return educations;
        }

        public bool Save(IEnumerable<Education> educations)
        {
            foreach (var education in educations)
            {
                if (education.DateFetched == null)
                {
                    throw new Exception("Couldn't save to cache, since DateFetched is null DateFetched");
                }
            }

            var feed = JsonConvert.SerializeObject(educations);
            File.WriteAllText(filepath, feed);
            return true;
        }
    }
}