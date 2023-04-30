using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class EstimationService : EntityServiceBase<Estimation>, IEstimationService
{
    public EstimationService(AnidoptContext context) : base(context)
    {
    }
}
