using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LanguageExchangeHub1.Data.Models;

[Table("CourseUser")]
public class CourseUser
{
    [ForeignKey(nameof(Course))]
    public string CourseId { get; set; }
    public virtual Course Course { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public virtual User User { get; set; }

    [Key]
    public int Id { get; set; }
}


