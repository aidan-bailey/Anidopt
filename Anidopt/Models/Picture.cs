using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Picture : EntityModelBase
{
    #region Native Properties
    
    [Required]
    public byte[] Image { get; set; }

    [Required]
    public bool Showcase { get; set; }

    [Required]
    public int AnimalId { get; set; }
    
    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(AnimalId))]
    public virtual Animal? Animal { get; set; }
    
    #endregion
}
