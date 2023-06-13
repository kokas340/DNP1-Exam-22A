using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Student
{
    [Key]
    public int Student_Id { get; set; }
    [MaxLength(25)][Required]
    public string Name { get; set; }
    [Required]
    public string Programme { get; set; }
    
    //public ICollection<GradeInCourse> GradeInCourse { get; set; }
    
   
}