using System.ComponentModel.DataAnnotations;

public class EnrollmentCreateDto
{
    [Required]
    public int StudentId { get; set; }
    [Required]
    public int CourseId { get; set; }
}