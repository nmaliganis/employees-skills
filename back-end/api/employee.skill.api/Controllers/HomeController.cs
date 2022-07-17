using Microsoft.AspNetCore.Mvc;

namespace employees.skills.api.Controllers
{
  /// <summary>
  /// HomeController
  /// </summary>
  public class HomeController : Controller
  {

    // GET: /<controller>/
    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
      return new RedirectResult("~/swagger");
    }
  }
}