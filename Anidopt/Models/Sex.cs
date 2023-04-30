using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Sex: EntityBase
{
    [Required]
    public string? Name { get; set; }
}
