using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Models
{
    public enum EducationLevels
    {
        Master, // Kandidat & master in Danish
        Bachelor, // Bachelor in Danish
        ProfessionalBachelor, // Professionsbachelor in Danish
        TopUpDegree, // Top-up in Danish
        AcademyProfession // Erhvervsakademisk Uddannelse
    }
    public class Education
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public EducationLevels? Level { get; set; }
        public string Link { get; set; }
        public int DurationInMonths { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public DateTime DateFetched { get; set; }
        public string Description { get; set; }
    }
}
