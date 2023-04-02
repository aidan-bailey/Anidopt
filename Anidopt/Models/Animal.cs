using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public class Animal
{

    /*********************
     * NATIVE PROPERTIES *
     *********************/

    public int Id { get; set; }
 
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public DateTime BirthDay { get; set; }

    public string? Description { get; set; }

    [Required]
    public float AdultHeightEstimation { get; set; }

    [Required]
    public float AdultWeightEstimation { get; set; }

    /****************
     * FOREIGN KEYS *
     ****************/

    // ORGANISATION
    
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

}
