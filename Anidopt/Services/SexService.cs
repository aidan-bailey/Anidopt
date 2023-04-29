using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class SexService : ServiceBase<Sex>, ISexService
{
    public SexService(AnidoptContext context) : base(context)
    {
    }
}
