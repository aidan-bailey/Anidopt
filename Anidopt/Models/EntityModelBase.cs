using Microsoft.EntityFrameworkCore;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public partial class EntityModelBase
{
    public int Id { get; set; }
}
