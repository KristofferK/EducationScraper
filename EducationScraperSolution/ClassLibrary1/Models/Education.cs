using System;
using System.Collections.Generic;
using System.Text;

namespace EducationScrapers.Models
{
    public enum EducationsLevels
    {
        Master, // Kandidat & master in Danish
        Bachelor, // Bachelor in Danish
        ProfessionalBachelor, // Professionsbachelor in Danish
        AcademyProfession // Erhvervsakademisk Uddannelse
    }
    public class Education
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string[] Languages { get; set; }
        public int DurationInMonths { get; set; }
        public DateTime DateFetched { get; set; }
    }
}
