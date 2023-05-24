using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anidopt.Controllers;

[Authorize("SiteAdmin")]
public class SiteAdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
