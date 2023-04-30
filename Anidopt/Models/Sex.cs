using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Sex: EntityModelBase
{
    [Required]
    public string? Name { get; set; }
}
