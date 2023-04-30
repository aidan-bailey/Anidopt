using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class SexService : EntityServiceBase<Sex>, ISexService
{
    public SexService(AnidoptContext context) : base(context)
    {
    }
}
