using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Picture : EntityModelBase
{
    #region Native Properties
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get;}

    [Required]
    public byte[] Image { get; set; }

    [Required]
    public int Position { get; set; }

    [Required]
    public int AnimalId { get; set; }
    
    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(AnimalId))]
    public virtual Animal? Animal { get; set; }
    
    #endregion
}
