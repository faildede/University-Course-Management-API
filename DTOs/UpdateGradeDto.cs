using System.ComponentModel.DataAnnotations;
public class UpdateGradeDto
{
    [Range(0, 100)]
    public decimal Grade { get; set; }
}