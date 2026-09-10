using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Course
{
    public int CourseId { get; set; }

    [Required]
    [StringLength(60)]
    [Column(TypeName = "text(60)")]
    public string? Title { get; set; }

    public ICollection<Student>? Students { get; set; }
}