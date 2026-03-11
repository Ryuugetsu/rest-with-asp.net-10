using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    [Table("Person")]
    public class Person : BaseEntity
    {
        [Required, MaxLength(80)] public string? FirstName { get; set; }
        [Required, MaxLength(80)] public string? LastName { get; set; }
        [Required, MaxLength(100)] public string? Address { get; set; }
        [Required, MaxLength(6)] public string? Gender { get; set; }
    }
}
