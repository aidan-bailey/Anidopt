using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public class Sex
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
}
