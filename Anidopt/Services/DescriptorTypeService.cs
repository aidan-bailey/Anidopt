using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class DescriptorTypeService : EntityServiceBase<DescriptorType>, IDescriptorTypeService
{
    public DescriptorTypeService(AnidoptContext context) : base(context)
    {
    }
}
