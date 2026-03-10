using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace Core
{
    [Table("Person")]
    public class Person
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
        [Required, MaxLength(80)] public string FirstName { get; set; }
        [Required, MaxLength(80)] public string LastName { get; set; }
        [Required, MaxLength(100)] public string Address { get; set; }
        [Required, MaxLength(6)] public string Gender { get; set; }
    }
}
