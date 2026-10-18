using System.ComponentModel.DataAnnotations;

public class CourseCreateDto
{
    [Required(ErrorMessage = "Название курса обязательно")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "Количество кредитов должно быть в диапазоне от 1 до 10")]
    public int Credits { get; set; }

    [Required]
    public int TeacherId { get; set; }
}

    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int TeacherId { get; set; }
    }