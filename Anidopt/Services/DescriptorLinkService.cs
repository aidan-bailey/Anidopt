using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class DescriptorLinkService : EntityServiceBase<DescriptorLink>, IDescriptorLinkService
{
    public DescriptorLinkService(AnidoptContext context): base(context)
    {
    }
}
