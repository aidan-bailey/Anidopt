using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Anidopt.Models;

namespace Anidopt.ViewModels;

public class ClassifiedViewModel {
    #region Animal Properties

    [Required]
    public string? Name { get; set; }

    [Required]
    public DateTime BirthDay { get; set; }

    public string? Description { get; set; }

    [Required]
    public virtual float Weight { get; set; }

    [Required]
    public int BreedId { get; set; }

    [Required]
    public int SexId { get; set; }

    [Required]
    public int SizeId { get; set; }

    [ForeignKey(nameof(SizeId))]
    public virtual Size? Size { get; set; }

    [ForeignKey(nameof(SexId))]
    public virtual Sex? Sex { get; set; }

    [ForeignKey(nameof(BreedId))]
    public virtual Breed? Breed { get; set; }

    public virtual List<DescriptorLink>? DescriptorLinks { get; set; } = new();

    public virtual List<AnimalColourLink>? AnimalColourLinks { get; set; } = new();

    public virtual List<Picture> Pictures { get; set; } = new();

    #endregion
}
