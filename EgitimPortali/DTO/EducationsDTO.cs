using System.ComponentModel.DataAnnotations;

namespace EgitimPortali.DTO
{
    public class EducationsDTO
    {
        [Key]
        public int EducationId { get; set; }
        public string EducationTitle { get; set; }
        public string Time { get; set; }
        public bool IsActive { get; set; }
    }
}
