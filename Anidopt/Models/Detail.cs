using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

    public virtual DescriptorLink? DescriptorLink { get; set; }
}
