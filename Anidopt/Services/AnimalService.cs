using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class AnimalService : EntityServiceBase<Animal>, IAnimalService
{
    public AnimalService(AnidoptContext context) : base(context)
    {
    }
}
