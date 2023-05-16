using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class PictureUpload
{
    public static IList<string> SupportedImageTypes = new List<string> { "image/jpeg", "image/png" };

    [Required]
    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }

    [Required]
    public int AnimalId { get; set; }

    [Required]
    public bool Showcase { get; set; }
}
