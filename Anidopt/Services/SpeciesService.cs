using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class SpeciesService : EntityServiceBase<Species>, ISpeciesService
{
    public SpeciesService(AnidoptContext context) : base(context)
    {
    }
}
