using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    [Table("Books")]
    public class Book : BaseEntity
    {
        [Required] public string? Title { get; set; }
        [Required] public string? Author { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public DateTime LaunchDate { get; set; }
    }
}
