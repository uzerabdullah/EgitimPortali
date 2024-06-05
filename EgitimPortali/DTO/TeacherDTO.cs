using System.ComponentModel.DataAnnotations;

namespace EgitimPortali.DTO
{
    public class TeacherDTO
    {
        [Key]
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
    }
}
