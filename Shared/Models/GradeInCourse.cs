using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared;

public class GradeInCourse
{
    [Key]
    public int GradeInCourse_Id { get; set; }
    [MaxLength(4)][Required]
    public string CourseCode { get; set; }
    [Required]
    public int Grade { get; set; }
    
    [ForeignKey("Student")]
    public int Student_Id { get; set; }

   
}