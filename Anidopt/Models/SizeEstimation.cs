using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public class SizeEstimation
{
    /*********************
     * NATIVE PROPERTIES *
     *********************/

    public int Id { get; set; }

    [Required]
    public float Height { get; set; }

    [Required]
    public float Weight { get; set; }

    /****************
     * FOREIGN KEYS *
     ****************/

    [Required]
    public int BreedId { get; set; }

    [Required]
    public int SexId { get; set; }

    /**********************
     * VIRTUAL PROPERTIES *
     **********************/

    [ForeignKey(nameof(BreedId))]
    public virtual Breed? Breed { get; set; }

    [ForeignKey(nameof(SexId))]
    public virtual Sex? Sex { get; set; }
}
