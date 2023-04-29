using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class SpeciesService : ServiceBase<Species>, ISpeciesService
{
    public SpeciesService(AnidoptContext context) : base(context)
    {
    }
}
