using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Descriptor : EntityModelBase
{
    #region Native Properties

    [Required]
    public string? Name { get; set; }

    [Required]
    public int DescriptorTypeId { get; set; }

    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(DescriptorTypeId))]
    public virtual DescriptorType? DescriptorType { get; set; }

    #endregion
}
