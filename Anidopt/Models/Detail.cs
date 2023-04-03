using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public class Detail
{
    /*********************
     * NATIVE PROPERTIES *
     *********************/

    public int Id { get; set; }

    [Required]
    public string? Description { get; set; }

    /****************
     * FOREIGN KEYS *
     ****************/

    [Required]
    public int DescriptorLinkId { get; set; }

    /********************
     * PROXY PROPERTIES *
     ********************/

    [ForeignKey(nameof(DescriptorLinkId))]
    public virtual DescriptorLink? DescriptorLink { get; set; }
}
