using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public class Estimation
{
    /*********************
     * NATIVE PROPERTIES *
     *********************/

    public int Id { get; set; }

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
