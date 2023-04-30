using Microsoft.EntityFrameworkCore;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public partial class EntityBase
{
    public int Id { get; set; }
}
