using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class PictureService : EntityServiceBase<Picture>, IPictureService
{
    public PictureService(AnidoptContext context) : base(context)
    {
    }
}
