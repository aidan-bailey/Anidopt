using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class DescriptorLink : EntityModelBase
{
    #region Native Properties

    [Required]
    public int AnimalId { get; set; }

    [Required]
    public int DescriptorId { get; set; }

    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(AnimalId))]
    public virtual Animal? Animal { get; set; }

    [ForeignKey(nameof(DescriptorId))]
    public virtual Descriptor? Descriptor { get; set; }

    #endregion

}
