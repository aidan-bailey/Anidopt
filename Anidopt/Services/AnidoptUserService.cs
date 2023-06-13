using Anidopt.Data;
using Anidopt.Identity;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class AnidoptUserService : EntityServiceBase<AnidoptUser>, IAnidoptUserService {
    public AnidoptUserService(AnidoptContext context) : base(context) {
    }
}
