using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Animal : EntityModelBase
{

    /*********************
     * NATIVE PROPERTIES *
     *********************/
 
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public DateTime BirthDay { get; set; }

    public string? Description { get; set; }

    [Required]
    [Range(1, float.MaxValue)]
    public float Height { get; set; }

    [Required]
    [Range(1, float.MaxValue)]
    public float Weight { get; set; }

    /****************
     * FOREIGN KEYS *
     ****************/
    
    [Required]
    public int OrganisationId { get; set; }

    [Required]
    public int BreedId { get; set; }

    [Required]
    public int SexId { get; set; }

    /**********************
     * VIRTUAL PROPERTIES *
     **********************/

    [ForeignKey(nameof(SexId))]
    public virtual Sex? Sex { get; set; }

    [ForeignKey(nameof(BreedId))]
    public virtual Breed? Breed { get; set; }

    [ForeignKey(nameof(OrganisationId))]
    public virtual Organisation? Organisation { get; set; }

    public virtual List<DescriptorLink>? DescriptorLinks { get; set; }

    public virtual List<AnimalColourLink>? AnimalColourLinks { get; set; }
}
