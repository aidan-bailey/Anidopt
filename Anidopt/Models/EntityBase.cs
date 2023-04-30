using Microsoft.EntityFrameworkCore;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public class EntityBase
{
    public int Id { get; set; }
}
