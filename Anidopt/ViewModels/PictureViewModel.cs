using System.ComponentModel.DataAnnotations;

namespace Anidopt.ViewModels;

public class PictureViewModel {
    public static IList<string> SupportedImageTypes = new List<string> { "image/jpeg", "image/png" };

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }

    [Required]
    public int Position { get; set; }

    [Required]
    public int AnimalId { get; set; }
}
