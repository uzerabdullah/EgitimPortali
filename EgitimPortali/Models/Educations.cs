using System.ComponentModel.DataAnnotations;

namespace EgitimPortali.Models
{
    public class Educations
    {
        [Key]
        public int EducationId { get; set; }
        public string EducationTitle { get; set; }
        public string Time { get; set; }
        public bool IsActive { get; set; }
    }
}
